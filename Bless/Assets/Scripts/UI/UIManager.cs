using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text Staminatext;
    public Slider staminaSlider;
    public Image StaminaImage;
    public Color ExhustedColor;
    public Color StaminaColor;
    public TMP_Text ManaText;
    public Slider ManaSlider;
    public Image ManaImage;
    public Color DrainedColor;
    public Color ManaColor;
    public TMP_Text Speedtext;
    public TMP_Text GrabText; 
    private int timeToDissaperS;
    private int timeToDissaperM;

    [Header("HUD")]
    public GameObject PopUpHolder;
    public TMP_Text PopUpText;
    public int PopUpToDissapear;
    private int PopUpDissapear;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        if (Input.GetKeyUp(Static.option.Intract))
            ShowResouse();

        if (timeToDissaperS > 0)
        {
            staminaSlider.gameObject.SetActive(true);
            timeToDissaperS--;
        }
        else if (timeToDissaperS <= 0 && staminaSlider.gameObject.activeInHierarchy != false)
            staminaSlider.gameObject.SetActive(false);


        if (timeToDissaperM > 0)
        {
            ManaSlider.gameObject.SetActive(true);
            timeToDissaperM--;
        }
        else if (timeToDissaperM <= 0 && ManaSlider.gameObject.activeInHierarchy != false)
            ManaSlider.gameObject.SetActive(false);


        if (PopUpDissapear > 0)
            PopUpDissapear--;
        else
        {
            if (PopUpHolder.activeInHierarchy)
            {
                PopUpHolder.SetActive(false);
            }
        }
    }

    public void ShowResouse()
    {
        timeToDissaperS = 90;
        timeToDissaperM = 90;
    }

    public void ShowIntract(string text)
    {
        GrabText.text = $"Press E to " + text;
    }

    public void HideIntract()
    {
        GrabText.text = "";
    }

    public void StaminaUI(float current, float max)
    {
        timeToDissaperS = 30;
        Staminatext.text = (int)(current) + "/" + max;
        staminaSlider.maxValue = max;
        staminaSlider.value = current;
        if (Static.PlayerStat.Exhusted)
        {
            StaminaImage.color = ExhustedColor;
            Staminatext.color = ExhustedColor;
        }
        else if (!Static.PlayerStat.Exhusted)
        {
            StaminaImage.color = StaminaColor;
            Staminatext.color = StaminaColor;
        }
    }

    public void ManaUI(float current, float max)
    {
        timeToDissaperM = 30;
        ManaText.text = (int)(current) + "/" + max;
        ManaSlider.maxValue = max;
        ManaSlider.value = current;
        if (Static.PlayerStat.Drained) { ManaImage.color = DrainedColor; ManaText.color = DrainedColor; }
        else if (!Static.PlayerStat.Drained) { ManaImage.color = ManaColor; ManaText.color = ManaColor; }
    }

    public void SpeedUI(float speed)
    {
        Speedtext.text = "Speed: " + (int)(speed);
    }

    public void PopUp(string Text)
    {
        PopUpText.text = Text;
        PopUpHolder.SetActive(true);
        PopUpHolder.transform.DOShakeRotation(.3f);
        PopUpDissapear = PopUpToDissapear;
    }

    public bool CanPopUp()
    {
        return !PopUpHolder.activeInHierarchy;
    }
}
