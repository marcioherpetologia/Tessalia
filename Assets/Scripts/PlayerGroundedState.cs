using UnityEngine;

public class PlayerGroundedState : EntityState // Esta classe será uma combinação de todos os estados de solo, ex. Idle e Jump
{
    public PlayerGroundedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        if(rb.linearVelocity.y < 0)
            stateMachine.ChangeState(player.fallState);
        
        if (input.Player.Jump.WasPerformedThisFrame()) 
            stateMachine.ChangeState(player.jumpState);
    }
}
