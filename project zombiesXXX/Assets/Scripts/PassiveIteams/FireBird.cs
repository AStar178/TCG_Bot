using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBird : IteamPassive
{
    [SerializeField] ParticleSystem FIRE;
    [SerializeField] float DamageCooldown = 1;
    float t;
    ParticleSystem effect;

    public override void OnStart(PlayerState playerState)
    {

    }


    public override State OnUpdate(PlayerState playerState, ref State CalucatedValue, ref State state)
    {
        t -= Time.deltaTime;

        GameObject target = playerState.Player.PlayerTargetSystem.Target;

        if (target == null)
        {
            if (effect != null)
            {
                effect.Stop();
                Destroy(effect.gameObject, 1);
            }
        }
        if (target != null)
        {
            if (effect == null)
                effect = Instantiate(FIRE, transform.position, Quaternion.identity);
            effect.Play();
            effect.transform.position = target.transform.position;
        }
        if (t > 0)
            return state;
        t = DamageCooldown;
        
        DamageData damage = CreatDamageWithOutCrit(0 , playerState);
        damage.DamageAmount = (playerState.ResultValue.Damage / 2 + Scaling());
        if (target != null)
        {
            InCombat();
            target.GetComponent<IDamageAble>().TakeDamage(damage);
        }

        return state;
    }
}
