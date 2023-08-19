using System; 
using System.Collections; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
using UnityEngine; 

public class Toto : IteamPassive 
{ 
    public override void OnStart(PlayerState playerState) 
    { 
 
    } 
    public override void OnLevelUp(PlayerState playerState) 
    { 
 
    } 
    public override State OnUpdate(PlayerState playerState, ref State CalucatedValue, ref State state) 
    { 

        

        return base.OnUpdate(playerState, ref CalucatedValue, ref state); 
    } 
}