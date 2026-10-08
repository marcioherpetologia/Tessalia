using UnityEngine;

public class PlayerJumpState : PlayerAiredState
{
    public PlayerJumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    override public void Update()
    {
        base.Update();
        
        if(player.rb.linearVelocity.y < 0)
            stateMachine.ChangeState(player.fallState);
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(rb.linearVelocity.x, player.jumpForce);
    }
}


