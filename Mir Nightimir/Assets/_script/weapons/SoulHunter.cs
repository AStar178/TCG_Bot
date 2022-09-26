using System;
using UnityEngine;

public class SoulHunter : AbilityWeapons 
{
    [SerializeField] Sprite sprite;
    [SerializeField] float MDeathTimer = 30;
    bool canAttack;

    public override Transform CoustomTargetSelect()
    {
        if (!canAttack)
            return null;


        return CoustomTargetSelectingMelee( Raduis , FriendZoon );
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
        Damage damage = new Damage();

        damage = CreatDamage( GetWeaponManger().DamageAd / 2 , GetWeaponManger().DamageAp / 2 , GetWeaponManger().AmoroReduse , GetWeaponManger().MagicReduse );
        print ( damage.ApDamage );
        enemyHp.HpValueChange(damage , out var result);
        

        var s = Instantiate(GetWeaponManger().OnMeeleHit , pos.position , Quaternion.identity);
        
        GetWeaponManger().OnDealDamage( ((int)damage.AdDamage + (int)damage.ApDamage)  , pos.position , damage.type , result );
        Destroy(s , 6);

        if (0.75 < UnityEngine.Random.value)
        {
            GameObject b = Instantiate(EnemyStatic.soulHunterMinios, gameObject.transform);
            b.GetComponent<SoulHunterMinions>().player = gameObject;
            b.GetComponent<SoulHunterMinions>().SetAttackCooldown = GetPlayer().PlayerWeaponManger.AttackSpeed / 100;
            b.GetComponent<SoulHunterMinions>().damage = Rpg.CreatDamage(GetWeaponManger().DamageAd, GetWeaponManger().DamageAp, GetWeaponManger().AmoroReduse, GetWeaponManger().MagicReduse , GetPlayer() , default , GetPlayerTargetSelector().target.transform);
            b.GetComponent<SoulHunterMinions>().TimeToDeath = MDeathTimer;
        }
    }

}