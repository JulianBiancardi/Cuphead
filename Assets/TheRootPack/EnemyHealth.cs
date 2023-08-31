using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public int health = 10;
    public GameObject bossManager = null;
    public GameObject spriteObject;
    public float flashTime = 0.1f;
    public float flashIntensity = 0.1f;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public Collider2D collider;
    public AudioClip deathSound;
    private AudioSource audioSource;
    public UnityEvent OnEnemyDeath = new UnityEvent();
    public GameObject boosDeathEffect;

    void Start(){
        spriteRenderer = spriteObject.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other){
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(damageDealer != null){
            takeDamage(damageDealer.getDamage());
        }
    }

    private void takeDamage(int amount){
        health -= amount;
        StartCoroutine(FlashWhite());
        if(health <= 0){
            collider.enabled = false;
            animator.SetTrigger("isDead");
            if(audioSource != null){
                audioSource.PlayOneShot(deathSound);
            }
            if(boosDeathEffect != null){
                ParticleSystem lightning = boosDeathEffect.transform.GetChild(0).GetComponent<ParticleSystem>();
                ParticleSystem stars = boosDeathEffect.transform.GetChild(1).GetComponent<ParticleSystem>();
                lightning.Play();
                stars.Play();
            }
        }
    }
    
    private IEnumerator FlashWhite(){
        spriteRenderer.material.SetFloat("_FlashIntensity", flashIntensity);
        yield return new WaitForSeconds(flashTime);
        spriteRenderer.material.SetFloat("_FlashIntensity", 0f);
    }

    public void enemyDeath(){
        OnEnemyDeath.Invoke();
    }
}
