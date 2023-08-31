using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOutTutorial : MonoBehaviour
{
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            Debug.Log("PLAYER ENTERED DOOR");
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

    }
}
