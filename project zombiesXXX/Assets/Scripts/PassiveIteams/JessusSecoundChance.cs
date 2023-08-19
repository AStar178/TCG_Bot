//When you about to Die a light from sky will save you 
using System; 
using System.Collections; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
using UnityEngine;
using UnityEngine.VFX;

public class JessusSecoundChance : IteamPassive 
{ 
    public static JessusSecoundChance Current;
    [SerializeField] private VisualEffect visualEffect;
    public override void OnStart(PlayerState playerState) 
    { 
        Current = this;
        playerState.Player.PlayerHp.jesus = true;
    } 
    public override void OnLevelUp(PlayerState playerState) 
    { 
        playerState.Player.PlayerHp.jesus = true;
    } 
    public override State OnUpdate(PlayerState playerState, ref State CalucatedValue, ref State state) 
    { 
        return base.OnUpdate(playerState, ref CalucatedValue, ref state); 
    }

    public void JesusTime()
    {
        PlayerState.CalculatedValue.HpCurrent = PlayerState.CalculatedValue.HpMax;
        Instantiate(visualEffect , PlayerGetpos , Quaternion.identity);

    }
}