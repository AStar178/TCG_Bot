using System;
using UnityEngine;

public class Meleewepos : AbilityWeapons 
{
    [SerializeField] float Raduis = 1.4f;
    [SerializeField] Sprite sprite;
    bool canAttack;
    public override bool CoustomTargetSelect(Transform target)
    {
        Vector2 dir = ChooseDir();
        if ( Vector2.Dot( (Vector2)( target.position - transform.position ).normalized , dir ) < 0.1f ) { return false; }
        if ( canAttack == false ) { return false; } 
        return true;
    }
    public override void UpdateAbilityWp()
    {
        if ( Input.GetKeyDown( KeyCode.Space ) )
            canAttack = true;
        
        if ( Input.GetKeyUp( KeyCode.Space ) )
            canAttack = false;
    }
    public override void StartAbilityWp(Player newplayer)
    {
        base.StartAbilityWp(newplayer);

        GetPlayer().PlayerTarget.Raduis = Raduis;
        GetPlayer().PlayerMoveMent.SpriteRenderer.sprite = sprite;
    }
    public override void DealDamage(IHpValue enemyHp, Transform pos)
    {
        if (GetPWM().attackSpeed > 0) { return; }
        

        GetPWM().attackSpeed = 100/GetPWM().AttackSpeed;
        Damage damage = new Damage();

        damage.AdDamage = GetPWM().DamageAd;
        damage.ApDamage = GetPWM().DamageAp;
        damage.Ad_DefenceReduser = GetPWM().AmoroReduse;
        damage.ApDamage = GetPWM().MagicReduse;
        damage.PlayerRefernce = GetPlayer();

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