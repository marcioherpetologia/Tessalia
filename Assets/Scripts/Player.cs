using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    
    private PlayerInputSet input;
    private StateMachine stateMachine;
    
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState  { get; private set; }

    public Vector2 moveInput { get; private set; }
    
    [Header("Move Details")]
    public float moveSpeed;
    
    private bool facingRight = true;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        
        stateMachine = new StateMachine();
        
        input = new PlayerInputSet();

        idleState = new PlayerIdleState(this, stateMachine, "idle");
        moveState = new PlayerMoveState(this, stateMachine, "move");
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>(); //input, action map, action , momento que o input é reconhecido pelo jogo
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
        stateMachine.UpdateActiveState();
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

    private void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }
}
