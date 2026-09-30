using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string stateName;

    public EntityState(Player player, StateMachine stateMachine, string stateName) // Construtor: método especial no qual será chamado quando criarmos instancias dessa classe
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.stateName = stateName;
    }

    public virtual void Update() // Fazer o override no Player
    {
        
    }

    public virtual void Enter() // Chamado todas as vezes que precisar entrar em novo state
    {
        
    }

    public virtual void Exit() // Chamado todas as vezes que precisar sair de novo state
    {
        
    }
}
