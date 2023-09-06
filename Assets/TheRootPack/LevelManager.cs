using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public bool debugMode = false; 
    public GameObject player;
    public GameObject UI;
    public AudioClip lossSound;
    public AudioClip levelMusic;
    public GameObject salSpudder;
    public GameObject ollieBulb;
    public GameObject chaunceyChanteny;
    public BossDeathCard[] bossDeathCards;
    
    public GameObject deathCard;
    public UnityEvent OnWin;
    private AudioSource audioSource;
    private float totalHealth = 0f;
    private float damageDeal = 0f;


    private enum Phase {SalSpudder, OllieBulb, ChaunceyChanteny};
    private Phase currentPhase;
    private BossDeathCard currentBossDeathCard;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UI.SetActive(false);
        salSpudder.SetActive(false);
        ollieBulb.SetActive(false);
        chaunceyChanteny.SetActive(false);
        totalHealth = salSpudder.GetComponent<EnemyHealth>().GetHealth() + ollieBulb.GetComponent<EnemyHealth>().GetHealth() + chaunceyChanteny.GetComponent<EnemyHealth>().GetHealth();
        deathCard.GetComponent<DeathCard>().Init(totalHealth, new List<float>{salSpudder.GetComponent<EnemyHealth>().GetHealth(), salSpudder.GetComponent<EnemyHealth>().GetHealth() + ollieBulb.GetComponent<EnemyHealth>().GetHealth()});
        deathCard.SetActive(false);
        if(debugMode){
            Init();
        }
    }


    public void Init(){
        player.GetComponent<PlayerStateManager>().Init();
        UI.SetActive(true);
        salSpudder.SetActive(true);
        currentPhase = Phase.SalSpudder;
        currentBossDeathCard = bossDeathCards[0];
        Announcer.Instance.Ready();
        audioSource.Play();
    }

    public void NextBoss(){
        StartCoroutine(ChangePhase());
    }

    private IEnumerator ChangePhase(){
        if(currentPhase == Phase.SalSpudder){
            damageDeal = salSpudder.GetComponent<EnemyHealth>().GetHealth();
            yield return new WaitForSeconds(3);
            currentPhase = Phase.OllieBulb;
            currentBossDeathCard = bossDeathCards[1];
            salSpudder.SetActive(false);
            ollieBulb.SetActive(true);
        } else if(currentPhase == Phase.OllieBulb){
            damageDeal += ollieBulb.GetComponent<EnemyHealth>().GetHealth();
            yield return new WaitForSeconds(3);
            currentPhase = Phase.ChaunceyChanteny;
            currentBossDeathCard = bossDeathCards[2];
            ollieBulb.SetActive(false);
            chaunceyChanteny.SetActive(true);
        } else {
            Announcer.Instance.KnockOut();
            StopCarrots();
            StartCoroutine(Win());
        }
    }

    private void StopCarrots(){
        GameObject[] carrots = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject carrot in carrots){
            carrot.GetComponent<FollowPlayer>().stopFollow();
        }
    }

    public void PlayerDeath(){
        AudioManager.Instance?.Loss();
        if(currentPhase == Phase.SalSpudder){
            damageDeal = salSpudder.GetComponent<EnemyHealth>().GetDamageTaken();
        } else if(currentPhase == Phase.OllieBulb){
            damageDeal += ollieBulb.GetComponent<EnemyHealth>().GetDamageTaken();
        } else {
            damageDeal += chaunceyChanteny.GetComponent<EnemyHealth>().GetDamageTaken();
        }
        deathCard.GetComponent<DeathCard>().SetBossDeathCard(currentBossDeathCard, damageDeal / totalHealth);
        Announcer.Instance.Loss();
        StopCarrots();
    }

    public void OnRetryClick(){
        StartCoroutine(RestartLevel());
    }

    public void OnQuitClick(){
        LoaderManager loaderManager = GameObject.Find("LoaderManager").GetComponent<LoaderManager>();
        if(loaderManager != null){
            loaderManager.LoadSceneAsync(LoaderManager.Scene.Title);
        }
    }

    IEnumerator RestartLevel(){
        LoaderManager loaderManager = GameObject.Find("LoaderManager").GetComponent<LoaderManager>();
        if(loaderManager != null){
            loaderManager.LoadSceneAsync(LoaderManager.Scene.TheRootPack);
        }
        yield return null;
    }

    IEnumerator Win(){
        yield return new WaitForSeconds(5);
        LoaderManager loaderManager = GameObject.Find("LoaderManager").GetComponent<LoaderManager>();
        if(loaderManager != null){
            loaderManager.LoadSceneAsync(LoaderManager.Scene.Title);
        }
    }
}