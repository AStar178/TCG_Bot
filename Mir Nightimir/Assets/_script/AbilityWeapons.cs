using System;
using UnityEngine;

public abstract class AbilityWeapons : MonoBehaviour 
{
    private Player player;
    protected Player GetPlayer() => player;
    protected PlayerWeaponManger GetPWM() => player.PlayerWeaponManger;
    public virtual void StartAbilityWp(Player newplayer)
    {
        player = newplayer;
    }
    public virtual void UpdateAbilityWp()
    {

    }
    public virtual void DealDamage(IHpValue enemyHp , Transform pos)
    {

    }

    public virtual void StopAbilityWp()
    {
        Destroy(this.gameObject);
    }
}