using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : IteamPassive
{
    private bool active;
    public string HeroName;
    public float MaxIncreace;
    public float RegenIncreace;

    public override void OnStart(PlayerState playerState)
    {
        if (playerState.GetName() == HeroName)
            active = true;

        if (active)
        {
            MetroidEnergy energy = playerState.GetComponent<MetroidEnergy>();
            energy.EnergyMax += MaxIncreace * 2;
            energy.EnergyRegen += RegenIncreace * 2;
        }

        base.OnStart(playerState);
    }

    public override void OnLevelUp(PlayerState playerState)
    {
        if (active)
        {
            MetroidEnergy energy = playerState.GetComponent<MetroidEnergy>();
            energy.EnergyMax += MaxIncreace;
            energy.EnergyRegen += RegenIncreace;
        }
        base.OnLevelUp(playerState);
    }
}
