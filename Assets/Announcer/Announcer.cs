using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Announcer : MonoBehaviour
{
    
    public static Announcer Instance { get; private set; }
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip[] readySounds;
    public AudioClip[] goSounds;
    public AudioClip knockOutSound;

    private void Awake()
    {
        Instance = this;
    }

    void Start(){
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Ready(){
        animator.SetTrigger("intro");
    }

    public void playReadySound(){
        //Select one random
        int index = Random.Range(0, readySounds.Length);
        audioSource.clip = readySounds[index];
        audioSource.Play();
    }

    public void playGoSound(){
        //Select one random
        int index = Random.Range(0, goSounds.Length);
        audioSource.clip = goSounds[index];
        audioSource.Play();
    }

    public void knockOut(){
        audioSource.clip = knockOutSound;
        audioSource.Play();
        animator.SetTrigger("knockout");
    }
}
