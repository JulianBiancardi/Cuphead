using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperUI : MonoBehaviour
{
    public float pointsNeededForSuper = 50;
    public Image card1;
    public Image card2;
    public Image card3;
    public Image card4;
    public Image card5;
    private float heightBase;

    // Start is called before the first frame update
    void Start()
    {
        heightBase = card1.rectTransform.sizeDelta.y;
        card1.rectTransform.sizeDelta = new Vector2(card1.rectTransform.sizeDelta.x, 0);
        card2.rectTransform.sizeDelta = new Vector2(card2.rectTransform.sizeDelta.x, 0);
        card3.rectTransform.sizeDelta = new Vector2(card3.rectTransform.sizeDelta.x, 0);
        card4.rectTransform.sizeDelta = new Vector2(card4.rectTransform.sizeDelta.x, 0);
        card5.rectTransform.sizeDelta = new Vector2(card5.rectTransform.sizeDelta.x, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCards(int points){
        if(points < pointsNeededForSuper){
            float newHeight = (points / pointsNeededForSuper) * heightBase;
            card1.rectTransform.sizeDelta = new Vector2(card1.rectTransform.sizeDelta.x, newHeight);
            card2.rectTransform.sizeDelta = new Vector2(card2.rectTransform.sizeDelta.x, 0);
            card3.rectTransform.sizeDelta = new Vector2(card3.rectTransform.sizeDelta.x, 0);
            card4.rectTransform.sizeDelta = new Vector2(card4.rectTransform.sizeDelta.x, 0);
            card5.rectTransform.sizeDelta = new Vector2(card5.rectTransform.sizeDelta.x, 0);
        } else if(points < pointsNeededForSuper * 2){
            float newHeight = ((points - pointsNeededForSuper) / pointsNeededForSuper) * heightBase;
            card2.rectTransform.sizeDelta = new Vector2(card2.rectTransform.sizeDelta.x, newHeight);
            card3.rectTransform.sizeDelta = new Vector2(card3.rectTransform.sizeDelta.x, 0);
            card4.rectTransform.sizeDelta = new Vector2(card4.rectTransform.sizeDelta.x, 0);
            card5.rectTransform.sizeDelta = new Vector2(card5.rectTransform.sizeDelta.x, 0);
        } else if(points < pointsNeededForSuper * 3){
            float newHeight = ((points - pointsNeededForSuper * 2) / pointsNeededForSuper) * heightBase;
            card3.rectTransform.sizeDelta = new Vector2(card3.rectTransform.sizeDelta.x, newHeight);
            card4.rectTransform.sizeDelta = new Vector2(card4.rectTransform.sizeDelta.x, 0);
            card5.rectTransform.sizeDelta = new Vector2(card5.rectTransform.sizeDelta.x, 0);
        } else if(points < pointsNeededForSuper * 4){
            float newHeight = ((points - pointsNeededForSuper * 3) / pointsNeededForSuper) * heightBase;
            card4.rectTransform.sizeDelta = new Vector2(card4.rectTransform.sizeDelta.x, newHeight);
            card5.rectTransform.sizeDelta = new Vector2(card5.rectTransform.sizeDelta.x, 0);
        } else if(points < pointsNeededForSuper * 5){
            float newHeight = ((points - pointsNeededForSuper * 4) / pointsNeededForSuper) * heightBase;
            card5.rectTransform.sizeDelta = new Vector2(card5.rectTransform.sizeDelta.x, newHeight);
        }
    }
}
