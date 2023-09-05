using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeathCard : MonoBehaviour
{
    public static DeathCard Instance { get; private set; }
    public Image bossImage;
    public TextMeshProUGUI phrase;
    public BossDeathCard bossDeathCard;
    public Slider bossHealthSlider;
    public GameObject phaseSpot;
    public GameObject phaseContainer;

    private float damageDeal = 0f;

    void Start()
    {
    }

    public void Init(float totalHealth, List<float> healthDelimeters){
        RectTransform phaseContainerRectTransform = phaseContainer.GetComponent<RectTransform>();
        float phaseContainerWidth = phaseContainerRectTransform.rect.width;

        foreach(float healthDelimeter in healthDelimeters){
            GameObject phaseSpot = Instantiate(this.phaseSpot, phaseContainer.transform.position, Quaternion.identity);
            phaseSpot.transform.SetParent(phaseContainer.transform);
            RectTransform phaseSpotRectTransform = phaseSpot.GetComponent<RectTransform>();
            phaseSpotRectTransform.anchoredPosition = new Vector2((healthDelimeter / totalHealth) * phaseContainerWidth, 0);
        }
    }

    public void SetBossDeathCard(BossDeathCard bossDeathCard, float damageDeal)
    {
        this.bossDeathCard = bossDeathCard;
        bossImage.sprite = bossDeathCard.bossImage;
        phrase.text = "\"" + bossDeathCard.phrase + "\n" + bossDeathCard.secondPhrase + "\"";
        bossHealthSlider.value = 0;
        this.damageDeal = damageDeal;
    }

    public void StartAnim(){
        StartCoroutine(SetDamageDeal());
    }

    private IEnumerator SetDamageDeal()
    {
        float step = 0.01f;
        while(bossHealthSlider.value < damageDeal)
        {
            bossHealthSlider.value += step;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
