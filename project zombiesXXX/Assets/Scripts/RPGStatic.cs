using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;

public class RPGStatic : MonoBehaviour
{
    public static RPGStatic Instance;
    public GameObject textPrefab;
    public GameObject EmpetyIteam;
    [SerializeField] Material[] colors;
    [SerializeField] Color[] colorsx;
    public ObjectPool<TextDissaper> objectPools;

    public Vector3 TextMinMove, TextMaxMove;

    private void Awake() {
        objectPools = new ObjectPool<TextDissaper>(() => {
            return Instantiate(textPrefab , Vector3.zero , Quaternion.identity).GetComponent<TextDissaper>();
        }
        , s => {
            s.gameObject.SetActive(true);
        }
        ,
        s => 
        {
            s.gameObject.SetActive(false);
        }
        ,
        s =>
        {
            Destroy(s.gameObject);
        }
        ,
        false
        , 1000
        , 1000
        );
        Instance = this;

    }

    public void CreatCoustomTextPopup(string v, Vector3 position, Color color)
    {
        var text = objectPools.Get();
        text.f = 0;
        text.transform.position = position;
        TMPro.TMP_Text tMP_Text = text.GetComponentInChildren<TMPro.TMP_Text>();
        tMP_Text.text = v;
        tMP_Text.color = color;
        text.newPoisition = text.transform.position + randomVector();
    }
    public void CreatCoustomTextPopupOnlyUp(string v, Vector3 position, Color color)
    {
        var text = objectPools.Get();
        text.f = 0;
        text.transform.position = position;
        TMPro.TMP_Text tMP_Text = text.GetComponentInChildren<TMPro.TMP_Text>();
        tMP_Text.text = v;
        tMP_Text.color = color;
        text.newPoisition = text.transform.position + Vector3.up * 2;
    }
    public void CreatCoustomTextPopupMatrial(string v, Vector3 position, Color color)
    {
        var text = objectPools.Get();
        text.f = 0;
        text.transform.position = position;
        TMPro.TMP_Text tMP_Text = text.GetComponentInChildren<TMPro.TMP_Text>();
        tMP_Text.text = v;
        tMP_Text.material.color = color;
        text.newPoisition = text.transform.position + randomVector();
    }
    public void CreatCoustomTextPopupOnlyUpMatrial(string v, Vector3 position, Color color , float w)
    {
        var text = objectPools.Get();
        text.f = 0;
        text.transform.position = position;
        TMPro.TMP_Text tMP_Text = text.GetComponentInChildren<TMPro.TMP_Text>();
        tMP_Text.text = v;
        tMP_Text.material.SetColor( "_FaceColor" , color * w );
        text.newPoisition = text.transform.position + Vector3.up * 2;
    }

    public Vector3 randomVector()
    {
        Vector3 a = new Vector3()
        {
            x = Random.Range(TextMinMove.x, TextMaxMove.x + 1),
            y = Random.Range(TextMinMove.x, TextMaxMove.x + 1),
            z = Random.Range(TextMinMove.x, TextMaxMove.x + 1)
        };
        return a;
    }
    
    public Material GetRightMatrialColorForIteamRarety(RpgHelper.IteamType iteamType)
    {
        if (iteamType == RpgHelper.IteamType.Tier1)
            return colors[0];
        
        if (iteamType == RpgHelper.IteamType.Tier2)
            return colors[1];
            
        if (iteamType == RpgHelper.IteamType.Tier3)
            return colors[2];
            
        if (iteamType == RpgHelper.IteamType.Tier4)
            return colors[3];
            
        if (iteamType == RpgHelper.IteamType.Tier5)
            return colors[4];
        
        return null;
    }
    public Color GetRightColorForIteamRarety(RpgHelper.IteamType iteamType)
    {
        if (iteamType == RpgHelper.IteamType.Tier1)
            return colorsx[0];
        
        if (iteamType == RpgHelper.IteamType.Tier2)
            return colorsx[1];
            
        if (iteamType == RpgHelper.IteamType.Tier3)
            return colorsx[2];
            
        if (iteamType == RpgHelper.IteamType.Tier4)
            return colorsx[3];
            
        if (iteamType == RpgHelper.IteamType.Tier5)
            return colorsx[4];
        
        return Color.black;
    }
}
