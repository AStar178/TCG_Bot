using UnityEngine;

public class XpScale : AbilityPowerUps 
{
    [Range ( 0f , 1f )]
    [SerializeField] float XpGainBuffzzz;
    public override void OnPowerUp(Player player)
    {
        base.OnPowerUp(player);


        player.XpGainBuff += XpGainBuffzzz;
    }

}