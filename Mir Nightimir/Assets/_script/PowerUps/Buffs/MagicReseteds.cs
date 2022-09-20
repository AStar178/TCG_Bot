using UnityEngine;

public class MagicReseteds : AbilityPowerUps 
{
    
    [SerializeField] int MagicResetedszzz;
    public override void OnPowerUp(Player player)
    {
        base.OnPowerUp(player);

        player.MagicReseted += MagicResetedszzz;
    }



}