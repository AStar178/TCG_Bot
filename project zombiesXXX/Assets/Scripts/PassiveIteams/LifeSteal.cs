using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSteal : IteamPassive
{
    public float LifePercSet;
    public float LifePerc;

    public override void OnStart(PlayerState playerState)
    {
        LifePerc = 1 + LifePercSet / 100;
        base.OnStart(playerState);
    }

    public override void OnLevelUp(PlayerState playerState)
    {
        LifePerc = (1 * level) + (LifePercSet * level) / 100;
        base.OnLevelUp(playerState);
    }

    private void OnEnable()
    {
        PlayerState.OnAtuoAttackDealDamage += Heal;
    }

    private void OnDisable()
    {
        PlayerState.OnAtuoAttackDealDamage -= Heal;
    }

    public void Heal(DamageData data, EnemyHp enemyHp)
    {
        DamageData Hp = CreatDamageWithOutCrit(data.DamageAmount * LifePerc, PlayerState);

        PlayerState.GetComponent<PlayerHp>().Heal(Hp);
        PlayerState.OnHeal(data);
    }
}
