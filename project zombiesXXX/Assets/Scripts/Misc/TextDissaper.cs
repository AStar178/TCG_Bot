using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDissaper : MonoBehaviour
{
    [HideInInspector]
    public float f;
    public Vector3 newPoisition;


    void Update()
    {
        f += Time.deltaTime;

        transform.position = Vector3.Lerp(transform.position, newPoisition, 4 * Time.deltaTime);

        if (f > 1.5f)
            RPGStatic.Instance.objectPools.Release(this);
    }
}
