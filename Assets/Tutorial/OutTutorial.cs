using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOutTutorial : MonoBehaviour
{
    private Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            LoaderManager loaderManager = GameObject.Find("LoaderManager").GetComponent<LoaderManager>();
            if(loaderManager){
                Debug.Log("LOADER MANAGER: " + loaderManager);
                collider.enabled = false;
                loaderManager.LoadSceneAsync(LoaderManager.Scene.TheRootPack);
            }
        }

    }
}
