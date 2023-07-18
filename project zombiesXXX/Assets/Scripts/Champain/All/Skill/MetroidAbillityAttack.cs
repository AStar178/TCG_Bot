using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetroidAbillityAttack : IteamSkill
{
    [SerializeField] float DashSpeed;
    [SerializeField] float EnergyCost;
    [Tooltip("the lower the more it drain energy; 1 = 1 sec etc.")]
    [SerializeField] float EnergyCostTick;

    private PlayerState playerState;
    private MetroidEnergy energy;
    private Vector2 Input;

    private float t;
    private bool on;

    public override void OnStart(PlayerState playerState)
    {
        base.OnStart(playerState);
        this.playerState = playerState;
        energy = playerState.GetComponent<MetroidEnergy>();
    }

    public override void OnUseSkill(PlayerState playerState)
    {
        base.OnUseSkill(playerState);
    }

    public void Update()
    {
        if (playerState.Player.PlayerInputSystem.move != Vector2.zero)
            Input = playerState.Player.PlayerInputSystem.move;

        if (playerState.Player.PlayerInputSystem.EValue == 1 && energy.Energy >= EnergyCost && playerState.Player.PlayerInputSystem.move != Vector2.zero)
        {
            t += Time.deltaTime;

            Vector3 goDir = new Vector3(Input.x, 0, Input.y) * (DashSpeed + (playerState.ResultValue.SprintSpeed * 2));
            goDir.y = .2f;

            playerState.Player.PlayerThirdPersonController.rb.velocity = goDir;

            if (t >= EnergyCostTick)
            { energy.DamageEnergy(EnergyCost); t = 0; }

            if (on == false)
            {
                playerState.Player.PlayerEffect.TurnOnJectPackEffect();
                on = true;
            }
            return;
        }

        if (on == true)
        {
            playerState.Player.PlayerEffect.TurnOffJectPackEffect();
            on = false;
        }
    }
}