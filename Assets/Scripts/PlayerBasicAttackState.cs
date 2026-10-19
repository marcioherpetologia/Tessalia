using UnityEngine;

public class PlayerBasicAttackState : EntityState
{
    private float attackVelocityTimer;
    
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
        
        GenerateAttackVelocity();
    }

    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;
        
        if(attackVelocityTimer < 0)
            player.SetVelocity(0, rb.linearVelocityY);
    }

    private void GenerateAttackVelocity()
    {
        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(player.attackVelocity.x * player.facingDir, player.attackVelocity.y);
    }

}
