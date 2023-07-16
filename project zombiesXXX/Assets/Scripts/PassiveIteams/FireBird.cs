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
        effect = Instantiate(FIRE , transform.position , Quaternion.identity);
    }


    public override State OnUpdate(PlayerState playerState, ref State CalucatedValue, ref State state)
    {
        t -= Time.deltaTime;

        GameObject target = playerState.Player.PlayerTargetSystem.Target;

        if (target == null)
        {
            effect.Stop();
        }

        if (t > 0)
            return state;
        t = DamageCooldown;

        DamageData damage = CreatDamage(0 , playerState);
        damage.DamageAmount = (playerState.ResultValue.Damage / 2 + Scaling());
        if (target != null)
        {
            target.GetComponent<IDamageAble>().TakeDamage(damage);
            effect.Play();
            effect.transform.SetParent(target.transform);
            effect.transform.localPosition = Vector3.zero;
        }

        return state;
    }
}
