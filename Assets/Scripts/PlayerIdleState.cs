using Unity.VisualScripting;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        player.SetVelocity(0, rb.linearVelocity.y);
    }

    override public void Update()
    {
        base.Update();

        if (player.moveInput.x != 0)
            stateMachine.ChangeState(player.moveState);
    
    }
    
}
