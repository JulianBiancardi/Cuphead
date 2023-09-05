using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonTextColorChange : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private TextMeshProUGUI text;
    private Color32 originalColor;
    public Color32 selectedColor;

    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        originalColor = text.color;
    }

    public void OnSelect(BaseEventData eventData)
    {
        text.color = selectedColor;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        text.color = originalColor;
    }
}
