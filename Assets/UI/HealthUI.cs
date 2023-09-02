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
        Debug.Log("HealthUI updated");
        Health health = (Health) context;
        Debug.Log("Health: " + health.getHealth());

        int spriteIndex = Mathf.Clamp( health.getHealth(), 0, lifeSprites.Length - 1);
        image.sprite = lifeSprites[spriteIndex];
    }

}
