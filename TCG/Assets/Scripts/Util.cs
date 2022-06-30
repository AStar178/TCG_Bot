using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour
{
    public static CardBag PlayerBag;
    public static Setting Setting;
    public static UI UI;

    private void Awake()
    {
        PlayerBag = gameObject.GetComponent<CardBag>();
        Setting = gameObject.GetComponent<Setting>();
        UI = gameObject.GetComponent<UI>();

        Setting.Speed = 1 / Setting.Speed;
    }
}
