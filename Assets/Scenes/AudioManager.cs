using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; private set;}
    public AudioSource musicSource;
    public AudioSource sfxSource;

    void Awake(){
        if(Instance == null){
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip, bool loop){
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip){
        sfxSource.PlayOneShot(clip);
    }

    public void StopMusic(){
        musicSource.Stop();
    }

    public void StopSFX(){
        sfxSource.Stop();
    }

    IEnumerator FadeOut(AudioSource audioSource, float FadeTime) {
        float startVolume = audioSource.volume;
 
        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
 
            yield return null;
        }
 
        audioSource.Stop ();
        audioSource.volume = startVolume;
    }

    IEnumerator FadeIn(AudioSource audioSource, float FadeTime) {
        audioSource.pitch = 1;
        float startVolume = 0.1f;
 
        audioSource.volume = 0;
        audioSource.Play();
 
        while (audioSource.volume < 1) {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;
 
            yield return null;
        }
 
        audioSource.volume = 1;
    }

    public void FadeOutMusic(float FadeTime){
        StartCoroutine(FadeOut(musicSource, FadeTime));
    }

    public void FadeInMusic(float FadeTime){
        StartCoroutine(FadeIn(musicSource, FadeTime));
    }

    public void ResetVolume(){
        musicSource.volume = 1;
    }

    public void AdjustPitch(AudioSource audioSource, float pitch){
        audioSource.pitch = pitch;
    }

    public void Loss(){
        AdjustPitch(musicSource, 0.8f);
    }
}
