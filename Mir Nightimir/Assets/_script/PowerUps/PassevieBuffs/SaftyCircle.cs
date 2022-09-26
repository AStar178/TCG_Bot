using System;
using UnityEngine;

public class SaftyCircle : AbilityPowerUps 
{
    public static SaftyCircle Singiliton;
    public float CritialAmount = 1.75f;
    [SerializeField] GameObject Circle;
    float LastBuff;
    public override void OnPowerUp(Player player)
    {
        base.OnPowerUp(player);

        SaftyCircle.Singiliton.CritialAmount += 0.25f;
    }
    public override Damage DamaModifayer(Damage damage, Transform target, IHpValue hpValue)
    {
        if (stated == false) { damage.type = DamageType.AD; return damage; }

        if ( Vector2.Distance(GetPlayer().Body.position , target.position ) > 1.45f ) { damage.type = DamageType.AD; return damage; }

        damage.AdDamage *= CritialAmount;
        damage.ApDamage *= CritialAmount;

        damage.type = DamageType.AD; return damage;
    }
    bool stated;
    public override void OnFirstTime(Player player)
    {
        base.OnFirstTime(player);
        stated = true;
        Singiliton = this;
        var circle = Instantiate( Circle , transform.position , Quaternion.identity );

        circle.transform.SetParent( GetPlayer().Body );

        circle.transform.localPosition = Vector3.zero;
    }



}