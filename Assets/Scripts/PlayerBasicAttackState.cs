using UnityEngine;

public class PlayerBasicAttackState : EntityState
{
    private float attackVelocityTimer;
    
    private const int FistComboIndex = 1;
    private int comboIndex = 1;
    private int comboLimit = 3;

    private float lastTimeAttack;
    private bool comboAttackQueued;
    
    public PlayerBasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        if (comboLimit != player.attackVelocity.Length)
            comboLimit = player.attackVelocity.Length;
    }

    public override void Update()
    {
        base.Update();
        
        HandleAttackVelocity();

        if (input.Player.Attack.WasPressedThisFrame())
            comboAttackQueued = true;

        if (triggerCalled)
        {
            if(comboAttackQueued)
                stateMachine.ChangeState(player.basicAttackState);
            else
                stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Enter()
    {
        base.Enter();

        comboAttackQueued = false;
        
        ResetComboIndex();
        
        anim.SetInteger("basicAttackIndex", comboIndex);
        ApplyAttackVelocity();
    }
    public override void Exit()
    {
        base.Exit();
        comboIndex++;
        lastTimeAttack += Time.time;
    }

    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;
        
        if(attackVelocityTimer < 0)
            player.SetVelocity(0, rb.linearVelocityY);
    }

    private void ApplyAttackVelocity()
    {
        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];
        
        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(attackVelocity.x * player.facingDir, attackVelocity.y);
    }
    
    private void ResetComboIndex()
    {
        if (Time.time > lastTimeAttack + player.comboResetTime)     
            comboIndex = FistComboIndex;
        
        if(comboIndex > comboLimit)
            comboIndex = FistComboIndex;
    }

}
