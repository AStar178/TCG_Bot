using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ImageBar : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textPer;
    [SerializeField]
    private TMP_Text textAmou;
    [SerializeField]
    private Image image;

    [SerializeField]
    [Range(0,1)]
    private float NewFillValue = 1;

    public void Set(float current, float max, Color ImageColor)
    {
        // MaxHealthValue* (CurrentHealthValue/100) // Max health Percentage Formula
        NewFillValue = current / max;
        textAmou.text = $"{(int)current} / {(int)max}";
        int c = (int)((current / max) * 100);
        textPer.text = $"{c}%";
        image.color = ImageColor;
    }

    public void Update()
    {
        image.fillAmount = Mathf.Lerp(image.fillAmount, NewFillValue, 5f * Time.deltaTime);
    }
}
