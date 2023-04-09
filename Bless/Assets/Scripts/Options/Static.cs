using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Static : MonoBehaviour
{
    public static Option option;
    public static UIManager UiManager;
    public static PlayerStat PlayerStat;
    public static Bullets BulletObj;
    public static Compass Compass;
    public static QuestC QuestC;
    public static List<GameObject> Menus;
    public List<GameObject> menus;
    public LayerMask playerLayer;
    public static LayerMask PlayerLayer;

    public static bool InMenu()
    {
        bool b = false;

        foreach (GameObject gameObject in Menus)
        {
            if (gameObject.activeInHierarchy == true)
                b = true;
        }

        return b;
    }
    

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        option = GetComponent<Option>();
        UiManager = GetComponent<UIManager>();
        PlayerStat = GetComponent<PlayerStat>();
        BulletObj = GetComponent<Bullets>();
        Compass = GetComponent<Compass>();
        QuestC = GetComponent<QuestC>();
        Menus = menus;
        PlayerLayer = playerLayer;
    }
}
