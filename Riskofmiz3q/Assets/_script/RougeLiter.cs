using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RougeLiter : MonoBehaviour
{
    public static InputManager InputManager;
    private static GameObject blank;
    public GameObject Blank;

    // Start is called before the first frame update
    void Awake()
    {
        blank = Blank;
        InputManager = GetComponent<InputManager>();
    }

    public static GameObject Create(float destroy)
    {
        GameObject a = Instantiate(blank);
        Destroy(a, destroy);
        return a;
    }
}
