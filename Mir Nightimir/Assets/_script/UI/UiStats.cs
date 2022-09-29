using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiStats : MonoBehaviour
{

    [SerializeField] private TMP_Text hp;
    [SerializeField] private TMP_Text mama;
    [SerializeField] private TMP_Text xp;
    [SerializeField] private TMP_Text coins;
    [SerializeField] private TMP_Text Level;
    
    public void OnStat(UiEventData data)
    {
        hp.text = $"{data.CurrentHP}/{data.MaxHp}";
        mama.text = $"{data.CurrentMana}/{data.MaxMana}";
        xp.text = $"{data.CurrentXp}/{data.MaxXp}";
        coins.text = $"{data.CoinsAmount}";
        Level.text = data.CurrentLevel.ToString();
    }
}
