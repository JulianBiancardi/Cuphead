using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public int angle = 20;


    // Start is called before the first frame update
    void Start(){
        Shoot();
    }

    void Shoot(){
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, angle));
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, 0));
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, -angle));
    }
}
