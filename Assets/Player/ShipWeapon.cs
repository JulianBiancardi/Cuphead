using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update(){
        if(Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }

    void Shoot(){
        // Shooting logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
