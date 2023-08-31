using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameObject announcerPrefab;
    public GameObject salSpudderPrefab;
    public GameObject ollieBulbPrefab;
    public GameObject carrotPrefab;
    private int currentPhase = 1;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void enemyDefeated(){
        if(currentPhase == 1){
            salSpudderPrefab.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1f);
            Destroy(salSpudderPrefab, 2f);
            Invoke("phase2", 2f);
            currentPhase++;
        } else if (currentPhase == 2){
            Debug.Log("OllieBulb defeated");

            GameObject children = ollieBulbPrefab.transform.GetChild(0).gameObject;
            Rigidbody2D rb = children.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, -1f);
            Destroy(ollieBulbPrefab, 2f);
            Invoke("phase3", 2f);
            currentPhase++;
        } else if (currentPhase == 3){
            Debug.Log("Carrot defeated");
            announcerPrefab.GetComponent<Announcer>().knockOut();
        }
    }


    public void phase2(){
        //Activate OllieBulbs
        ollieBulbPrefab.SetActive(true);
    }

    public void phase3(){
        //Activate Carrot
        carrotPrefab.SetActive(true);
    }
}
