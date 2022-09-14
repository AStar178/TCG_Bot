using System;
using UnityEngine;

public abstract class AbilityWeapons : MonoBehaviour 
{
    private Player player;
    protected Player GetPlayer() => player;
    protected PlayerWeaponManger GetPWM() => player.PlayerWeaponManger;
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

    public virtual void StopAbilityWp()
    {
        Destroy(this.gameObject);
    }
}