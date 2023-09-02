using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{ 
    public GameObject salSpudder;
    public GameObject ollieBulb;
    public GameObject chaunceyChanteny;
    public UnityEvent OnWin;
    private AudioSource audioSource;

    private enum Phase {SalSpudder, OllieBulb, ChaunceyChanteny};
    private Phase currentPhase;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        salSpudder.SetActive(false);
        ollieBulb.SetActive(false);
        chaunceyChanteny.SetActive(false);
        currentPhase = Phase.SalSpudder;
    }


    public void Init(){
        salSpudder.SetActive(true);
        currentPhase = Phase.SalSpudder;
        Announcer.Instance.Ready();
        audioSource.Play();
    }

    public void NextBoss(){
        if(currentPhase == Phase.SalSpudder){
            currentPhase = Phase.OllieBulb;
            salSpudder.SetActive(false);
            ollieBulb.SetActive(true);
        } else if(currentPhase == Phase.OllieBulb){
            currentPhase = Phase.ChaunceyChanteny;
            ollieBulb.SetActive(false);
            chaunceyChanteny.SetActive(true);
        } else {
            Announcer.Instance.KnockOut();
            OnWin.Invoke();
        }
    }

    public void PlayerDeath(){
        //reset the level
        //fin loader manager object
        LoaderManager loaderManager = GameObject.Find("LoaderManager").GetComponent<LoaderManager>();
        if(loaderManager != null){
            loaderManager.LoadSceneAsync(LoaderManager.Scene.TheRootPack);
        }
    }
}