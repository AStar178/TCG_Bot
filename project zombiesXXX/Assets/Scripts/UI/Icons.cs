using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Icons : MonoBehaviour
{
    [SerializeField]
    Image IconImage;
    [SerializeField]
    Image CooldownImage;

    [SerializeField]
    TMP_Text CooldownText;

    private float NewValue;

    public void Awake()
    {
        SetIconImage(null);
    }

    public void Start()
    {
        CooldownText.text = "";
    }

    public void SetIconImage(Sprite Icon)
    {
        IconImage.sprite = Icon;

        if (Icon == null)
            IconImage.color = Color.black;
        if (Icon != null)
            IconImage.color = Color.white;
    }

    public void SetCooldown(float CurrentTime, float CooldownSet)
    {
        NewValue = CurrentTime / CooldownSet;
        if (CurrentTime == CooldownSet)
        {
            CooldownImage.fillAmount = 1;
        }

        if (CurrentTime > 0)
            CooldownText.text = $"{(int)CurrentTime}";
        else CooldownText.text = "";
    }
    public void SetIconMode(bool mode)
    {

        if (mode == false)
        {
            IconImage.color = Color.red;
            return;
        }
        IconImage.color = Color.white;
    }

    public void SetChargeTime(float CurrentTime, float CooldownSet)
    {
        NewValue = CurrentTime / CooldownSet;
        CooldownText.text = "";
    }

    public void Update()
    {
        CooldownImage.fillAmount = Mathf.Lerp(CooldownImage.fillAmount, NewValue, 5f * Time.deltaTime);
    }
}
