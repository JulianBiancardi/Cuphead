using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoopyLeGrande : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    public float jumpForce = 10.0f;
    public float jumpUpForce = 10.0f;
    public float jumpInterval = 3.0f;
    public Transform leftLimit;
    public Transform rightLimit;
    private bool isMovingRight = false; // Start by moving to the left
    private float lastJumpTime;

    private void Start(){
        rigidbody2D = GetComponent<Rigidbody2D>();
        lastJumpTime = Time.time;
    }

    private void Update(){
        Jump();
        if ((isMovingRight && transform.position.x >= rightLimit.position.x) ||
            (!isMovingRight && transform.position.x <= leftLimit.position.x))
        {
            isMovingRight = !isMovingRight;
            Jump();
        }
    }

    private void Jump(){
        if (Time.time - lastJumpTime >= jumpInterval){
            float moveDirection = isMovingRight ? 1f : -1f;
            Vector2 jumpForceVector = new Vector2(moveDirection, jumpUpForce).normalized * jumpForce;
            rigidbody2D.AddForce(jumpForceVector, ForceMode2D.Impulse);
            lastJumpTime = Time.time;
        }
    }
}
