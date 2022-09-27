using UnityEngine;

public class MagicBullent : MonoBehaviour 
{
    public AbilityWeapons magic;
    public Transform target;
    [SerializeField] Rigidbody2D rigidbod;
    [SerializeField] float Speed = 1;
    [SerializeField] bool yellow;
    private void Update() 
    {
        if (yellow)
            transform.localEulerAngles += Vector3.forward * ( 1f + Random.Range( -0.25f , 0.25f ) ) ;
        if (target == null) { return; }


        rigidbod.velocity = (target.position - transform.position).normalized * Speed;

    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        
        if (!other.TryGetComponent(out IHpValue hpValue)) { return; } 
        var damage = magic.CreatDamage( magic.GetWeaponManger().DamageAd , magic.GetWeaponManger().DamageAp , magic.GetWeaponManger().AmoroReduse , magic.GetWeaponManger().MagicReduse );
        hpValue.HpValueChange(damage , out var result);
        var s = Instantiate(magic.GetWeaponManger().OnMagicHit , other.transform.position , Quaternion.identity);
  
        magic.GetWeaponManger().OnDealDamage( ((int)damage.AdDamage + (int)damage.ApDamage)  , other.transform.position , damage.type , result );
        Destroy(s , 6);
        Destroy(this.gameObject);
    }

}