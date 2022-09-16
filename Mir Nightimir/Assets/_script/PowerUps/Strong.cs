using UnityEngine;

public class Strong : AbilityPowerUps 
{
    [SerializeField] float strength;
    public override void OnPowerUp(Player player)
    {
        base.OnPowerUp(player);


        player.strength += strength;
    }

}