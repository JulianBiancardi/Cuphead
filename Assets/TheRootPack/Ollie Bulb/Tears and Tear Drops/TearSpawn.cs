using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearSpawn : MonoBehaviour
{
    public GameObject tearPrefab;
    public float tearsOffset = 0.1f;
    
    private Vector3[] possibleSpawnPoints = new Vector3[4];
    private Vector3 lastSpawnPoint;

    public void throwTears(float spawnRate){
        possibleSpawnPoints[0] = transform.position + new Vector3(tearsOffset, 0, 0);
        possibleSpawnPoints[1] = transform.position + new Vector3(-tearsOffset, 0, 0);
        possibleSpawnPoints[2] = transform.position + new Vector3(tearsOffset * 2, 0, 0);
        possibleSpawnPoints[3] = transform.position + new Vector3(-tearsOffset * 2, 0, 0);
        lastSpawnPoint = transform.position;
        InvokeRepeating("throwTear", 0.0f, spawnRate);
    }

    public void stopThrowingTears(){
        CancelInvoke("throwTear");
    }

    void throwTear(){
        //Instantiate a tear at a random spawn point and ignore lastSpawnpoint
        Vector3 spawnPosition = possibleSpawnPoints[Random.Range(0, possibleSpawnPoints.Length)];
        while(spawnPosition == lastSpawnPoint){
            spawnPosition = possibleSpawnPoints[Random.Range(0, possibleSpawnPoints.Length)];
        }
        Instantiate(tearPrefab, spawnPosition, Quaternion.identity);
        lastSpawnPoint = spawnPosition;
    }

}
