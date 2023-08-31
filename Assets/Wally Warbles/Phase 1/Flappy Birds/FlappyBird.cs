using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBird : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public Animator animator;
    public Collider2D collider;
    public AudioSource audio;

    [SerializeField]
    public float frequency = 20f;
    public float magnitude = 0.5f;

    Vector3 position, localScale;
    public int speed = 2;
    public bool isDead = false;

    void Start(){
        position = transform.position;
        localScale = transform.localScale;
    }

    private void Update() {
        //Move the bird from right to the left in a sine wave
        position -= transform.right * Time.deltaTime * speed;
        transform.position = position + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
        
    }

    void Destroy(){
        Destroy(gameObject);
    }

    //Detect bullet
    void OnTriggerEnter2D(Collider2D other){
        Debug.Log("TRIGGEREEEEEEEEEEEEEED");
        Debug.Log(other.gameObject.tag);

        if(other.gameObject.tag == "Bullet"){
            Debug.Log("Triggered");
            isDead = true;
            animator.SetBool("isDead", true);

            //Deactivate the collider
            collider.enabled = false;

            //Play death sound
            audio.Play();
        }
    }
}
