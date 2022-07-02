using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ULT_UI : MonoBehaviour
{
    
    public float anger;
    public Image angerBar;
    public float Tie;
    public BattleS battleS;


    private void OnEnable() {
        battleS.AngerChangeEvent += AngerValue;
    }

    private void OnDisable() {
        battleS.AngerChangeEvent -= AngerValue;
    }

    public void AngerValue(float AngerValue2)
    {
        anger = AngerValue2/100;
    }

    private void Update() {
        

        angerBar.fillAmount = Mathf.Lerp(angerBar.fillAmount , anger , Tie * Time.deltaTime);


    }
}
