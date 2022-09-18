using UnityEngine;

public class strength : AbilityPowerUps 
{
    [SerializeField] float strengthzz;
    public override void OnPowerUp(Player player)
    {
        base.OnPowerUp(player);


        player.strength += strengthzz;
    }

}