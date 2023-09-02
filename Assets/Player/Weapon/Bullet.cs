using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public bool isEXBullet = false;
    private Rigidbody2D rigidbody2D;
    private Collider2D collider2D;
    private Animator animator;
    private AudioSource audioSource;

    void Start(){
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D hitInfo){
        EnemyHealth enemy = hitInfo.GetComponent<EnemyHealth>();
        if (enemy != null){
            Debug.Log(hitInfo.name);
            enemy.TakeDamage(damage);
            rigidbody2D.velocity = Vector2.zero;
            collider2D.enabled = false;
            audioSource.Play();
            animator.SetTrigger("hit");

            GameObject player = GameObject.Find("Player");
            if(player != null){
                PlayerWeapon weapon = player.GetComponent<PlayerWeapon>();
                if(weapon != null && !isEXBullet){
                    weapon.AddPoint(damage);
                }
            }
        }
    }

    void DestroyBullet(){
        Destroy(gameObject);
    }
}
