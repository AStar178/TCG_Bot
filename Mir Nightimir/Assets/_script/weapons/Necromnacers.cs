using System;
using UnityEngine;
using System.Linq;

public class Necromnacers : AbilityWeapons 
{
    [SerializeField] float ManaCoust;
    [SerializeField] Sprite sprite;
    [SerializeField] GameObject MagicBullit;
    [SerializeField] Transform spawnPos;
    [SerializeField] LayerMask graves;
    public override void StartAbilityWp(Player newplayer)
    {
        base.StartAbilityWp(newplayer);

        GetPlayer().PlayerTarget.Raduis = Raduis;
        GetPlayer().PlayerMoveMent.SpriteRenderer.sprite = sprite;
    }

    public override void DealDamage( IHpValue enemyHp , Transform pos )
    {
        if (GetWeaponManger().attackSpeed > 0) { return; }
        

        GetWeaponManger().attackSpeed = 100/GetWeaponManger().AttackSpeed * 3f;


        var Bullet = Instantiate(MagicBullit , spawnPos.position , Quaternion.identity);
        var cp = Bullet.GetComponent<MagicBullent>();
        Destroy(Bullet , 10);
        cp.magic = this;
        cp.target = pos;
    }

    public override void AbilityWeaponsUseAbility()
    {
        if (GetWeaponManger().CurrentMana < ManaCoust) { GetWeaponManger().CreatCoustomTextPopup( "More Mana Needed" , transform.position , Color.blue); return; }

        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll( transform.position , 5 , graves );
        if (collider2Ds.Length == 0) { GetWeaponManger().CreatCoustomTextPopup( "No Mionuse Found" , transform.position , Color.red); return; }
        GetWeaponManger().CurrentMana -= ManaCoust;
        GetPlayer().UpdateUI();

        for (int i = 0; i < collider2Ds.Length; i++)
        {
            GetWeaponManger().CreatCoustomTextPopup( "Mionuse Rise For MEEE" , transform.position , Color.red);

            if (collider2Ds[i].TryGetComponent<EnemyHp>( out var enemyHp ))
            {
                enemyHp.NecromanserISHAHAHAH( GetPlayer().PlayerTarget.EnemyLayer );
            }
        }  
        GetPlayer().PlayerTarget.target = null;
    }

}