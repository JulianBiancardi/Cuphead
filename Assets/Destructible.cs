using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible :Damageable
{
    public AudioSource audioSource;
    public AudioClip deathSound;

    void Start()
    {
        OnDeath.AddListener(Destroy);
        audioSource = GetComponent<AudioSource>();
    }


    void Destroy(){
        audioSource.PlayOneShot(deathSound);
        Destroy(gameObject);
    }
}
