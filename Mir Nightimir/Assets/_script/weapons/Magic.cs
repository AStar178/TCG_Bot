using System;
using UnityEngine;

public class Magic : AbilityWeapons 
{
    [SerializeField] Sprite sprite;
    [SerializeField] GameObject MagicBullit;
    [SerializeField] Transform spawnPos;
    public override void StartAbilityWp(Player newplayer)
    {
        base.StartAbilityWp(newplayer);

        GetPlayer().PlayerTarget.Raduis = Raduis;
        GetPlayer().PlayerMoveMent.SpriteRenderer.sprite = sprite;
    }
    public override void DealDamage(IHpValue enemyHp, Transform pos)
    {
        if (GetWeaponManger().attackSpeed > 0) { return; }
        

        GetWeaponManger().attackSpeed = 100/GetWeaponManger().AttackSpeed * 2f;


        var Bullet = Instantiate(MagicBullit , spawnPos.position , Quaternion.identity);
        var cp = Bullet.GetComponent<MagicBullent>();
        cp.magic = this;
        cp.target = pos;
    }

}