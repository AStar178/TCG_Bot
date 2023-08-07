using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGravityHalve : IteamSkill
{
    public float CooldownSet;
    private float Cooldown;
    public float ActiveTimeSet;
    private float ActiveTime;
    bool active;
    public GameObject Effect;
    private GameObject effect;

    public Vector3 gravityDevide;
    private Vector3 OriginalGravity;

    public override void OnStart(PlayerState playerState)
    {
        base.OnStart(playerState);
        OriginalGravity = Physics.gravity;
    }

    public override void OnUseSkill(PlayerState playerState)
    {
        base.OnUseSkill(playerState);


        if (Cooldown <= 0)
        {
            Cooldown = CooldownSet;
            ActiveTime = ActiveTimeSet;
            Physics.gravity = new Vector3 (0, Physics.gravity.y * gravityDevide.y, 0);
            effect = Instantiate(Effect, playerState.transform);
            active = true;
        }
    }

    public void Update()
    {
        if (Cooldown > 0)
            Cooldown -= Time.deltaTime;

        if (ActiveTime < 0 && active)
        {
            Physics.gravity = OriginalGravity;
            Destroy(effect);
            active = false;
        }
        else
            ActiveTime -= Time.deltaTime;
    }
}
