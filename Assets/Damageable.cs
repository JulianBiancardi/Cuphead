using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Damageable: MonoBehaviour
{
    public int health = 1;
    public Collider2D hitbox;
    
    public UnityEvent OnDamage = new UnityEvent();
    public UnityEvent OnDeath = new UnityEvent();

    private void TakeDamage(int damage){
        health -= damage;
        if(health <= 0){
            OnDeath.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(damageDealer != null){
            OnDamage.Invoke();
            TakeDamage(damageDealer.getDamage());
        }
    }
}
