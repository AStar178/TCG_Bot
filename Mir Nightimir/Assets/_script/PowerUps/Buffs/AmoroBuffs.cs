using UnityEngine;

public class AmoroBuffs : AbilityPowerUps 
{
    
    [SerializeField] int Amorozzz;
    public override void OnPowerUp(Player player)
    {
        base.OnPowerUp(player);

        player.AmoroBuff += Amorozzz;
    }



}