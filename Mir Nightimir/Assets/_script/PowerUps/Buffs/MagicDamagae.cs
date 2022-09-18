using UnityEngine;

public class MagicDamagae : AbilityPowerUps 
{
    [SerializeField] float Magiczz;
    public override void OnPowerUp(Player player)
    {
        base.OnPowerUp(player);


        player.intelligence += Magiczz;
    }

}