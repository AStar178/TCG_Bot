using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour , IHpValue
{ 
    [SerializeField] EnemyState EnemyState;

    public float MaxHp;
    public float Currenthp;
    public float Amoro;
    public float MagicResest;

    public void HpValueChange(Damage damage)
    {
        float AdDamageAmount = 100 - Amoro;
        Currenthp -= damage.AdDamage * ( AdDamageAmount / 100 );
        float ApDamageAmount = 100 - MagicResest;
        Currenthp -= damage.ApDamage * ( AdDamageAmount / 100 );
    }

}
