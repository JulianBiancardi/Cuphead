using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    public AudioClip deathSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            audioSource.PlayOneShot(deathSound);
            DestroyCarrot();
        }
    }

    public void DestroyCarrot(){
        Destroy(gameObject);
    }
}
