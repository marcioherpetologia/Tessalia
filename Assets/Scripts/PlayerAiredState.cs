using UnityEngine;

public class PlayerAiredState : EntityState // SuperState no qual programaremos tudo que acontecerá no ar
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerAiredState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.moveInput.x != 0)
        {
            player.SetVelocity(player.moveInput.x * player.moveSpeed, rb.linearVelocity.y);
        }
    }
}
