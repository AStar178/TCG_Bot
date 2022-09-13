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

        Collider2D collider2D = Physics2D.OverlapCircle( transform.position , Raduis , EnemyLayer );

        if (collider2D != null)
        {
            
        }        
    }

    private void AttackTarget()
    {

    }
}
