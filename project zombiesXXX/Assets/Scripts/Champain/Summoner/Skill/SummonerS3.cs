using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerS3 : IteamSkill
{
    public float CooldownSet;
    private float Cooldown;

    SummonerEffects Summoner;

    public override void OnStart(PlayerState playerState)
    {
        Summoner = playerState.GetComponent<SummonerEffects>();
        Summoner.SetS3(this);

        base.OnStart(playerState);
    }

    public override void OnUseSkill(PlayerState playerState)
    {
        if (Summoner.Passive.Stop == true || Cooldown > 0)
            return;

        Cooldown = CooldownSet;
        Summoner.Passive.StartBeam();

        base.OnUseSkill(playerState);
    }

    public void Update()
    {
        if (Cooldown > 0)
        {
            Cooldown -= Time.deltaTime;
            Icons.SetCooldown(Cooldown, CooldownSet);
        }
    }
}
