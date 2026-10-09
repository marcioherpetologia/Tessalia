using UnityEngine;

public class PlayerWallSlideState : EntityState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerWallSlideState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

}
