using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI_Upgrateded : MonoBehaviour
{
    [SerializeField] Image Image;
    [SerializeField] TMP_Text Names;
    [SerializeField] TMP_Text Discrepen;
    [SerializeField] GameObject Panel;
    [SerializeField] float Tie;
    bool on;
    bool one;
    float s;
    public async void OnGetUpdate( Sprite sprite , string name , string dis )
    {
        if (on == false)
        {
            if (one == false)
            {
                Panel.transform.DOMoveX(gameObject.transform.position.x + 1355, 0.1f);
                await AIStatic.Wait(.1f);
                Panel.SetActive(true);
                await AIStatic.Wait(.1f);
                Panel.transform.DOMoveX(gameObject.transform.position.x, .5f);
                one = true;
                on = true;
            } else
            {
                Panel.SetActive(true);
                on = true;
                Panel.transform.DOMoveX(gameObject.transform.position.x, .5f);
            }
        }

        Image.sprite = sprite;
        Names.text = name;
        Discrepen.text = dis;
        s = Tie + 1;

        while ( s > 1 )
        {
            
            s -= Time.deltaTime;
            await Task.Yield();

        }

        Panel.transform.DOMoveX(Panel.transform.position.x - 30, .4f);
        await AIStatic.Wait(.4f);
        Panel.transform.DOMoveX(gameObject.transform.position.x + 1355, .4f);
        await AIStatic.Wait(.4f);
        Panel.SetActive( false );
        on = false;
    }   

}
