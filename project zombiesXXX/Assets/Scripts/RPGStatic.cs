using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGStatic : MonoBehaviour
{
    public static RPGStatic Instance;
    public GameObject textPrefab;
    [SerializeField] Material[] colors;
    [SerializeField] Color[] colorsx;
    private void Awake() {
        
        Instance = this;

    }

    public void CreatCoustomTextPopup(string v, Vector3 position, Color color)
    {
        var text = Instantiate(textPrefab, position, Quaternion.identity);
        TMPro.TMP_Text tMP_Text = text.GetComponentInChildren<TMPro.TMP_Text>();
        tMP_Text.text = v;
        tMP_Text.color = color;
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
