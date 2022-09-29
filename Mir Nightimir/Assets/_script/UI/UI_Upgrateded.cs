using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Upgrateded : MonoBehaviour
{
    [SerializeField] Image Image;
    [SerializeField] TMP_Text Names;
    [SerializeField] TMP_Text Discrepen;
    [SerializeField] GameObject gameObjecta;
    public async void OnGetUpdate( Sprite sprite , string name , string dis )
    {
        gameObjecta.SetActive( true );

        Image.sprite = sprite;
        Names.text = name;
        Discrepen.text = dis;

        await Task.Delay( 5000 );
        gameObjecta.SetActive( false );
    }   

}
