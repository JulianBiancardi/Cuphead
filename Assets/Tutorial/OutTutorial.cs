using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class DoorOutTutorial : MonoBehaviour
{
    public UnityEvent OnPlayerEnter = new UnityEvent();
    private Collider2D collider;

    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            OnPlayerEnter.Invoke();
        }
    }
}
