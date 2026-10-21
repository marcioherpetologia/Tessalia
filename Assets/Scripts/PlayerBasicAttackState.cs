using UnityEngine;

public class PlayerBasicAttackState : EntityState
{
    private float attackVelocityTimer;
    
    private int comboIndex = 1;
    private const int FistComboIndex = 1;
    private int comboLimit = 3;
    
    public PlayerBasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        
    }

    public override void Update()
    {
        base.Update();
        
        HandleAttackVelocity();
        
        if(triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }

    public override void Enter()
    {
        base.Enter();
        
        ResetComboIndex();
        
        anim.SetInteger("basicAttackIndex", comboIndex);
        ApplyAttackVelocity();
    }
    public override void Exit()
    {
        base.Exit();
        comboIndex++;
    }

    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;
        
        if(attackVelocityTimer < 0)
            player.SetVelocity(0, rb.linearVelocityY);
    }

    private void ApplyAttackVelocity()
    {
        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(player.attackVelocity.x * player.facingDir, player.attackVelocity.y);
    }
    
    private void ResetComboIndex()
    {
        if(comboIndex > comboLimit)
            comboIndex = FistComboIndex;
    }

}
