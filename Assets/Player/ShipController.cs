using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour{

    public Animator animator;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update() {
        // Move the player left and right
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * 5);

        // Move the player up and down
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * verticalInput * Time.deltaTime * 5);

        //Change the direction base on the input
        animator.SetFloat("direction", verticalInput);
        
    }
}
