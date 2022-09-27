using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBuffs : AbilityPowerUps
{
    [SerializeField] int Hp;


    public override void OnPowerUp(Player player)
    {
        base.OnPowerUp(player);

        player.HpBuff += Hp;
        player.PlayerHp.Currenthp = player.PlayerHp.MaxHp;
        GetPlayer().UpdateUI();

    }
}
