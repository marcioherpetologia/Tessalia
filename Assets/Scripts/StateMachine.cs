using UnityEngine;

public class StateMachine // Classe que será a referência para o state atual
{
    public EntityState currentState { get; private set; } // public para leitura e private para alteração
    
    public void Initialize(EntityState startState) // Método para inicializar um novo state
    {
        currentState = startState;
        currentState.Enter();
    }
}
