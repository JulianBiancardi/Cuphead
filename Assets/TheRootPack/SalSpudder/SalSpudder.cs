using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalSpudder : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    private AudioSource audioSource;
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    public AudioClip attackSound;
    public AudioClip deathSound;
    public float projectileSpeed = 10.0f;
    public float projectileLifetime = 3.0f;
    public float projectileInterval;

    void Start(){
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        Invoke("attack", projectileInterval);
    }

    private void attack(){
        animator.SetTrigger("attack");
    }

    private void throwProjectile(){
        audioSource.PlayOneShot(attackSound);
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D rigidbody2D = projectile.GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = - transform.right * projectileSpeed;
        Destroy(projectile, projectileLifetime);
    }

    private void finishAttack(){
        animator.ResetTrigger("attack");
        Invoke("attack", projectileInterval);
    }

    public void playDeathSound(){
        audioSource.PlayOneShot(deathSound);
    }

}
