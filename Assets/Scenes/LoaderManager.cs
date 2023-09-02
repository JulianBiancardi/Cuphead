using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderManager : MonoBehaviour
{
    private Animator animator; 
    private Scene currentScene;
    private Scene nextScene;
    private bool isLoading = false;
    public GameObject loadingScreen;
    public AudioSource audioSource;

    public enum Scene
    {
        Title,
        Tutorial,
        Loading,
        Win,
        TheRootPack
    }

    void Awake() {
        currentScene = Scene.Title;
        loadingScreen.SetActive(false);
        SceneManager.LoadSceneAsync(Scene.Title.ToString(), LoadSceneMode.Additive);
    }

    void Start() {
        animator = GetComponent<Animator>();
        animator.SetTrigger("intro");
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Space) && currentScene == Scene.Title && !isLoading){
           LoadSceneAsync(Scene.TheRootPack);
        }
    }


    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    public void LoadSceneAsync(Scene scene){
        animator.SetTrigger("outro");
        StartCoroutine(FadeOut(audioSource, 0.5f));
        nextScene = scene;
        isLoading = true;
    }
    IEnumerator GetSceneLoadProgress(){
        for(int i = 0; i < scenesLoading.Count; i++){
            if(scenesLoading[i] != null){
                while(!scenesLoading[i].isDone){
                    yield return null;
                }
            }
        }

        scenesLoading.Clear();
        StartCoroutine(FinishLoadingLevel());
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

    public void FinishOutro(){
        scenesLoading.Add(SceneManager.UnloadSceneAsync(currentScene.ToString()));
        loadingScreen.SetActive(true);
        scenesLoading.Add(SceneManager.LoadSceneAsync(nextScene.ToString(), LoadSceneMode.Additive));
        StartCoroutine(GetSceneLoadProgress());
    }

    IEnumerator FinishLoadingLevel(){
        yield return new WaitForSeconds(5f);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextScene.ToString()));
        currentScene = nextScene;
        isLoading = false;
        loadingScreen.SetActive(false);
        animator.SetTrigger("intro");
        audioSource.volume = 1f;
        if(currentScene == Scene.TheRootPack){
            //Find the level manager and init the level
            LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            if(levelManager != null){
                levelManager.Init();
            }
        }
    }
}
