using UnityEngine;

public class PlayerWeaponManger : MonoBehaviour 
{    
    public float AttackSpeed;
    public float DamageAd;
    public float DamageAp;
    public float AmoroReduse;
    public float MagicReduse;
    float attackSpeed;
    private void Update() 
    {
        if(attackSpeed > 0)
        {
            attackSpeed -= Time.deltaTime;
        }
    
    }

    public void DealDamage(IHpValue enemyHp)
    {
        if (attackSpeed < 0) { return; }

        attackSpeed = 100/AttackSpeed;
        Damage damage = new Damage();

        damage.AdDamage = DamageAd;
        damage.ApDamage = DamageAp;
        damage.Ad_DefenceReduser = AmoroReduse;
        damage.ApDamage = MagicReduse;

        enemyHp.HpValueChange(damage);
    }


}