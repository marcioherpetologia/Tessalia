using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string animBoolName;

    protected Animator anim;
    protected Rigidbody2D rb;
    protected PlayerInputSet input;

    public EntityState(Player player, StateMachine stateMachine, string animBoolName) // Construtor: método especial no qual será chamado quando criarmos instancias dessa classe
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        
        anim = player.anim;
        rb = player.rb;
        input = player.input;
    }

    public virtual void Update() // Fazer o override no Player
    {
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    public virtual void Enter() // Chamado todas as vezes que precisar entrar em novo state
    {
        anim.SetBool(animBoolName, true);
    }

    public virtual void Exit() // Chamado todas as vezes que precisar sair de novo state
    {
        anim.SetBool(animBoolName, false);
    }
}
