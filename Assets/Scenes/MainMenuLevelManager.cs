using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLevelManager : MonoBehaviour
{
    public GameObject menu;
    void Start()
    {
        if(menu != null){
            menu.SetActive(false);
        }
    }

    public void Init(){
        if(menu != null){
            menu.SetActive(true);
        }
    }

    public void OnStartClick(){
        LoaderManager loaderManager = GameObject.Find("LoaderManager").GetComponent<LoaderManager>();
        if(loaderManager != null){
            loaderManager.LoadSceneAsync(LoaderManager.Scene.TheRootPack);
            if(menu != null){
                menu.SetActive(false);
            }
        }
    }

    public void OnTutorialClick(){
        LoaderManager loaderManager = GameObject.Find("LoaderManager").GetComponent<LoaderManager>();
        if(loaderManager != null){
            loaderManager.LoadSceneAsync(LoaderManager.Scene.Tutorial);
        }
        if(menu != null){
            menu.SetActive(false);
        }
    }
}
