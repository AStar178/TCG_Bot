using UnityEngine;

public class MagicBullent : MonoBehaviour 
{
    public AbilityWeapons magic;
    public Damage damage;
    public Transform target;
    [SerializeField] Rigidbody2D rigidbod;
    [SerializeField] float Speed = 1;
    private void Update() 
    {

        if (target == null) { return; }


        rigidbod.velocity = (target.position - transform.position).normalized * Speed;

    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        
        if (!other.TryGetComponent(out IHpValue hpValue)) { return; } 


        hpValue.HpValueChange(damage);
        var s = Instantiate(magic.GetPWM().OnMagicHit , transform.position , Quaternion.identity);
        bool pornOnline = false;
        if (damage.AdDamage < damage.ApDamage)
            pornOnline = true;
        magic.GetPWM().OnDealDamage( ((int)damage.AdDamage + (int)damage.ApDamage)  , transform.position , pornOnline );
        Destroy(s , 6);
        Destroy(this.gameObject);
    }

}