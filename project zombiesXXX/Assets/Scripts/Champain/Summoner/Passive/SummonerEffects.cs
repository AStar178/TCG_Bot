using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerEffects : MonoBehaviour
{
    [SerializeField]
    private Transform spiritualPos;
    private SpritualWeapon weapon;
    private SummonerS1 SummonerS1;
    private SummonerS2 SummonerS2;

    public Transform SpiritualPos() => spiritualPos;
    public SpritualWeapon Passive => weapon;
    public SummonerS1 S1 => SummonerS1;
    public SummonerS2 S2 => SummonerS2;

    public void SetPassive(SpritualWeapon weapon)
    {
        this.weapon = weapon;
    }
    public void SetS1(SummonerS1 weapon)
    {
        SummonerS1 = weapon;
    }
    public void SetS2(SummonerS2 weapon)
    {
        SummonerS2 = weapon;
    }
}
