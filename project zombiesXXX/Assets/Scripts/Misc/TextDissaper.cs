using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDissaper : MonoBehaviour
{
    float t;
    float f;

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;

        if (t >= .05f)
        {
            transform.position += new Vector3(0f, .05f, 0f);
            f++;
            t = 0f;
        }

        if (f > 50)
            Destroy(gameObject);
    }
}
