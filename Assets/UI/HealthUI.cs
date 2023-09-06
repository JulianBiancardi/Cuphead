using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour{

    private Image image;

    [SerializeField]
    private Sprite[] lifeSprites;

    [SerializeField]
    private Sprite deathSprite;

    
    private void Start() {
        image = GetComponent<Image>();
    }

    public void update(int health){
        if(health <= 0){
            image.sprite = deathSprite;
            return;
        }

        image.sprite = lifeSprites[health - 1];
    }

}
