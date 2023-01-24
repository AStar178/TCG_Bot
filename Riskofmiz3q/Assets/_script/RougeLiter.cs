using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RougeLiter : MonoBehaviour
{
    public GameObject orientarion;
    public static GameObject Orientarion;
    public static InputManager InputManager;
    public static ObjectHolder ObjectHolder;

    // Start is called before the first frame update
    void Awake()
    {
        InputManager = GetComponent<InputManager>();
        ObjectHolder = GetComponent<ObjectHolder>();
        Orientarion = orientarion;
    }

    public static GameObject Create(float destroy, GameObject gameObject, Vector3 pos)
    {
        GameObject a = Instantiate(gameObject);
        a.transform.position = pos;
        Destroy(a, destroy);
        return a;
    }

    public static void SetParent(Transform boy, Transform parent)
    {
        boy.parent = parent;
    }
}
