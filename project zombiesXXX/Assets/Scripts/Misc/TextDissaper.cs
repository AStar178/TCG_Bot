using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDissaper : MonoBehaviour
{
    float t;
    public float f;

    void Update()
    {
        t += Time.deltaTime;

        if (t >= .05f)
        {
            transform.position += new Vector3(0f, .1f, 0f);
            f++;
            t = 0f;
        }

        if (f > 50)
            RPGStatic.Instance.objectPools.Release(this);
    }
}
