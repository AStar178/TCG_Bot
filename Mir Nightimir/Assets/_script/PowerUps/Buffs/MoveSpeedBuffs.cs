using UnityEngine;

public class MoveSpeedBuffs : AbilityPowerUps 
{
    
    [SerializeField] int MoveSpeedBuffszzz;
    public override void OnPowerUp(Player player)
    {
        base.OnPowerUp(player);

        player.MoveSpeedBuff += MoveSpeedBuffszzz;
    }



}