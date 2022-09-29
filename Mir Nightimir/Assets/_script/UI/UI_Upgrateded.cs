using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Upgrateded : MonoBehaviour
{
    [SerializeField] Image Image;
    [SerializeField] TMP_Text Names;
    [SerializeField] TMP_Text Discrepen;
    public void OnGetUpdate(UpdagrateEventdata data)
    {
        Image.sprite = data.Image;
        Names.text = data.Name;
        Discrepen.text = data.Discripsen;
    }

}
