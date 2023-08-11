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
    public override void OnStart(PlayerState playerState)
    {
        playerState.ShowForwardIndecater = true;
    }
    public override void OnUseSkill(PlayerState playerState)
    {
        if (Cooldown <= 0)
        {
            if (DistanceCheakPlayerCameraRayCast(Range) == false)
                return;

            Cooldown = CooldownSet;
            Icons.SetCooldown(Cooldown, CooldownSet);
            
            playerState.Player.PlayerThirdPersonController.rb.position = raycastHit.point;
        }

        base.OnUseSkill(playerState);
    }

    public void Update()
    {
        Icons.SetIconMode( DistanceCheakPlayerCameraRayCast(Range) );
        if (Cooldown > 0)
        {
            Cooldown -= Time.deltaTime;
            Icons.SetCooldown(Cooldown, CooldownSet);
        }
    }

}
