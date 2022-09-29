using System;
using UnityEngine;

public class SaftyCircle : AbilityPowerUps 
{
    public static SaftyCircle Singiliton;
    public float CritialAmount = 1.75f;
    [SerializeField] GameObject Circle;
    public override void OnPowerUp(Player player)
    {
        base.OnPowerUp(player);

        SaftyCircle.Singiliton.CritialAmount += 0.25f;

        if (isHim)
            return;

        GetPlayer().abilityPowerUps.Remove(this); 
        Destroy(this.gameObject);
    }
    public override Damage DamaModifayer(Damage damage, Transform target, IHpValue hpValue)
    {
        if (isHim == false) { damage.type = DamageType.AD; return damage; }

        if ( Vector2.Distance(GetPlayer().Body.position , target.position ) > 1.45f ) { damage.type = DamageType.AD; return damage; }
        
        if ( damage.AdDamage > damage.ApDamage )
        {
            damage.type = DamageType.Critial;
            damage.AdDamage *= CritialAmount;
            return damage;
        }
        
        
        damage.ApDamage *= CritialAmount;

        damage.type = DamageType.Critial; return damage;
    }
    public override void OnFirstTime(Player player)
    {
        base.OnFirstTime(player);
        
        Singiliton = this;
        var circle = Instantiate( Circle , transform.position , Quaternion.identity );

        circle.transform.SetParent( GetPlayer().Body );

        circle.transform.localPosition = Vector3.zero;
    }



}