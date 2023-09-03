using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public bool debugMode = false; 
    public GameObject player;
    public AudioClip lossSound;
    public AudioClip levelMusic;
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
        if(debugMode){
            Init();
        }
    }


    public void Init(){
        player.GetComponent<PlayerStateManager>().Init();
        salSpudder.SetActive(true);
        currentPhase = Phase.SalSpudder;
        Announcer.Instance.Ready();
        audioSource.Play();
    }

    public void NextBoss(){
        StartCoroutine(ChangePhase());
    }

    private IEnumerator ChangePhase(){
        if(currentPhase == Phase.SalSpudder){
            yield return new WaitForSeconds(3);
            currentPhase = Phase.OllieBulb;
            salSpudder.SetActive(false);
            ollieBulb.SetActive(true);
        } else if(currentPhase == Phase.OllieBulb){
            yield return new WaitForSeconds(10);
            currentPhase = Phase.ChaunceyChanteny;
            ollieBulb.SetActive(false);
            chaunceyChanteny.SetActive(true);
        } else {
            Announcer.Instance.KnockOut();
            GameObject[] carrots = GameObject.FindGameObjectsWithTag("Projectile");
            foreach (GameObject carrot in carrots){
                Destroy(carrot);
            }
            StartCoroutine(Win());
        }
    }

    public void PlayerDeath(){
        AudioManager.Instance?.Loss();
        Announcer.Instance.Loss();
        StartCoroutine(RestartLevel());
    }

    IEnumerator RestartLevel(){
        yield return new WaitForSeconds(5);
        LoaderManager loaderManager = GameObject.Find("LoaderManager").GetComponent<LoaderManager>();
        if(loaderManager != null){
            loaderManager.LoadSceneAsync(LoaderManager.Scene.TheRootPack);
        }
    }

    IEnumerator Win(){
        yield return new WaitForSeconds(5);
        LoaderManager loaderManager = GameObject.Find("LoaderManager").GetComponent<LoaderManager>();
        if(loaderManager != null){
            loaderManager.LoadSceneAsync(LoaderManager.Scene.Title);
        }
    }
}