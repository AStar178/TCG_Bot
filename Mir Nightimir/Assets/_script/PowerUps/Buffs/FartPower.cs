using UnityEngine;

public class FartPower : AbilityPowerUps 
{
    [SerializeField] float FartPowerzzz;
    public override void OnPowerUp(Player player)
    {
        base.OnPowerUp(player);


        player.Fart += FartPowerzzz;
    }

}