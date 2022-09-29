using UnityEngine;

public class vitalityBuffs : AbilityPowerUps 
{
    
    [SerializeField] int vitalityBuffszzz;
    public override void OnPowerUp(Player player)
    {
        base.OnPowerUp(player);

        player.vitality += vitalityBuffszzz;
        player.PlayerHp.Currenthp += player.PlayerHp.MaxHp * 0.25f;
        player.PlayerHp.Currenthp = Mathf.Clamp( player.PlayerHp.Currenthp , 0 , player.PlayerHp.MaxHp );
    }



}