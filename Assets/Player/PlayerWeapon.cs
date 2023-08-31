using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject superBulletPrefab;
    public Transform bulletSpawn;
    public AudioClip shootClip;
    public float bulletSpeed = 10f;
    public float bulletLife = 2f;
    public float fireRate = 0.5f;
    public float superSpeed = 20f;
    private float nextFire = 0.0f;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void Shoot(Quaternion targetRotation)
    {
        if(Time.time > nextFire){
            nextFire = Time.time + fireRate;
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, targetRotation);
            Rigidbody2D bulletRigidbody2D = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody2D.velocity = bullet.transform.right * bulletSpeed;
            Destroy(bullet, bulletLife);
            if(!audioSource.isPlaying){
                audioSource.PlayOneShot(shootClip);
            }
        }
    }

    public void StopShooting()
    {
        nextFire = 0.0f;
        audioSource.Stop();
    }

    public void Super(Quaternion targetRotation){
        Debug.Log("Super");
        GameObject bullet = Instantiate(superBulletPrefab, bulletSpawn.position, targetRotation);
        Rigidbody2D bulletRigidbody2D = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody2D.velocity = bullet.transform.right * superSpeed;
        Destroy(bullet, bulletLife);
    }

}
