using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RougeLiter : MonoBehaviour
{
    public InputManager InputManager;
    public ObjectHolder ObjectHolder;
    public static RougeLiter rougeLiter;

    // Start is called before the first frame update
    void Awake()
    {
        rougeLiter = this;
        InputManager = GetComponent<InputManager>();
        ObjectHolder = GetComponent<ObjectHolder>();
    }

    public GameObject Create(float destroy, GameObject gameObject, Vector3 pos, GameObject parent = null)
    {
        GameObject a = Instantiate(gameObject, parent.transform);
        a.transform.position = pos;
        Destroy(a, destroy);
        return a;
    }
}
