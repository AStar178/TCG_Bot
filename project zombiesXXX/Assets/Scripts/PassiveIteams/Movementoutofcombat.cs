using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Movementoutofcombat : PassiveIteam
{
    [SerializeField] VisualEffect effect;
    [SerializeField] Vector3 offset;
    [SerializeField] float combatTime = 5;
    float time;
    VisualEffect effectx;
    public override void OnStart(PlayerState playerState)
    {
        effectx = Instantiate(effect , transform.position + offset , Quaternion.identity);
        effectx.transform.SetParent(playerState.Player.findTarget.transform);
    }
    public override State OnUpdateMultiyMYHEADISDIEING(PlayerState playerState, ref State state)
    {
        if (playerState.Combat)
        {
            effectx.Stop();
            time = combatTime;

            return state;
        }
        time -= Time.deltaTime;    
        if (time < 0)
        {
            effectx.Play();
            state.SprintSpeed = state.SprintSpeed * (2 + Scaling());

        }


        return state;
    }
}
