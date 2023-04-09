using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSimiliar : MonoBehaviour
{
    public Transform LikeThis;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = LikeThis.rotation;
    }
}
