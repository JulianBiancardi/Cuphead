using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour, IObserver{

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite[] lifeSprites;

    [SerializeField]
    private Sprite deathSprite;

    
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void update(IObservable context){
        Debug.Log("HealthUI updated");
        Health health = (Health) context;
        Debug.Log("Health: " + health.getHealth());

        int spriteIndex = Mathf.Clamp( health.getHealth(), 0, lifeSprites.Length - 1);
        spriteRenderer.sprite = lifeSprites[spriteIndex];
    }

}
