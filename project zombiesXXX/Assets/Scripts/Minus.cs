using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minus : MonoBehaviour
{
    public static Minus Instance;

    public GameObject textPrefab;

    public void CreatCoustomTextPopup(string v, Vector3 position, Color color)
    {
        var text = Instantiate(textPrefab, position, Quaternion.identity);
        TMPro.TMP_Text tMP_Text = text.GetComponentInChildren<TMPro.TMP_Text>();
        tMP_Text.text = v;
        tMP_Text.color = color;
    }
}
