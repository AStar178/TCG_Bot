using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Movementoutofcombat : IteamPassive
{
    [SerializeField] VisualEffect effect;
    [SerializeField] Vector3 offset;
    [SerializeField] float combatTime = 5;
    float time;
    VisualEffect effectx;
    public override void OnStart(PlayerState playerState)
    {
        effectx = Instantiate(effect , transform.position + offset , Quaternion.identity);
        effectx.transform.SetParent(playerState.Player.PlayerInputSystem.transform);
    }
    public override State OnUpdate(PlayerState playerState , ref State CalculatedValue , ref State state)
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
