using UnityEngine;

public class PlayerDashState : EntityState
{
    public PlayerDashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
}
