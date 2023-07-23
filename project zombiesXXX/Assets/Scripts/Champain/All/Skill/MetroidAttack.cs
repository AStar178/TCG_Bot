using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetroidAttack : IteamSkill
{
    [SerializeField] float AttackSpeed;
    float t;
    public override void OnUseSkill(PlayerState playerState)
    {

        if (t > 0)
            return;
        if (playerState.Player.PlayerTargetSystem.Target == null)
            return;

        var damage = CreatDamage( playerState.ResultValue.Damage , playerState , out var crited );
        playerState.Player.PlayerTargetSystem.Target.GetComponent<IDamageAble>().TakeDamage(damage);
        InCombat();
        t = AttackSpeed * (1/(playerState.ResultValue.AttackSpeed+1));
        playerState.OnAtuoAttackDealDamage?.Invoke(damage);
        playerState.Player.PlayerEffect.Shooteffect.Play();
        base.OnUseSkill(playerState);

    }
    private void Update() {
        
        t -= Time.deltaTime;

    }
}
