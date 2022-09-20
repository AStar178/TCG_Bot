using UnityEngine;

public class vitalityBuffs : AbilityPowerUps 
{
    
    [SerializeField] int vitalityBuffszzz;
    public override void OnPowerUp(Player player)
    {
        base.OnPowerUp(player);

        player.vitality += vitalityBuffszzz;
    }



}