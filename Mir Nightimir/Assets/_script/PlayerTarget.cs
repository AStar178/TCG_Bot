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
    [SerializeField] public SpriteRenderer TargetIcon;

    void Update()
    {
        if (target == null)
        {
            TargetIcon.enabled = false;
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
        if ( !target.TryGetComponent<IHpValue>(out var Hp) ) { TargetIcon.enabled = false; return; }
        if ( Vector2.Distance(transform.position , target.position) > Raduis ) { TargetIcon.enabled = false; target = null; return; }
        if ( PlayerWeaponManger.CurrentWeapons.CoustomTargetSelect(target) == false ) { TargetIcon.enabled = false; target = null; return; }
        TargetIcon.enabled = true;
        
        TargetIcon.transform.position = new Vector2 ( target.position.x + -0.54f , target.position.y + 0.54f );
        PlayerWeaponManger.DealDamage(Hp , target);
    }
    #if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        
        Handles.DrawWireDisc(transform.position , Vector3.forward , Raduis);

    }
    #endif
}
