using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tear : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip[] tearHitGroundSounds;
    public float tearLifeTime = 2f;

    void Start(){
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        Destroy(gameObject, tearLifeTime);
    }


    void OnTriggerEnter2D(Collider2D hitInfo){
        //If tear hit ground set animator hitGround trigger
        if (hitInfo.gameObject.layer == LayerMask.NameToLayer("Ground")){
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.gravityScale = 0;
            animator.SetTrigger("hitGround");
            audioSource.PlayOneShot(tearHitGroundSounds[Random.Range(0, tearHitGroundSounds.Length)]);
        }
    }

    void DestroyTear(){
        Destroy(gameObject);
    }
}
