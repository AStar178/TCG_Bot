using UnityEngine;

public class DamageAdBuffs : AbilityPowerUps 
{
    
    [SerializeField] int DamageAdzz;
    public override void OnPowerUp(Player player)
    {
        base.OnPowerUp(player);

        player.DamageAdBuff += DamageAdzz;
    }



}