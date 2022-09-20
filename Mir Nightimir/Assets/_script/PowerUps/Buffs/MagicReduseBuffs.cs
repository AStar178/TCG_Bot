using UnityEngine;

public class MagicReduseBuffs : AbilityPowerUps 
{
    
    [SerializeField] int MagicReduseBuffzzz;
    public override void OnPowerUp(Player player)
    {
        base.OnPowerUp(player);

        player.MagicReduseBuff += MagicReduseBuffzzz;
    }



}