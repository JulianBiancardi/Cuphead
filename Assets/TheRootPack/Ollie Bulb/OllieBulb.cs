using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OllieBulb : MonoBehaviour
{
    public GameObject tearSpawn1;
    public GameObject tearSpawn2;
    public float tearSpawnRate = 0.5f;
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip introSound;
    public AudioClip crySound;
    public AudioClip deathSound;

    void Start(){
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void playIntroSound(){
        audioSource.PlayOneShot(introSound);
    }
    public void playCrySound(){
        audioSource.PlayOneShot(crySound);
    }
    public void playDeathSound(){
        audioSource.PlayOneShot(deathSound);
    }

    void starCry(){
        audioSource.PlayOneShot(crySound);
        animator.SetBool("cry", true);
    }

    void throwTears(){
        tearSpawn1.GetComponent<TearSpawn>().throwTears(tearSpawnRate);
        tearSpawn2.GetComponent<TearSpawn>().throwTears(tearSpawnRate);
    }

    public void stopthrowTears(){
        tearSpawn1.GetComponent<TearSpawn>().stopThrowingTears();
        tearSpawn2.GetComponent<TearSpawn>().stopThrowingTears();
    }
}
