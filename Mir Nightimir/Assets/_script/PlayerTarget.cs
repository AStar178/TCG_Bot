using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
            target = collider2D.gameObject.transform;
        }        

    }

    private void AttackTarget()
    {
        if ( !target.TryGetComponent<IHpValue>(out var Hp) ) { return; }
        if ( Vector2.Distance(transform.position , target.position) > Raduis ) { return; }

        PlayerWeaponManger.DealDamage(Hp , target);
    }
    #if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        
        Handles.DrawWireDisc(transform.position , Vector3.forward , Raduis);

    }
    #endif
}
