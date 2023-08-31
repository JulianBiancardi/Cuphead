using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            LoaderSceneManager.LoadScene(LoaderSceneManager.Scene.Tutorial);
        }
    }
}
