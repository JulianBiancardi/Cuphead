using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private AudioSource audioSource;

    void Start(){
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D hitInfo){
        Debug.Log(hitInfo.name);
        //Only set trigger if other has enemy script
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null){
            audioSource.Play();
            animator.SetTrigger("hit");
        }
    }

    void DestroyBullet(){
        Destroy(gameObject);
    }
}
