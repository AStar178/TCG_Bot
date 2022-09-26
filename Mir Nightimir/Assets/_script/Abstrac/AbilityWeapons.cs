using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class AbilityWeapons : MonoBehaviour 
{
    public string WeaponName;
    private Player player;
    public Player GetPlayer() => player;
    public Damage CreatDamage( float ad, float ap, float amoroReduse, float magicReduse ) => GetPlayer().CreatDamage( ad , ap , amoroReduse , magicReduse , GetPlayerTargetSelector().target);
    public PlayerWeaponManger GetWeaponManger() => player.PlayerWeaponManger;
    public PlayerTarget GetPlayerTargetSelector() => player.PlayerTarget;
    public bool rotationLeftSprite;
    public WeaponType Type;
    [SerializeField] protected float Raduis;
    protected float Tau => 6.28318530718f;
    protected float PI => MathF.PI;
    [Range(0.3f , 6)] [SerializeField] protected float FriendZoon;
    public virtual Transform CoustomTargetSelect()
    {
        if (Type == WeaponType.Range)
        {
            var CostumTarget2 = CoustomTargetSelectingRange( Raduis ); 

            return CostumTarget2;
        }

        var CostumTarget = CoustomTargetSelectingMelee( Raduis , FriendZoon ); 

        return CostumTarget;
    }

    public virtual void StartAbilityWp(Player newplayer)
    {
        player = newplayer;
    }

    public virtual void UpdateAbilityWp()
    {

    }

    public virtual bool CoustomRouls(Transform target)
    {
        if (Type == WeaponType.Range)
        {

            return CostumRoulsRangDefulit(target);
        }


        return CostumRoulsMeleeDefulit(target);
    }

    public virtual void DealDamage(IHpValue enemyHp , Transform pos)
    {
        
    }
    public virtual void AbilityWeaponsUseAbility()
    {
        
    }
    public virtual void StopAbilityWp()
    {
        Destroy(this.gameObject);
    }
    protected virtual Transform CoustomTargetSelectingMelee(float Raduis , float FrindZoon)
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll( transform.position , Raduis , GetPlayerTargetSelector().EnemyLayer );

        Collider2D coinsAmount = collider2Ds.Where( n => {

           if ( Vector2.Distance( transform.position , n.transform.position ) < FrindZoon )
                return true; 
            
            return Vector2.Dot( (Vector2)( n.transform.position - transform.position ).normalized , ChooseDir() ) > 0.1f;
        })
        .OrderBy( n => (n.transform.position - transform.position).sqrMagnitude).FirstOrDefault();
        

        if (coinsAmount != null)
            return coinsAmount.transform;

        return null;
    }
    protected virtual bool CostumRoulsMeleeDefulit( Transform target )
    {
        if ( Vector2.Distance( transform.position , target.transform.position ) < FriendZoon )
            return true; 

        if ( Vector2.Dot( (Vector2)( target.transform.position - transform.position ).normalized , ChooseDir() ) > 0.1f )
            return true;

        return false;

    }
    protected virtual bool CostumRoulsRangDefulit( Transform target )
    {
        return true;
    }
    protected virtual Transform CoustomTargetSelectingRange( float Raduis )
    {
        List<Collider2D> collider2Ds = new List<Collider2D>(); 
        collider2Ds.AddRange ( Physics2D.OverlapCircleAll( transform.position , Raduis , GetPlayerTargetSelector().EnemyLayer ) );

        Collider2D colosed = collider2Ds.OrderBy( t => ( ( t.transform.position - transform.position ).sqrMagnitude ) ).FirstOrDefault();
        if (colosed != null)
        {
            return colosed.transform;
        }

        return null;
    }
    
    protected Vector2 ChooseDir()
    {
        if (rotationLeftSprite == false) {
            if (GetPlayer().PlayerMoveMent.SpriteRenderer.flipX == false)
                return Vector2.right;

            return Vector2.left;
        }
        if (GetPlayer().PlayerMoveMent.SpriteRenderer.flipX == false)
            return Vector2.left;

        return Vector2.right;

    }
}

public enum WeaponType
{
    Melee ,
    Range
}