using System;
using UnityEngine;

public class Meleewepos : AbilityWeapons 
{
    [SerializeField] float Raduis = 1.4f;
    [SerializeField] Sprite sprite;
    bool canAttack;
    public override bool CoustomTargetSelect( Transform target , out Transform CostumTarget )
    {
        Vector2 dir = ChooseDir();
        if ( canAttack == false ) { CostumTarget = target; return false; }
        if ( Vector2.Dot( (Vector2)( target.position - transform.position ).normalized , dir ) < 0.1f ) { CostumTarget = CoustomTargetSelecting(); return CostumTarget != null ? true : false; }
        CostumTarget = target; 
        return true;
    }

    private Transform CoustomTargetSelecting()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll( transform.position , Raduis , GetTarget().EnemyLayer );


        for (int i = 0; i < collider2Ds.Length; i++)
        {
            if ( Vector2.Dot( (Vector2)( collider2Ds[i].transform.position - transform.position ).normalized , ChooseDir() ) > 0.1f )
                return collider2Ds[i].transform;
        }

        return null;
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
        damage.type = GetPlayer().DamageModifayer( enemyHp , pos , damage );
        enemyHp.HpValueChange(damage , out var result);


        var s = Instantiate(GetPWM().OnMeeleHit , pos.position , Quaternion.identity);
        
        GetPWM().OnDealDamage( ((int)damage.AdDamage + (int)damage.ApDamage)  , pos.position , damage.type , result );
        Destroy(s , 6);
    }

    private Vector2 ChooseDir()
    {
        if (GetPlayer().PlayerMoveMent.SpriteRenderer.flipX == false)
            return Vector2.right;

        return Vector2.left;
    }
}