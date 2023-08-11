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

        if (raycastHit.collider == null)
            return;
        if (DistanceCheakPlayerCameraRayCast(Range) == false)
            return;

        Cooldown = CooldownSet;
        Summoner.Passive.StartVortex(raycastHit.point);

        base.OnUseSkill(playerState);
    }

    public void Update()
    {
        Icons.SetIconMode( DistanceCheakPlayerCameraRayCast(Range) );
        if (Cooldown > 0)
        {
            Cooldown -= Time.deltaTime;
            Icons.SetCooldown(Cooldown, CooldownSet);
        }
    }
}
