using UnityEngine;

public abstract class Iteam : MonoBehaviour {
    public int Oderlayer = 0;
    public virtual void OnStart(PlayerState playerState)
    {

    }
    public virtual State OnUpdate(PlayerState playerState , ref State CalucatedValue , ref State state)
    {
        return state;
    }

    public virtual void OnDrop(PlayerState playerState)
    {

    }

    public DamageData CreatDamage(float damage , PlayerState playerState)
    {
        DamageData damageData = new DamageData();
        damageData.DamageAmount = damage;
        damageData.target = playerState.Player.PlayerInputSystem.transform;
        return damageData;
    }

    protected void InCombat()
    {
        Player.Current.PlayerState.Combat = true;
        Player.Current.PlayerState.xc = 5;
    }
}