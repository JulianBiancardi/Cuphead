using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LoaderSceneManager 
{

    public enum Scene
    {
        Title,
        Tutorial,
        Loading
    }

    private static Action onLoaderCallback;

    public static void LoadScene(Scene scene){
        Animator irisAnimator = GameObject.Find("Iris").GetComponent<Animator>();
        irisAnimator.SetTrigger("outro");
        onLoaderCallback = () => {
            SceneManager.LoadScene(scene.ToString());
        };
        //StartCoroutine(LoadSceneAsync());
    }
/*
    IEnumerator LoadSceneAsync(){
        Animator irisAnimator = GameObject.Find("Iris").GetComponent<Animator>();
        irisAnimator.SetTrigger("outro");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(Scene.Loading.ToString());
    }*/

    public static void LoaderCallBack(){
        if(onLoaderCallback != null){
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
