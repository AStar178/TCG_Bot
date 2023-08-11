using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerS2 : IteamSkill
{
    public float CooldownSet;
    private float Cooldown;

    public float Range = 50;

    private SummonerEffects Summoner;

    public override void OnStart(PlayerState playerState)
    {
        Summoner = playerState.GetComponent<SummonerEffects>();
        Summoner.SetS2(this);
        playerState.ShowForwardIndecater = true;
        base.OnStart(playerState);
    }

    public override void OnUseSkill(PlayerState playerState)
    {
        if (Summoner.Passive.Stop == true || Cooldown > 0)
            return;

        var Camerax = Camera.main;
        var ray = Physics.Raycast(Camerax.transform.position, Camerax.transform.forward, out var hs, Range, GroundLayer);

        if (hs.collider == null)
            return;

        Cooldown = CooldownSet;
        Summoner.Passive.StartVortex(hs.point);

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
