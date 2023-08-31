using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallBack : MonoBehaviour
{
    private bool isFirstUpdate = true;
    void Update()
    {
        if(isFirstUpdate){
            isFirstUpdate = false;
            LoaderSceneManager.LoaderCallBack();
        }
    }
}
