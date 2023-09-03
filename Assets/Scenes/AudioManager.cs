using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; private set;}
    public AudioMixer masterMixer;

    void Awake(){
        if(Instance == null){
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip, bool loop){
    }

    public void PlaySFX(AudioClip clip){
    }

    public void StopMusic(){
    }

    public void StopSFX(){
    }

    IEnumerator FadeOut(string variable, float FadeTime) {
        masterMixer.GetFloat(variable, out float startVolume);

        while (startVolume > -80) {
            startVolume -= 80 * Time.deltaTime / FadeTime;
            masterMixer.SetFloat(variable, startVolume);
            yield return null;
        }
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
        //StartCoroutine(FadeOut("musicVolume", FadeTime));
    }

    public void FadeInMusic(float FadeTime){
    }

    public void ResetVolume(){
    }

    public void AdjustMusicPitch(){
        masterMixer.SetFloat("musicPitch", 1f);
    }

    public void Loss(){
        masterMixer.SetFloat("musicPitch", 0.7f);
    }
}
