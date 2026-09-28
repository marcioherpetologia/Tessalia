using UnityEngine;

public class EntityState
{
    protected StateMachine stateMachine;

    public EntityState(StateMachine stateMachine) // Construtor: método especial no qual será chamado quando criarmos instancias dessa classe
    {
        this.stateMachine = stateMachine;
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
