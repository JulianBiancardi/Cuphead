using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public float jumpForce = 20f;

    public State groundState {get; private set;}
    public State crouchState {get; private set;}
    public State jumpState {get; private set;}
    private State currentState;

    public Rigidbody2D rigidbody2D {get; private set;}
    public Collider2D collider2D {get; private set;}
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
    public bool isGrounded {get; set;}

    public bool isDashing {get; set;}
    public int superCount {get; set;}
    
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
    }

    void Start()
    {
        isGrounded = true;
        isDashing = false;
        superCount = 3;
        direction = Direction.Right;
        currentState = groundState; 
        currentState.Enter();   
    }


    void Update()
    {
       currentState.UpdateState();
    }

    public void ChangeState(State newState){
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void Dash(){
        StartCoroutine(StartDash());
    }

    public IEnumerator StartDash(){
        animator.SetTrigger("dash");
        isDashing = true;
        rigidbody2D.velocity = new Vector2(10 * (int)direction, rigidbody2D.velocity.y);
        audioSource.PlayOneShot(dashSound);
        yield return new WaitForSeconds(0.2f);
        isDashing = false;
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
        Debug.Log("HIT WITH " + other.gameObject.name);
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

    public void Super(){
        playerWeapon.Super(getTargetRotation());
    }

    private Quaternion getTargetRotation(){
        if(Input.GetKey(KeyCode.W)){
            animator.SetFloat("targetYaxis", 1);
            return Quaternion.Euler(0, transform.rotation.eulerAngles.y, 90);
        } else {
            animator.SetFloat("targetYaxis", 0);
            return Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }

}
