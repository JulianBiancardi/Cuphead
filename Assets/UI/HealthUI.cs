using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour, IObserver{

    private Image image;

    [SerializeField]
    private Sprite[] lifeSprites;

    [SerializeField]
    private Sprite deathSprite;

    
    private void Start() {
        image = GetComponent<Image>();
    }

    public void update(IObservable context){
        Health health = (Health) context;
        if(health.getHealth() <= 0){
            image.sprite = deathSprite;
            return;
        }

        image.sprite = lifeSprites[health.getHealth() - 1];
    }

}
