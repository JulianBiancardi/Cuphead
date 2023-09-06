using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour{
    public Collider2D collider2D;
    private AudioSource audioSource;
    private Animator animator;

    public int health = 3;
    public float invulnerabilityTime = 6f;
    public AudioClip damageSound;
    public AudioClip deathSound;

    public UnityEvent<int> OnPlayerHealthChange = new UnityEvent<int>();
    public UnityEvent OnPlayerDeath = new UnityEvent(); 

    public int getHealth(){
        return health;
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        OnPlayerHealthChange.Invoke(health);
    }

    public void takeDamage(){
        health -= 1;
        OnPlayerHealthChange.Invoke(health);
        audioSource.PlayOneShot(damageSound);
        animator.SetTrigger("hit");
        CameraShake.Instance.ShakeCamera(1f, 0.2f);
        if (health <= 0){
            collider2D.enabled = false;
            animator.SetTrigger("death");
            audioSource.PlayOneShot(deathSound);
            OnPlayerDeath.Invoke();
        } else {
            Invulnerability();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null && health > 0){
            takeDamage();
        }
    }

    public void finishHit(){
        animator.ResetTrigger("hit");
    }

    void Invulnerability(){
        StartCoroutine(BlinkSprite());
        StartCoroutine(StartInvulnerability());
    }

    IEnumerator StartInvulnerability(){
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        yield return new WaitForSeconds(invulnerabilityTime);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
    }

    IEnumerator BlinkSprite(){
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        for (int i = 0; i < 6; i++){
            spriteRenderer.color = new Color(1f,1f,1f,0.5f);
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = new Color(1f,1f,1f,1f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
