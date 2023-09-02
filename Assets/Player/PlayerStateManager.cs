using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour
{
    public float jumpForce = 20f;
    public float dashForce = 10f;

    public State groundState {get; private set;}
    public State crouchState {get; private set;}
    public State jumpState {get; private set;}
    private State currentState;

    public Rigidbody2D rigidbody2D {get; private set;}
    public Collider2D collider2D {get; private set;}
    public PlayerInput playerInput {get; private set;}
    public Animator animator {get; private set;}
    public Transform transform {get; private set;}
    public PlayerWeapon playerWeapon {get; private set;}
    public AudioSource audioSource {get; private set;}
    public AudioClip jumpSound;
    public AudioClip dashSound;
    public AudioClip landSound;
    public AudioClip parrySound;
    public GameObject landGroundEffect;

    public Collider2D groundCollider;
    public float move {get; set;}
    public float targetXaxis {get; set;}
    public float targetYaxis {get; set;}
    public bool isGrounded {get; set;}

    public bool isDashing {get; set;}
    public bool isShooting {get; set;}
    
    public enum Direction
    {
        Left = -1,
        Right = 1
    }
    public Direction direction {get; set;}

   
    
    void Awake()
    {
        groundState = new PlayerGroundState(this);
        crouchState = new PlayerCrouchState(this);
        jumpState = new PlayerJumpState(this);
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();
        playerWeapon = GetComponent<PlayerWeapon>();
        audioSource = GetComponent<AudioSource>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Start()
    {
        move = 0;
        targetXaxis = 0;
        targetYaxis = 0;
        isGrounded = true;
        isDashing = false;
        isShooting = false;
        direction = Direction.Right;
        currentState = groundState; 
        currentState.Enter(); 
        playerInput.DeactivateInput();
    }


    void Update()
    {
        currentState.UpdateState();
        Shoot();
    }

    public void Init(){
        playerInput.ActivateInput();
        animator.SetTrigger("intro");
    }
    public void ChangeState(State newState){
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void FallOneWayPlatform(Collider2D platformCollider){
        StartCoroutine(StartFallOneWayPlatform(platformCollider));
    }

    public IEnumerator StartFallOneWayPlatform(Collider2D platformCollider){
        Physics2D.IgnoreCollision(groundCollider, platformCollider, true);
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreCollision(groundCollider, platformCollider, false);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        //if other has layer ground trigger event
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground")){
            Debug.Log("HIT GROUND");
            isGrounded = true;
        }
    }

    public void LandGroundFX(){
        GameObject effect = Instantiate(landGroundEffect, transform.position, Quaternion.identity);
        effect.GetComponent<ParticleSystem>().Play();
    }


    public void OnMove(InputValue value){
        move = value.Get<Vector2>().x;
        targetXaxis = value.Get<Vector2>().x;
        targetYaxis = value.Get<Vector2>().y;
        animator.SetFloat("speed", Mathf.Abs(move));
        animator.SetFloat("targetYaxis", targetYaxis);
        Flip(move);
    }
   
    public void OnJump(){
        currentState.OnJump();
    }

    public void OnDash(){
        StartCoroutine(Dash());
    }

    public void OnShoot(InputValue value){
        animator.SetBool("isShooting", value.isPressed);
        isShooting = value.isPressed;
    }

    public void Shoot(){
        if(isShooting && !isDashing){
            playerWeapon.Shoot(currentState.getTargetRotation());
        } else{
            playerWeapon.StopShooting();
        }
    }

    public void OnSuper(){
        if(!playerWeapon.CanEX()){
            return;
        }
        animator.SetTrigger("superAttack");
    }
    public void Super(){
        playerWeapon.Super(currentState.getTargetRotation());
    }

    public IEnumerator Dash(){
        animator.SetTrigger("dash");
        isDashing = true;
        rigidbody2D.gravityScale = 0;
        rigidbody2D.velocity = new Vector2(dashForce * (int) direction, 0);
        audioSource.PlayOneShot(dashSound);
        yield return new WaitForSeconds(0.2f);
        isDashing = false;
        rigidbody2D.gravityScale = 5;
    }

    void Flip(float horizontal)
    {
        if(horizontal > 0){
            direction = PlayerStateManager.Direction.Right;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } else if(horizontal < 0){
            direction = PlayerStateManager.Direction.Left;
            transform.rotation = Quaternion.Euler(0, -180, 0);
        } else {
        }
    }
}
