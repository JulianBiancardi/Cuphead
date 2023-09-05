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
    public AudioClip knockOutBellSound;
    public GameObject deathCard;

    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        animator.enabled = false;
    }

    void Start(){
    }

    public void Ready(){
        animator.enabled = true;
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

    public void KnockOut(){
        audioSource.PlayOneShot(knockOutSound);
        audioSource.PlayOneShot(knockOutBellSound);
        animator.SetTrigger("knockout");
    }

    public void Loss(){
        animator.SetTrigger("loss");
    }

    public void OnLossAnimEnd(){
        deathCard.SetActive(true);
        deathCard.GetComponent<DeathCard>().StartAnim();
    }
}
