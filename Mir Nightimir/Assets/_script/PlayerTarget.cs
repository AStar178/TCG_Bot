using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTarget : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] public PlayerWeaponManger PlayerWeaponManger;
    [SerializeField] public Player Player;
    [SerializeField] public float Raduis;
    [SerializeField] public LayerMask EnemyLayer;
    void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        AttackTarget(); 
    }

    private void FindTarget()
    {
        
    }

    
    private void AttackTarget()
    {

    }
}
