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
    [SerializeField] float Tie;
    float s;
    public async void OnGetUpdate( Sprite sprite , string name , string dis )
    {
        gameObjecta.SetActive( true );

        Image.sprite = sprite;
        Names.text = name;
        Discrepen.text = dis;
        s = Tie;

        while ( s > 0 )
        {
            
            s -= Time.deltaTime;
            await Task.Yield();

        }
            
        gameObjecta.SetActive( false );
    }   

}
