using System;
using UnityEngine;

public class HornOffUndiying : IteamPassive {

    [SerializeField] float Cooldown;
    [SerializeField] float StayCooldown;
    [SerializeField] int attackSpeed;
    float cooldown;
    float staycooldown;
    private void Start() {
        
        staycooldown = StayCooldown;

    }
    public override State OnUpdate(PlayerState playerState, ref State CalucatedValue, ref State state)
    {
        cooldown -= Time.deltaTime;

        if (cooldown > 0)
            return state;

        if ((CalucatedValue.HpCurrent / CalucatedValue.HpMax) > 0.25f)
            return state;

        if (staycooldown > 0)
        {
            staycooldown -= Time.deltaTime;
            state.CritDamageMulty *= 2;
            state.AttackSpeed *= attackSpeed;
            state.Damage *= 2;

            return state;
        }
        
        cooldown = Cooldown;
        staycooldown = StayCooldown;


        return base.OnUpdate(playerState, ref CalucatedValue, ref state);
    }



}