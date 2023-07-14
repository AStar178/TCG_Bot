using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBird : PassiveIteam
{
    [SerializeField] ParticleSystem FIRE;
    [SerializeField] float DamageCooldown = 1;
    float t;
    ParticleSystem effect;

    public override void OnStart(PlayerState playerState)
    {
        effect = Instantiate(FIRE , transform.position , Quaternion.identity);
    }

    

    public override void OnUpdate(PlayerState playerState)
    {
        t -= Time.deltaTime;

        GameObject target = playerState.Player.findTarget.Target;

        if (target == null)
        {
            effect.Stop();
        }

        if (t > 0)
            return;
        t = DamageCooldown;

        DamageData damage = new DamageData();
        damage.DamageAmount = (playerState.ResultValue.Damage / 2 + Scaling());

        if (target != null)
        {
            target.GetComponent<IDamageAble>().TakeDamage(damage);
            effect.Play();
            effect.transform.SetParent(target.transform);
            effect.transform.localPosition = Vector3.zero;
        }

        base.OnUpdate(playerState);
    }
}
