using System;
using UnityEngine;

public abstract class AbilityWeapons : MonoBehaviour 
{
    public string WeaponName;
    private Player player;
    public Player GetPlayer() => player;
    public PlayerWeaponManger GetWeaponManger() => player.PlayerWeaponManger;
    public PlayerWeaponManger GetPWM() => player.PlayerWeaponManger;
    public PlayerTarget GetTarget() => player.PlayerTarget;
    public bool rotationLeftSprite;
    public virtual bool CoustomTargetSelect( Transform target , out Transform CostumTarget )
    {
        CostumTarget = target;
        return true;
    }
    public virtual Transform SelectedTarget(float Raduis , LayerMask EnemyLayer)
    {
        Collider2D collider2D = Physics2D.OverlapCircle( transform.position , Raduis , EnemyLayer );

        if (collider2D != null)
        {
            return collider2D.gameObject.transform;
        }        
        return null;
    }
    public virtual void StartAbilityWp(Player newplayer)
    {
        player = newplayer;
    }
    public virtual void UpdateAbilityWp()
    {

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
}