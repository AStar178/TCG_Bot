using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHelper : MonoBehaviour
{
    public Rigidbody rb;
    public float Aggro;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
}
