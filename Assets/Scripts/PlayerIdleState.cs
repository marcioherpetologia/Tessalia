using Unity.VisualScripting;
using UnityEngine;

public class PlayerIdleState : EntityState
{
    public PlayerIdleState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    override public void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.F))
            stateMachine.ChangeState(player.moveState);
    }
    
    
}
