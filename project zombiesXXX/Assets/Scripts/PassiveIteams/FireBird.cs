using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBird : PassiveIteam
{
    [SerializeField] GameObject FIRE;
    [SerializeField] float DamageCooldown = 1;
    float t;
    GameObject effect;

    public override void OnUpdate(PlayerState playerState)
    {
        t -= Time.deltaTime;

        GameObject target = playerState.Player.findTarget.Target;

        if (target == null)
        {
            Destroy(effect);
        }

        if (t > 0)
            return;
        t = DamageCooldown;

        DamageData damage = new DamageData();
        damage.DamageAmount = 1;

        if (target != null)
        {
            target.GetComponent<IDamageAble>().TakeDamage(damage);
            if (effect == null)
                effect = Instantiate(FIRE, target.transform);
            effect.transform.localPosition = Vector3.zero;
        }

        base.OnUpdate(playerState);
    }
}
