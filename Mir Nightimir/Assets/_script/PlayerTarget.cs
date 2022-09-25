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
    public List<int> targetLayer;

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

    private void AttackTarget()
    {
        if ( LayerCheak(target) == false ) { target = null; return; }
        if ( !target.TryGetComponent<IHpValue>( out var Hp ) ) { TargetIcon.enabled = false; return; }
        if ( Vector2.Distance( transform.position , target.position ) > Raduis ) { TargetIcon.enabled = false; target = null; return; }
        if ( PlayerWeaponManger.CurrentWeapons.CoustomTargetSelect( target , out var CostumTarget ) == false ) { TargetIcon.enabled = false; target = null; return; }
        TargetIcon.enabled = true;
        
        TargetIcon.transform.position = new Vector2 ( target.position.x + -0.54f , target.position.y + 0.54f );
        PlayerWeaponManger.DealDamage(Hp , CostumTarget);
    }


    private bool LayerCheak(Transform other)
    {
        
        for (int i = 0; i < targetLayer.Count; i++)
        {
            if ( targetLayer[i] == other.gameObject.layer )
                return true;
        }

        return false;
    }
    
    #if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        
        Handles.DrawWireDisc(transform.position , Vector3.forward , Raduis);

    }
    #endif
}
