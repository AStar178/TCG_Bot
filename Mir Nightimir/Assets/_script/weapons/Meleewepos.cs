using System;
using UnityEngine;

public class Meleewepos : AbilityWeapons 
{
    [SerializeField] Sprite sprite;
    bool canAttack;

    public override Transform CoustomTargetSelect()
    {
        if ( !canAttack )
            return null;

        return CoustomTargetSelectingMelee( Raduis , FriendZoon );
    }

    public override void GetSprite()
    {
        image = sprite;
    }


    public override bool CoustomRouls(Transform target)
    {
        return CostumRoulsMeleeDefulit(target);
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
        if (GetWeaponManger().attackSpeed > 0) { return; }

        GetWeaponManger().attackSpeed = 100/GetWeaponManger().AttackSpeed;
        var damage = CreatDamage( GetWeaponManger().DamageAd , GetWeaponManger().DamageAp , GetWeaponManger().AmoroReduse , GetWeaponManger().MagicReduse );
        enemyHp.HpValueChange(damage , out var result);


        var s = Instantiate(GetWeaponManger().OnMeeleHit , pos.position , Quaternion.identity);
        
        GetWeaponManger().OnDealDamage( ((int)damage.AdDamage + (int)damage.ApDamage)  , pos.position , damage.type , result );
        Destroy(s , 6);
    }

}