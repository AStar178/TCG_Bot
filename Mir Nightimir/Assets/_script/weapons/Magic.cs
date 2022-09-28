using System;
using UnityEngine;

public class Magic : AbilityWeapons 
{
    [SerializeField] Sprite sprite;
    [SerializeField] GameObject MagicBullit;
    [SerializeField] Transform spawnPos;
    [SerializeField] int amount = 3;
    [SerializeField] private float manaCost;
    [SerializeField] private int SetBeforeSkillUp = 10;
    [SerializeField] private int SkillUpAddAmount = 1;
    int levelBeforeUpdate;

    public override void StartAbilityWp(Player newplayer)
    {
        base.StartAbilityWp(newplayer);

        levelBeforeUpdate = SetBeforeSkillUp - 1;
        GetPlayer().PlayerTarget.Raduis = Raduis;
        GetPlayer().PlayerMoveMent.SpriteRenderer.sprite = sprite;
    }
    public override void DealDamage(IHpValue enemyHp, Transform pos)
    {
        if ( GetWeaponManger().attackSpeed > 0 ) { return; }
        

        GetWeaponManger().attackSpeed = 100/GetWeaponManger().AttackSpeed * 2f;


        var Bullet = Instantiate(MagicBullit , spawnPos.position , Quaternion.identity);
        var cp = Bullet.GetComponent<MagicBullent>();
        cp.magic = this;
        cp.target = pos;
    }
    public override void AbilityWeaponsUseAbility()
    {
        if ( GetPlayerTargetSelector().target == null ) { GetWeaponManger().CreatCoustomTextPopup( "No Target" , transform.position , Color.red ); return; }
        if ( GetWeaponManger().CurrentMana < manaCost ) { GetWeaponManger().CreatCoustomTextPopup( "Ne More Juise" , transform.position , Color.red ); return; }
        GetWeaponManger().CurrentMana -= manaCost;
        GetPlayer().UpdateUI();

        var dir = Rpg.CreatMultipleDir( amount );

        for (int i = 0; i < dir.Count; i++)
        {
            GetWeaponManger().CreatCoustomTextPopup( " Some Magic Wizzard stuff " , transform.position , Color.cyan );
            var Bullet = Instantiate(MagicBullit , spawnPos.position + (Vector3)( dir[i] ) , Quaternion.identity);
            var cp = Bullet.GetComponent<MagicBullent>();
            Destroy(Bullet , 10);
            cp.magic = this;
            cp.target = GetPlayerTargetSelector().target;   
        }

    }

    public override void OnLevelUp()
    {
        levelBeforeUpdate--;

        if (levelBeforeUpdate <= 0)
        {
            levelBeforeUpdate += SetBeforeSkillUp;
            amount += SkillUpAddAmount;
            EnemyStatic.CreatCoustomTextPopup("Can summon " + amount + " more magical staff", GetPlayer().Body.transform.position, Color.green);
        }
    }

}