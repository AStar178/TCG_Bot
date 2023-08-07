using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetroidAttack : IteamSkill
{
    [SerializeField] float AttackSpeed;
    float t;
    [SerializeField] float EnergyPerHit;
    private MetroidEnergy MetroidEnergy;

    public override void OnStart(PlayerState playerState)
    {
        MetroidEnergy = playerState.GetComponent<MetroidEnergy>();
        base.OnStart(playerState);
    }

    public override void OnUseSkill(PlayerState playerState)
    {

        if (t > 0)
            return;
        if (playerState.Player.PlayerTargetSystem.Target == null)
            return;

        var damage = CreatDamage( playerState.ResultValue.Damage , playerState , out var crited );
        var enemy = playerState.Player.PlayerTargetSystem.Target.GetComponent<EnemyHp>();
        enemy.TakeDamage(damage);
        InCombat();
        t = AttackSpeed * (1/(playerState.ResultValue.AttackSpeed+1));
        playerState.OnAtuoAttackDealDamage?.Invoke(damage , enemy);
        playerState.OnAbilityAttackDealDamage?.Invoke(damage , enemy);
        MetroidEnergy.RestoreEnergy(EnergyPerHit);
        MetroidEffect.Current.Shooteffect.Play();
        base.OnUseSkill(playerState);

    }
    private void Update() {
        
        t -= Time.deltaTime;

    }
}
