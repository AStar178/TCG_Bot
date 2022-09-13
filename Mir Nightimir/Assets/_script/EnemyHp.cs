using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour , IHpValue
{ 
    [SerializeField] EnemyState EnemyState;

    public float MaxHp;
    public float Currenthp;

    public void HpValueChange(Damage damage)
    {
        float AdDamageAmount = 100 - EnemyState.State.Ap_Defence;
        Currenthp -= damage.AdDamage * ( AdDamageAmount / 100 );
    }

}
