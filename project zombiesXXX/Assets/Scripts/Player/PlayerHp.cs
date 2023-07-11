using System;
using UnityEngine;

public class PlayerHp : PlayerComponetSystem , IDamageAble
{

    void IDamageAble.TakeDamage(DamageData Data)
    {
        float Hp = GetHpCurrent - Data.DamageAmount;
        Hp = Mathf.Clamp( Hp , 0 , 100 );
        SetHpCurrent(Hp);
        if (GetHpCurrent <= 0)
            Killed();

    }

    private void Killed()
    {
        Debug.Log("DIED");
    }

}