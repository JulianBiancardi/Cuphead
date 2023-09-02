using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject superBulletPrefab;
    public Transform bulletSpawn;
    public AudioClip startShootClip;
    public AudioClip shootClip;
    public AudioClip superClip;
    public float bulletSpeed = 10f;
    public float bulletLife = 2f;
    public float fireRate = 0.5f;
    public float superSpeed = 20f;
    private float nextFire = 0.0f;
    private AudioSource audioSource;
    private bool initialShoot = true;
    private int superCount = 0;
    private int maxSuperCount = 5;
    private int pointsNeededForSuper = 50;
    private int points = 0;
    private int maxPoints;

    public UnityEvent<int> OnPointsChange;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        maxPoints = pointsNeededForSuper * maxSuperCount;
    }


    public bool CanEX(){
        return superCount > 0;
    }

    public void Shoot(Quaternion targetRotation)
    {
        if(initialShoot){
            audioSource.PlayOneShot(startShootClip);
            initialShoot = false;
        }

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
        initialShoot = true;
        audioSource.Stop();
    }

    public void Super(Quaternion targetRotation){
        if(!CanEX()){
            return;
        }
        superCount--;
        points -= pointsNeededForSuper;
        OnPointsChange.Invoke(points);
        audioSource.PlayOneShot(superClip);
        GameObject bullet = Instantiate(superBulletPrefab, bulletSpawn.position, targetRotation);
        Rigidbody2D bulletRigidbody2D = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody2D.velocity = bullet.transform.right * superSpeed;
        Destroy(bullet, bulletLife);
    }

    public void AddPoint(int amount){
        points += amount;
        if(points > maxPoints){
            points = maxPoints;
        } else {
            superCount = points / pointsNeededForSuper;
            if(superCount > maxSuperCount){
                superCount = maxSuperCount;
            }
            OnPointsChange.Invoke(points);
        }
    }
}
