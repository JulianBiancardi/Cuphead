using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaunceyChanteny : MonoBehaviour
{
    public GameObject carrotSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void throwCarrot(){
        carrotSpawn.GetComponent<CarrotSpawn>().throwCarrots();
    }

    void stopThrowingCarrots(){
        carrotSpawn.GetComponent<CarrotSpawn>().stopThrowingCarrots();
    }
}
