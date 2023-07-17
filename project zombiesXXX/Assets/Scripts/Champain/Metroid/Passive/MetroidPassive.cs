using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetroidPassive : IteamPassive
{
    [SerializeField] float JetPackPower = 7.5f;
    [SerializeField] float HelperEnegyCost = 8.5f;
    [SerializeField] float EnergyCost = 5f;
    bool on;
    float holdTimer;
    Rigidbody rb;
    MetroidEnergy energy;
    private float t;

    public override void OnStart(PlayerState playerState)
    {
        rb = playerState.Player.PlayerThirdPersonController.rb;
        energy = playerState.GetComponent<MetroidEnergy>();
    }

    public override State OnUpdate(PlayerState playerState , ref State Ctate , ref State state)
    {

        if (playerState.Player.PlayerInputSystem.JumpValue == 1)
        {
            holdTimer += Time.deltaTime;

            if (holdTimer > .2f && rb.velocity.y <= 0 && energy.Energy > 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, playerState.Player.PlayerState.ResultValue.JumpAmount * .5f, rb.velocity.z);
                playerState.Player.PlayerEffect.JumpSomeTimeThing();
                energy.DamageEnergy(HelperEnegyCost);
            }

            if (holdTimer > .2f && energy.Energy > 0)
            {
                rb.velocity += new Vector3(0,
                    (JetPackPower + playerState.Player.PlayerState.ResultValue.JumpAmount * .1f) * Time.deltaTime, 0);
                if (on == false)
                {
                    playerState.Player.PlayerEffect.TurnOnJectPackEffect();
                    on = true;
                }

                if (t >= .2f)
                { energy.DamageEnergy(EnergyCost); t = 0; }
                else
                    t += Time.deltaTime;
                return state;
            }
            if (on == true)
            {
                on = false;
                playerState.Player.PlayerEffect.TurnOffJectPackEffect();
            }
            return state;
        }
        else
        {
            holdTimer = 0;
        }

        if (on == true)
        {
            playerState.Player.PlayerEffect.TurnOffJectPackEffect();
            on = false;
            holdTimer = 0;
        }

        if (playerState.Player.PlayerThirdPersonController.Grounded)
        {
            on = false;
        }
            
        return state;
    }
}
