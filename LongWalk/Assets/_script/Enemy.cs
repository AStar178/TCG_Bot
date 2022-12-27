using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent.SetDestination( EnemySpawner.falafa.position );
    }

    
}
