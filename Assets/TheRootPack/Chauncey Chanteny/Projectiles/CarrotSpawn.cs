using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotSpawn : MonoBehaviour
{
    public GameObject carrotPrefab;
    public float spawnRate = 1f;

    public void throwCarrots(){
        InvokeRepeating("throwCarrot", 0f, spawnRate);
    }
    public void stopThrowingCarrots(){
        CancelInvoke("throwCarrot");
    }

    void throwCarrot(){
        //Spawn a carrot in random position from top of the screen
        Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), 6f, 0f);
        GameObject carrot = Instantiate(carrotPrefab, spawnPosition, Quaternion.identity);
    }
}
