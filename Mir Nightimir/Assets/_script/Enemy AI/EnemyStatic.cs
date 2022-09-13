using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatic : MonoBehaviour
{
    public static GameObject project;
    public GameObject Project;

    private void Awake()
    {
        project = Project;
    }
}
