using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SoulHunter : AbilityWeapons
{
    [SerializeField] Sprite sprite;
    [Header("Skill")]
    [SerializeField] float SummonChance = 17;
    [SerializeField] float MDeathTimer = 30;
    public List<SoulHunterMinions> SoulsDominions;
    public int MinionCap = 0;
    [SerializeField] private int SetBeforeSkillUp = 10;
    [SerializeField] private int SkillUpAddAmount = 1;
    [Header("Minions Damage")]
    public float DevidedAmount = 1f;
    public float MultyAmount = 1f;
    public float PlusAmount = 0f;
    int levelBeforeUpdate;
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

        levelBeforeUpdate = SetBeforeSkillUp - 1;
        GetPlayer().PlayerTarget.Raduis = Raduis;
        GetPlayer().PlayerMoveMent.SpriteRenderer.sprite = sprite;
    }
    public override void DealDamage(IHpValue enemyHp, Transform pos)
    {
        if (GetWeaponManger().attackSpeed > 0) { return; }
        

        GetWeaponManger().attackSpeed = 100/GetWeaponManger().AttackSpeed;
        Damage damage = new Damage();

        damage = CreatDamage( GetWeaponManger().DamageAd , GetWeaponManger().DamageAp , GetWeaponManger().AmoroReduse , GetWeaponManger().MagicReduse );
        print ( damage.ApDamage );
        enemyHp.HpValueChange(damage , out var result);
        

        var s = Instantiate(GetWeaponManger().OnMeeleHit , pos.position , Quaternion.identity);
        
        GetWeaponManger().OnDealDamage( ((int)damage.AdDamage + (int)damage.ApDamage)  , pos.position , damage.type , result );
        Destroy(s , 6);
        
        if (SoulsDominions.Count < MinionCap)
        {
            if (UnityEngine.Random.Range(1, 100) <= SummonChance)
            {
                GameObject b = Instantiate(AIStatic.soulHunterMinios, gameObject.transform);
                b.GetComponent<SoulHunterMinions>().player = gameObject;
                b.GetComponent<SoulHunterMinions>().Master = this;
                b.GetComponent<SoulHunterMinions>().SetAttackCooldown = GetPlayer().PlayerWeaponManger.AttackSpeed / 100;
                b.GetComponent<SoulHunterMinions>().TimeToDeath = MDeathTimer;
                var cp = b.GetComponent<SoulHunter>();
                cp.MultyAmount = MultyAmount;
                cp.DevidedAmount = DevidedAmount;
                cp.PlusAmount = PlusAmount;
                SoulsDominions.Add(b.GetComponent<SoulHunterMinions>());
            }
        }
    }

    public override void OnLevelUp()
    {
        levelBeforeUpdate--;

        if (levelBeforeUpdate <= 0)
        {
            levelBeforeUpdate += SetBeforeSkillUp;
            MinionCap += SkillUpAddAmount;
            AIStatic.CreatCoustomTextPopup("Can summon up to " + MinionCap + " Souls", GetPlayer().Body.transform.position, Color.green);
        }
    }

}