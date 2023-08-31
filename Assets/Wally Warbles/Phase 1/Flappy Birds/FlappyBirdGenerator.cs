using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBirdGenerator : MonoBehaviour
{
    public GameObject flappyBirdPrefab;

    // Start is called before the first frame update
    void Start(){
        //Generate a new bird every 2 seconds
        InvokeRepeating("GenerateBird", 0, 2);

    }

    void GenerateBird(){
        //Generate a new bird
        Instantiate(flappyBirdPrefab, transform.position, Quaternion.identity);
    }
}
