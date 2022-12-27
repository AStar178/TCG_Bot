using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBoss : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbodys;
    [SerializeField] float Speed;
    void Update()
    {
        rigidbodys.velocity = (EnemySpawner.falafa.position - transform.position).normalized * Speed;
    }
}
