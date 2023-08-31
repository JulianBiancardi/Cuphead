using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D collider2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    public float speed = 10.0f;
    private int direction = 1; // 1 = right, -1 = left
    public float jumpForce = 10.0f;
    public float dashForce = 10.0f;
    public Transform groundCheck;
    private bool isGrounded = false;
    private bool isCrouching = false;
    private bool isJumping = false;
    private bool isDashing = false;
    private bool isHittingStructure = false;
    private GameObject currentOneWayPlatform;
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public ParticleSystem jumpEffect;
    public AudioClip jumpSound;
    public AudioClip groundSound;
    public AudioClip dashSound;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        checkGrounded();
        move();
        crouch();
        jump();
        dash();
        attack();
        if (Input.GetKeyDown(KeyCode.Q) && currentOneWayPlatform != null){
            StartCoroutine(DisableCollider());
        }
    }

    void checkGrounded()
    {
        isGrounded = Physics2D.BoxCast(collider2D.bounds.center, collider2D.bounds.size, 0f, Vector2.down, .1f, 1 << LayerMask.NameToLayer("Ground"));
        animator.SetBool("isGrounded", isGrounded);
        if (isGrounded && isJumping){
            isJumping = false;
            if(jumpEffect != null){
                jumpEffect.Play();
            }
            audioSource.PlayOneShot(groundSound);
        } else {
        }
    }

    void move(){
        if(isCrouching || isDashing){
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal > 0){
            direction = 1;
        } else if (horizontal < 0){
            direction = -1;
        }

        rigidbody2D.velocity = new Vector2(horizontal * speed, rigidbody2D.velocity.y);
        animator.SetFloat("speed", Mathf.Abs(horizontal));
        spriteRenderer.flipX = direction == -1;
    }

    void crouch(){
        if (Input.GetKey(KeyCode.S) && isGrounded){
            animator.SetBool("isCrouching", true);
            isCrouching = true;
            rigidbody2D.velocity = new Vector2(0, 0);
        } else {
            animator.SetBool("isCrouching", false);
            isCrouching = false;
        }
    }
    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded){
            isGrounded = false;
            isCrouching = false;
            isJumping = true;
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
            animator.SetBool("isGrounded", false);
            audioSource.PlayOneShot(jumpSound);
        }
    }

    void dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && !isCrouching){
            rigidbody2D.velocity = new Vector2(dashForce * direction, rigidbody2D.velocity.y);
            animator.SetTrigger("dash");
            isDashing = true;
            StartCoroutine(stopDash());
        }
    }

    IEnumerator stopDash(){
        audioSource.PlayOneShot(dashSound);
        yield return new WaitForSeconds(0.4f);
        animator.ResetTrigger("dash");
        isDashing = false;
    }

    void attack(){
        if (Input.GetKey(KeyCode.E) && isGrounded){
            animator.SetBool("isShooting", true);
        } else {
            animator.SetBool("isShooting", false);
        }
    }

    void shootBullet(){
        //Shoot bullet to the right
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        if (direction == -1){
            bullet.GetComponent<SpriteRenderer>().flipX = true;
        } 

        shootPoint.GetComponent<AudioSource>().Play();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Collision with " + other.gameObject.name);
        if (other.gameObject.CompareTag("OneWayPlatform")){
            currentOneWayPlatform = other.gameObject;
            Physics2D.IgnoreCollision(collider2D, other.collider, true);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("OneWayPlatform")){
            Physics2D.IgnoreCollision(collider2D, other.collider, false);
        }    
    }

    IEnumerator DisableCollider(){
        BoxCollider2D platformCollider =  currentOneWayPlatform.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(collider2D, platformCollider, true);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(collider2D, platformCollider, false);
    }
}
