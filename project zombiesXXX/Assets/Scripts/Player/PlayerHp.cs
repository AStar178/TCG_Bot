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
        float Armorpers = (Player.PlayerState.ResultValue.Deffece / (100 + Player.PlayerState.ResultValue.Deffece));
        float Hp = GetHpCurrent - (Data.DamageAmount * (1 - Armorpers));
        Hp = Mathf.Clamp(Hp, 0, Player.PlayerState.ResultValue.HpMax);
        SetHpCurrent(Hp);
        Player.PlayerState.OnDamageTake(Data);
        if (GetHpCurrent <= 0)
            Killed();

    }
    void IDamageAble.Heal(DamageData Data)
    {

        if (Player.PlayerState.InvisableTime > 0)
        {
            return;
        }
        float Hp = GetHpCurrent + Data.DamageAmount;
        Hp = Mathf.Clamp(Hp, 0, Player.PlayerState.ResultValue.HpMax);
        SetHpCurrent(Hp);
        Player.PlayerState.OnHeal(Data);
        if (GetHpCurrent >= Player.PlayerState.ResultValue.HpMax)
            SetHpCurrent(Player.PlayerState.ResultValue.HpMax);

    }

    private void Killed()
    {
        Debug.Log("DIED");
    }

}