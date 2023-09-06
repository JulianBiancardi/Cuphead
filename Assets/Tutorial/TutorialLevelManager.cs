using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevelManager : MonoBehaviour
{
    public GameObject player;
    public AudioClip levelMusic;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Init(){
        player.GetComponent<PlayerStateManager>().Init();
        audioSource.clip = levelMusic;
        audioSource.Play();
    }

    public void FinishTutorial(){
        LoaderManager loaderManager = GameObject.Find("LoaderManager").GetComponent<LoaderManager>();
        if(loaderManager){
            loaderManager.LoadSceneAsync(LoaderManager.Scene.MainMenu);
        }
    }
}
