using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerS1 : IteamSkill
{
    public float CooldownSet;
    private float Cooldown;

    private SummonerEffects Summoner;

    public override void OnStart(PlayerState playerState)
    {
        Summoner = playerState.GetComponent<SummonerEffects>();
        Summoner.SetS1(this);

        base.OnStart(playerState);
    }

    public override void OnUseSkill(PlayerState playerState)
    {
        if (Summoner.Passive.Stop == true || Cooldown > 0)
            return;

        if (Summoner.Passive.StartChargeAtTarget()  == true)
        {
            int b = (Summoner.Passive.level) / 10;
            if (b > 10)
                b = 10;
            else if (b < 1)
                b = 1;
            Cooldown = CooldownSet / b;
            Icons.SetCooldown(Cooldown, CooldownSet / b);
        }
        base.OnUseSkill(playerState);
    }

    public void Failed()
    {
        Cooldown = .1f;
    }

    public void Update()
    {
        if (Cooldown > 0)
        { Cooldown -= Time.deltaTime; Icons.SetCooldown(Cooldown, CooldownSet); }
    }
}
