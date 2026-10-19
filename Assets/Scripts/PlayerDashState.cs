using UnityEngine;

public class PlayerDashState : EntityState
{
    private float originalGravityScale;
    private int dashDir;
    
    public PlayerDashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        
        CancelDashIfNeeded();
        
        player.SetVelocity(player.dashSpeed * dashDir, 0);

        if (stateTimer < 0)
        {
            if(player.groundDetected)
                stateMachine.ChangeState(player.idleState);
            else
                stateMachine.ChangeState(player.fallState);
        }

    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;
        
        dashDir = player.facingDir;
        
        originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
        
    }

    override public void Exit()
    {
        base.Exit();
        player.SetVelocity(0, 0);
        rb.gravityScale = originalGravityScale;
    }

    private void CancelDashIfNeeded()
    {
        if (player.wallDetected)
        {
            if(player.groundDetected)
                stateMachine.ChangeState(player.idleState);
            else
                stateMachine.ChangeState(player.wallSlideState);
        }
    }
}
