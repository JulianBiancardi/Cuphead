using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float speed = 5f;
    public float playerBodyOffset = 0.5f;
    private Rigidbody2D rigidbody2D;
    private Collider2D collider2D;
    public bool stop = false;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(stop){
            return;
        }

        // Get the player's transform
        Vector3 playerPosition = GameObject.Find("Player").transform.position + new Vector3(0, playerBodyOffset, 0); 
        Vector3 direction = playerPosition - transform.position;
        
        //Move the enemy to the player position with certain velocity
        rigidbody2D.velocity = direction.normalized * speed;

        //Rotate enemy to face the player acording to sight
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rigidbody2D.rotation = 90 + angle;

    }

    public void stopFollow(){
        stop = true;
        rigidbody2D.velocity = Vector2.zero;
        collider2D.enabled = false;
    }
}
