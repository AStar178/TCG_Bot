using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeed : AbilityPowerUps
{
    [SerializeField] int AttackSpeedBuff;
    public override void OnPowerUp(Player player)
    {
        base.OnPowerUp(player);

        player.AttackSpeedBuff += AttackSpeedBuff;
    }

}
