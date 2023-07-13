using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetroidPassive : PassiveIteam
{
    [SerializeField] float JetPackPower;
    bool flying;
    float timetick = 0.1f;
    float t;
    public override void OnUpdate(PlayerState playerState)
    {
        t -= Time.deltaTime;
        if (flying)
        {
            if (t < 0)
            {
                playerState.Player.PlayerThirdPersonController._verticalVelocity += JetPackPower;
                t = timetick;
            }
            
        }
        
        if (playerState.Player.StarterAssetsInputs.jump)
            {
                if (flying == true)
                {
                    flying = false;
                    return;
                }
                flying = true;
            }

    }
}
