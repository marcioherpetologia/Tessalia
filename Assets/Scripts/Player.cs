using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    
    public PlayerInputSet input { get; private set;}
    private StateMachine stateMachine;
    
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState  { get; private set; }
    public PlayerJumpState jumpState  { get; private set; }
    public PlayerFallState fallState  { get; private set; }
    public PlayerWallSlideState wallSlideState  { get; private set; }
    public PlayerWallJumpState wallJumpState  { get; private set; }
    public PlayerDashState dashState  { get; private set; }
    public PlayerBasicAttackState basicAttackState { get; private set; }
    

    public Vector2 moveInput { get; private set; }
    
    [Header("Collision Detection")]
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    public bool groundDetected { get; private set; }
    public bool wallDetected { get; private set; }
    
    
    [Header("Move Details")]
    public float moveSpeed;
    public float jumpForce;
    [Range(0, 1)] public float wallSlideSlowMultiplier = .5f;
    [Range(0, 1)] public float inAirMoveMultiplier = .7f;
    private bool facingRight = true;
    public int facingDir { get; private set; } = 1;
    public Vector2 wallJumpForce;
    public float dashDuration = .25f;
    public float dashSpeed = 20f;
    
    [Header("Attack Details")]
    public Vector2[] attackVelocity;
    public float attackVelocityDuration;
    public float comboResetTime = 1;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        
        stateMachine = new StateMachine();
        input = new PlayerInputSet();

        idleState = new PlayerIdleState(this, stateMachine, "idle");
        moveState = new PlayerMoveState(this, stateMachine, "move");
        jumpState = new PlayerJumpState(this, stateMachine, "jumpFall");
        fallState = new PlayerFallState(this, stateMachine, "jumpFall");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "wallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "jumpFall");
        dashState = new PlayerDashState(this, stateMachine, "dash");
        basicAttackState = new PlayerBasicAttackState(this, stateMachine, "basicAttack");
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>(); //input, action map, action , momento que o input é reconhecido pelo jogo. O ctx é uma abreviação para context (ou InputAction.CallbackContext). Ele é o parâmetro de uma função callback enviada pelo Unity Input System sempre que uma ação acontece (como apertar ou soltar um botão).
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero; // Quando o jogador soltar os botões de movimento, zere o vetor de movimento.

    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        HandleCollisionDetection();
        stateMachine.UpdateActiveState();
    }

    public void CallAnimationTrigger()
    {
        stateMachine.currentState.CallAnimationTrigger();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    private void HandleFlip(float xVelocity)
    {
        if(xVelocity > 0 && !facingRight) // Ex. personagem virada para a esquerda, o input for pressionado para ir para a direita (linerVelocity é positivo agora) e ainda estiver virado para a esquerda, flipar 
            Flip();
        else if(xVelocity < 0 && facingRight)
            Flip();
    }

    public void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
        facingDir *= -1;
    }

    private void HandleCollisionDetection()
    {
        groundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(wallCheckDistance * facingDir, 0));
    }
}
