using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minus : MonoBehaviour
{
    public static Minus Instance;
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
        if (iteamType == RpgHelper.IteamType.TRASHAss)
            return colors[0];
        
        if (iteamType == RpgHelper.IteamType.NotThatgoodbutokay)
            return colors[1];
            
        if (iteamType == RpgHelper.IteamType.YESDUDE)
            return colors[2];
            
        if (iteamType == RpgHelper.IteamType.WTFILOVELIFEIFIGETTHELEGENDERYOFCOURSE)
            return colors[3];
            
        if (iteamType == RpgHelper.IteamType.THISISTHEGREATESDAYOFMYLIFE)
            return colors[4];
        
        return null;
    }
    public Color GetRightColorForIteamRarety(RpgHelper.IteamType iteamType)
    {
        if (iteamType == RpgHelper.IteamType.TRASHAss)
            return colorsx[0];
        
        if (iteamType == RpgHelper.IteamType.NotThatgoodbutokay)
            return colorsx[1];
            
        if (iteamType == RpgHelper.IteamType.YESDUDE)
            return colorsx[2];
            
        if (iteamType == RpgHelper.IteamType.WTFILOVELIFEIFIGETTHELEGENDERYOFCOURSE)
            return colorsx[3];
            
        if (iteamType == RpgHelper.IteamType.THISISTHEGREATESDAYOFMYLIFE)
            return colorsx[4];
        
        return Color.black;
    }
}
