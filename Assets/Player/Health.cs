using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IObservable{
    private AudioSource audioSource;
    private Animator animator;
    
    [SerializeField]
    public List<IObserver> healtObservers = new List<IObserver>();

    public int health = 3;
    public GameObject uiObserver;
    public AudioClip damageSound;

    public UnityEvent OnPlayerDeath = new UnityEvent(); 

    public int getHealth(){
        return health;
    }

    public void addObserver(IObserver observer){
        healtObservers.Add(observer);
    }

    public void removeObserver(IObserver observer){
    }

    public void notifyObservers(IObservable context){
        foreach (IObserver observer in healtObservers){
            observer.update(context);
        }
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        addObserver(uiObserver.GetComponent<IObserver>());
        notifyObservers(this);
    }

    public void takeDamage(){
        health -= 1;
        audioSource.PlayOneShot(damageSound);
        animator.SetTrigger("hit");
        CameraShake.Instance.ShakeCamera(1f, 0.2f);
        if (health <= 0){
            animator.SetBool("isDead", true);
            OnPlayerDeath.Invoke();
        }
        notifyObservers(this);
    }

    void OnTriggerEnter2D(Collider2D other) {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null){
            takeDamage();
        }
    }

    public void finishHit(){
        animator.ResetTrigger("hit");
    }


}
