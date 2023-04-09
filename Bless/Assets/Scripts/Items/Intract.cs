using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Intract : MonoBehaviour
{
    [SerializeField] UnityEvent onIntract;

    public void OnIntract()
    {
        onIntract.Invoke();
    }
}
