using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Collider2D collider2D;
    private AudioSource audioSource;
    private Animator animator;

    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D other) {
         if(other.gameObject.CompareTag("Player")){
            collider2D.enabled = false;
            audioSource.Play();
            animator.SetTrigger("collected");
            Destroy(gameObject, 1f);
         }
    }
}
