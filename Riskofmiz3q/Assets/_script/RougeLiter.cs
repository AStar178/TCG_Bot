using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RougeLiter : MonoBehaviour
{
    public static InputManager InputManager;
    public static ObjectHolder ObjectHolder;

    // Start is called before the first frame update
    void Awake()
    {
        InputManager = GetComponent<InputManager>();
        ObjectHolder = GetComponent<ObjectHolder>();
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
