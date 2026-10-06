using UnityEngine;

public class PlayerGroundedState : EntityState // Esta classe será uma combinação de todos os estados de solo, ex. Idle e Jump
{
    public PlayerGroundedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        if (input.Player.Jump.WasPerformedThisFrame())
        {
            Debug.Log("Jumping");
        }
        
    }
}
