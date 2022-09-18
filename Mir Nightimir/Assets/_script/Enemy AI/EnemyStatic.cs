using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatic : MonoBehaviour
{
    public static GameObject project;
    public GameObject Project;
    public static GameObject textPrefab;
    public GameObject TextPrefab;

    private void Awake()
    {
        project = Project;
        TextPrefab = textPrefab;
    }
}
