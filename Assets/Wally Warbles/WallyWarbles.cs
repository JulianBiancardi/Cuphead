using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallyWarbles : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Transform transform;
    public float speed = 10.0f;

    // Start is called before the first frame update
    void Start(){
        rigidbody2D = GetComponent<Rigidbody2D>(); 
        transform = GetComponent<Transform>(); 
        rigidbody2D.velocity = new Vector2(0, speed);
    }


    void Update() {
        //If wally is at the bottom of the screen, move him up
        if (transform.position.y <= -2.5){
            rigidbody2D.velocity = new Vector2(0, speed);
        }
        //If wally is at the top of the screen, move him down
        else if (transform.position.y >= 2.5){
            rigidbody2D.velocity = new Vector2(0, -speed);
        }
    }
}
