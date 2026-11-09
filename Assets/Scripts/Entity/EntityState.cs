using UnityEngine;

public abstract class EntityState
{
    protected StateMachine stateMachine;
    protected string animBoolName;

    protected Animator anim;
    protected Rigidbody2D rb;

    protected float stateTimer;

    protected bool triggerCalled;

    public EntityState(StateMachine stateMachine, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }
    
    public virtual void Update() // Fazer o override no Player
    {
        stateTimer -=  Time.deltaTime;
    }
        
    public virtual void Enter() // Chamado todas as vezes que precisar entrar em novo state
    {
        anim.SetBool(animBoolName, true);
        triggerCalled = false;
    }

    public virtual void Exit() // Chamado todas as vezes que precisar sair de novo state
    {
        anim.SetBool(animBoolName, false);
    }

    public void CallAnimationTrigger()
    {
        triggerCalled = true;
    }

}
