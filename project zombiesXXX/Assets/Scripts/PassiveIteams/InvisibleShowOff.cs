using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleShowOff : IteamPassive
{
    public float InvisibleTimer;
    public float ScalingMaxLevel;
    private bool isOn;

    public override State OnUpdate(PlayerState playerState, ref State CalucatedValue, ref State state)
    {
        if (playerState.Combat && isOn == false)
        {
            isOn = true;
            float f = InvisibleTimer * Scaling1and0(ScalingMaxLevel);
            playerState.InvisableTime = f;
            playerState.Player.PlayerEffect.TurnOnInvisableEffectTime(f);
        } 
        if (!playerState.Combat && isOn == true)
        {
            isOn = false;
        }

        return base.OnUpdate(playerState, ref CalucatedValue, ref state);
    }
}
