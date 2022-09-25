using UnityEngine;

public class PlayerBullent : MonoBehaviour
{
    public Damage damage;
    [SerializeField] GameObject Effect;
    public int EnemyLayer;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if ( other.gameObject.layer != EnemyLayer ) { return; }

        if ( !other.TryGetComponent(out IHpValue hpValue) ) { return; }
        
        hpValue.HpValueChange(damage , out var result);
        
        var s = Instantiate(Effect , other.transform.position , Quaternion.identity);
        damage.PlayerRefernce.PlayerWeaponManger.OnDealDamage( ((int)damage.AdDamage + (int)damage.ApDamage)  , other.transform.position , damage.type , result );
        Destroy(s , 6);
        Destroy(this.gameObject);
    }

}