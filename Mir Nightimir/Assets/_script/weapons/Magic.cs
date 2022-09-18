using System;
using UnityEngine;

public class Magic : AbilityWeapons 
{
    [SerializeField] float Raduis = 2.1f;
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
        if (GetPWM().attackSpeed > 0) { return; }
        

        GetPWM().attackSpeed = 100/GetPWM().AttackSpeed * 2f;


        var Bullet = Instantiate(MagicBullit , spawnPos.position , Quaternion.identity);
        var cp = Bullet.GetComponent<MagicBullent>();
        cp.magic = this;
        cp.target = pos;
    }

}