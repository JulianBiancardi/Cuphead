using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevelManager : MonoBehaviour
{
    public GameObject player;
    public AudioClip levelMusic;

    void Start()
    {
    }

    public void Init(){
        player.GetComponent<PlayerStateManager>().Init();
        AudioManager.Instance.PlayMusic(levelMusic, true);
    }
}
