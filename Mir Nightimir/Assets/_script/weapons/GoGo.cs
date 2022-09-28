using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GoGo : AbilityWeapons
{
    [SerializeField] Sprite sprite;
    public Spirit Spit;
    [SerializeField] private int SetBeforeSkillUp = 3;
    [SerializeField] private int SetSkillUpAmount = 1;
    [HideInInspector] public int SkillUpAddAmount;
    int levelBeforeUpdate;
    [HideInInspector]
    public bool attacking;
    public float StandAggroRange = 2;
    float counter;
    bool canAttack;

    public override Transform CoustomTargetSelect()
    {
        if (!canAttack)
            return null;


        return CoustomTargetSelectingMelee( Raduis , FriendZoon );
    }

    public override void GetSprite()
    {
        image = sprite;
    }


    public Transform GetTarget() => CoustomTargetSelectingMelee(Raduis , FriendZoon );


    public override void UpdateAbilityWp()
    {
        if (Spit != null)
        {
            if (GetPlayer().PlayerHp.Currenthp <= GetPlayer().PlayerHp.MaxHp * .1f)
                Spit.Retreat(); 
            
            if (attacking == true)
            {
                counter -= Time.deltaTime;
                if (counter <= 0)
                {
                    attacking = false;
                    if (Spit != null)
                        Spit.JojoTarget = null;
                }
            }
        }

        if ( Input.GetKeyDown( KeyCode.Space ) )
            canAttack = true;
        
        if ( Input.GetKeyUp( KeyCode.Space ) )
            canAttack = false;
    }
    public override void StartAbilityWp(Player newplayer)
    {
        base.StartAbilityWp(newplayer);

        levelBeforeUpdate = SetBeforeSkillUp - 1;
        SkillUpAddAmount = 0;
        GetPlayer().PlayerTarget.Raduis = Raduis;
        GetPlayer().PlayerMoveMent.SpriteRenderer.sprite = sprite;
    }
    public override void DealDamage(IHpValue enemyHp, Transform pos)
    {
        attacking = true;
        if (Spit != null)
            Spit.JojoTarget = pos;

        if (GetWeaponManger().attackSpeed > 0) { return; }
        

        GetWeaponManger().attackSpeed = 100/GetWeaponManger().AttackSpeed;
        Damage damage = new Damage();

        damage = GetDamage();
        print ( damage.ApDamage );
        enemyHp.HpValueChange(damage , out var result);
        

        var s = Instantiate(GetWeaponManger().OnMeeleHit , pos.position , Quaternion.identity);
        
        GetWeaponManger().OnDealDamage( ((int)damage.AdDamage + (int)damage.ApDamage)  , pos.position , damage.type , result );
        Destroy(s , 6);
    }

    public Damage GetDamage()
    {
        return CreatDamage(GetWeaponManger().DamageAd, GetWeaponManger().DamageAp, GetWeaponManger().AmoroReduse, GetWeaponManger().MagicReduse);
    }

    public override void OnLevelUp()
    {
        levelBeforeUpdate--;

        if (levelBeforeUpdate <= 0)
        {
            levelBeforeUpdate += SetBeforeSkillUp;
            EnemyStatic.CreatCoustomTextPopup("PowerUp! " + (15 + SkillUpAddAmount) * 10 + "% more Spirit Power", GetPlayer().Body.transform.position, Color.red);
            SkillUpAddAmount += SetSkillUpAmount;
            print("PowerUp! " + (15 + SkillUpAddAmount) * 10 + "% more Spirit Power");
        }
    }

    public override void AbilityWeaponsUseAbility()
    {
        if (GetPlayer().PlayerHp.Currenthp > GetPlayer().PlayerHp.MaxHp * .1f)
        {
            if (Spit == null)
            {
                GameObject b = Instantiate(EnemyStatic.stand, GetPlayer().Body.transform);
                b.transform.localScale = new Vector3(0, 0, 0);
                b.transform.DOMove(new Vector3 (GetPlayer().Body.transform.position.x + .9f, transform.position.y, transform.position.z), 0);
                b.GetComponent<Spirit>().player = GetPlayer().Body.gameObject;
                b.GetComponent<Spirit>().Jojo = this;
                b.GetComponent<Spirit>().Spawn();
                Spit = (b.GetComponent<Spirit>());
            }
            else
            {
                Spit.Retreat();
            }
        }
    }

}