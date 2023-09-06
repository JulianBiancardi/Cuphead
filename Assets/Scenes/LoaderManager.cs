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
    public AudioClip titleMusic;
    public AudioClip levelMusic;

    public enum Scene
    {
        Title,
        MainMenu,
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
        if(Input.anyKey && currentScene == Scene.Title && !isLoading){
           LoadSceneAsync(Scene.MainMenu);
        }
    }


    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    public void LoadSceneAsync(Scene scene){
        animator.SetTrigger("outro");
        AudioManager.Instance.FadeOutMusic(0.5f);
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

    public void FinishOutro(){
        scenesLoading.Add(SceneManager.UnloadSceneAsync(currentScene.ToString()));
        loadingScreen.SetActive(true);
        scenesLoading.Add(SceneManager.LoadSceneAsync(nextScene.ToString(), LoadSceneMode.Additive));
        StartCoroutine(GetSceneLoadProgress());
    }

    IEnumerator FinishLoadingLevel(){
        yield return new WaitForSeconds(3f);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextScene.ToString()));
        currentScene = nextScene;
        isLoading = false;
        loadingScreen.SetActive(false);
        animator.SetTrigger("intro");
        AudioManager.Instance.AdjustMusicPitch();
        if(currentScene == Scene.Title){
            AudioManager.Instance.PlayMusic(titleMusic, false);
        } else {
            //Find the level manager and init the level
            GameObject levelManager = GameObject.Find("LevelManager");
            if(levelManager != null){
                levelManager.SendMessage("Init");
            }
        }
    }
}
