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
            target = FindTarget();
            return;
        }
        AttackTarget(); 
    }

    public Transform FindTarget()
    {
        if (PlayerWeaponManger.CurrentWeapons == null) { return null; }
        return PlayerWeaponManger.CurrentWeapons.SelectedTarget(Raduis , EnemyLayer);
    }

    private  void AttackTarget()
    {
        if ( !target.TryGetComponent<IHpValue>(out var Hp) ) { return; }
        if ( Vector2.Distance(transform.position , target.position) > Raduis ) { target = null; return; }
        if ( PlayerWeaponManger.CurrentWeapons.CoustomTargetSelect(target) == false ) { return; }

        PlayerWeaponManger.DealDamage(Hp , target);
    }
    #if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        
        Handles.DrawWireDisc(transform.position , Vector3.forward , Raduis);

    }
    #endif
}
