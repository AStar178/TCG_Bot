using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Blink : IteamSkill
{
    [SerializeField]
    private float Range;

    [SerializeField]
    private float CooldownSet;
    private float Cooldown;

    public override void OnUseSkill(PlayerState playerState)
    {
        if (Cooldown <= 0)
        {
            var Camerax = Camera.main;
            var ray = Physics.Raycast(Camerax.transform.position, Camerax.transform.forward, out var hs, Range, GroundLayer);
            if (hs.collider == null)
                return;
            Cooldown = CooldownSet;
            Icons.SetCooldown(Cooldown, CooldownSet);

            playerState.Player.PlayerThirdPersonController.rb.position = hs.point;
        }

        base.OnUseSkill(playerState);
    }

    public void Update()
    {
        if (Cooldown > 0)
        {
            Cooldown -= Time.deltaTime;
            Icons.SetCooldown(Cooldown, CooldownSet);
        }
    }

}
