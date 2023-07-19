using System;
using UnityEngine;

public class PlayerHp : PlayerComponetSystem , IDamageAble
{
    void IDamageAble.TakeDamage(DamageData Data)
    {
        if (Player.PlayerState.InvisableTime > 0)
        {
            return;
        }
        float Armorpers = (Player.PlayerState.ResultValue.Deffece / (100 + Player.PlayerState.ResultValue.Deffece)) ;
        float Hp = GetHpCurrent - (Data.DamageAmount * (1 - Armorpers) );
        Hp = Mathf.Clamp( Hp , 0 , Player.PlayerState.ResultValue.HpMax );
        SetHpCurrent(Hp);
        print(Hp);
        if (GetHpCurrent <= 0)
            Killed();

    }

    private void Killed()
    {
        Debug.Log("DIED");
    }

}