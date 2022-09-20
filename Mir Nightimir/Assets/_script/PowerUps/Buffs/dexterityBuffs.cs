using UnityEngine;

public class dexterityBuffs : AbilityPowerUps 
{
    
    [SerializeField] int Dexteryzzz;
    public override void OnPowerUp(Player player)
    {
        base.OnPowerUp(player);

        player.dexterity += Dexteryzzz;
    }



}