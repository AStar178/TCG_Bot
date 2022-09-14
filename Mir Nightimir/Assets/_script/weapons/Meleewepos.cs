using System;
using UnityEngine;

public class Meleewepos : AbilityWeapons 
{
    public override void DealDamage(IHpValue enemyHp, Transform pos)
    {
        if (GetPWM().attackSpeed > 0) { return; }
        Vector2 dir = ChooseDir();
        if ( Vector2.Dot( (Vector2)(pos.position - transform.position).normalized , dir ) < 0.1f ) { return; } 

        GetPWM().attackSpeed = 100/GetPWM().AttackSpeed;
        Damage damage = new Damage();

        damage.AdDamage = GetPWM().DamageAd;
        damage.ApDamage = GetPWM().DamageAp;
        damage.Ad_DefenceReduser = GetPWM().AmoroReduse;
        damage.ApDamage = GetPWM().MagicReduse;

        enemyHp.HpValueChange(damage);
        var s = Instantiate(GetPWM().OnMeeleHit , pos.position , Quaternion.identity);
        bool pornOnline = false;
        if (damage.AdDamage < damage.ApDamage)
            pornOnline = true;
        GetPWM().OnDealDamage( ((int)damage.AdDamage + (int)damage.ApDamage)  , pos.position , pornOnline );
        Destroy(s , 6);
    }

    private Vector2 ChooseDir()
    {
        if (GetPlayer().PlayerMoveMent.SpriteRenderer.flipX == false)
            return Vector2.right;

        return Vector2.left;
    }
}