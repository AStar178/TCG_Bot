using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testo : SkillManager
{
    public override void OnSkill_1()
    {
        base.OnSkill_1();

        Damage damage = RPG.CreateDamage(3, DamageType.Fire);
        RPG.CreateBullet(transform, RougeLiter.rougeLiter.ObjectHolder.FireBullet, RougeLiter.rougeLiter.ObjectHolder.FireOnHit, damage, 3, 10);
        Debug.Log("I am CRAZYYYYYYYYYYYYYYYYY");
    }
}
