using UnityEngine;

public class Enemy : Entity
{
    public EnemyIdleState idleState;
    public EnemyMoveState moveState;
    public EnemyAttackState attackState;
    public EnemyBattleState battleState;

    [Header("Movement Details")] 
    public float idleTime = 2;
    public float moveSpeed = 1.4f;
    [Range(0,2)] public float moveAnimSpeedMultiplier = 1;
    
    [Header("Player Detection Details")]
    public Transform player { get; private set; }
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance;
    
    [Header("Battle Details")]
    public float battleMoveSpeed = 3.0f;
    public float attackDistance = 2.0f;
    public float battleTimeDuration = 5f;
    public float minRetreadDistance = 1.5f;
    public Vector2 retreadVelocity;

    public void TryEnterBattleState(Transform player)
    {
        if(stateMachine.currentState == battleState)
            return;
        
        if(stateMachine.currentState == attackState)
            return;
        
        this.player = player; //quando chamar o método em outro script, será passado um transform o qual será associado a variavel declarada neste script
        stateMachine.ChangeState(battleState);
    }
    
    
    public Transform GetPlayerReference()
    {
        if(player == null)
            player = PlayerDetected().transform;
        
        return player;
    }
    
    public RaycastHit2D PlayerDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, playerCheckDistance, whatIsPlayer | whatIsGround);

        if (hit.collider == null || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
            return default; // retorna o raycast com valor padrão
        
        return hit;
    }
    
    protected virtual void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (facingDir * playerCheckDistance), playerCheck.position.y));
        Gizmos.color = Color.coral;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (facingDir * attackDistance), playerCheck.position.y));
        Gizmos.color = Color.darkSlateBlue;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (facingDir * minRetreadDistance), playerCheck.position.y));
        
    }

}