using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetroidPassive : IteamPassive
{
    [SerializeField] float JetPackPower;
    [SerializeField] float FlyTime = 1f;
    float t;
    bool on;
    public override State OnUpdate(PlayerState playerState , ref State Ctate , ref State state)
    {
        
        if (playerState.Player.PlayerInputSystem.JumpValue == 1)
        {
            if (t > 0)
            {
                playerState.Player.PlayerThirdPersonController._verticalVelocity += JetPackPower * Time.deltaTime;
                if (on == false)
                {
                    playerState.Player.PlayerEffect.TurnOnJectPackEffect();
                    on = true;
                }
                
                t -= Time.deltaTime;
                return state;
            }
            if (on == true)
            {
                playerState.Player.PlayerEffect.TurnOffJectPackEffect();
                on = false;
            }
            return state;
        }
        t = FlyTime;
        if (on == true)
        {
            playerState.Player.PlayerEffect.TurnOffJectPackEffect();
            on = false;
        }
            
        return state;
    }
}
