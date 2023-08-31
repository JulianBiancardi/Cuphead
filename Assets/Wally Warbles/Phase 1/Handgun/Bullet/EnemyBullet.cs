using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 20f;
    public int direction = -1;
    public Rigidbody2D rb;

    void Start(){
        rb.velocity = transform.right * speed * direction;
    }

    void OnTriggerEnter2D(Collider2D hitInfo){
        Debug.Log(hitInfo.name);
    }
}
