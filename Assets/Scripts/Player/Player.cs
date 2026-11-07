using System;
using System.Collections;
using UnityEngine;

public class Player : Entity
{
    public PlayerInputSet input { get; private set;}
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState  { get; private set; }
    public PlayerJumpState jumpState  { get; private set; }
    public PlayerFallState fallState  { get; private set; }
    public PlayerWallSlideState wallSlideState  { get; private set; }
    public PlayerWallJumpState wallJumpState  { get; private set; }
    public PlayerDashState dashState  { get; private set; }
    public PlayerBasicAttackState basicAttackState { get; private set; }
    public PlayerJumpAttackState jumpAttackState { get; private set; }
    
    [Header("Move Details")]
    public float moveSpeed;
    public float jumpForce;
    [Range(0, 1)] public float wallSlideSlowMultiplier = .5f;
    [Range(0, 1)] public float inAirMoveMultiplier = .7f;
    public Vector2 wallJumpForce;
    public float dashDuration = .25f;
    public float dashSpeed = 20f;
    
    [Header("Attack Details")]
    public Vector2[] attackVelocity;
    public Vector2 jumpAttackVelocity;
    public float attackVelocityDuration;
    public float comboResetTime = 1;
    private Coroutine queuedAttackCo;
    
    public Vector2 moveInput { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        
        input = new PlayerInputSet();

        idleState = new PlayerIdleState(this, stateMachine, "idle");
        moveState = new PlayerMoveState(this, stateMachine, "move");
        jumpState = new PlayerJumpState(this, stateMachine, "jumpFall");
        fallState = new PlayerFallState(this, stateMachine, "jumpFall");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "wallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "jumpFall");
        dashState = new PlayerDashState(this, stateMachine, "dash");
        basicAttackState = new PlayerBasicAttackState(this, stateMachine, "basicAttack");
        jumpAttackState = new PlayerJumpAttackState(this, stateMachine, "jumpAttack");
    }

    protected override void Start()
    {
        base.Start();
        
        stateMachine.Initialize(idleState);
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
    
    
    public void EnterAttackStateWithDelay()
    {
        if(queuedAttackCo != null)
            StopCoroutine(EnterAttackStateWithDelayCo());

        queuedAttackCo = StartCoroutine(EnterAttackStateWithDelayCo());
    }

    private IEnumerator EnterAttackStateWithDelayCo()
    {
        yield return new WaitForEndOfFrame();
        stateMachine.ChangeState(basicAttackState);
    }
}
