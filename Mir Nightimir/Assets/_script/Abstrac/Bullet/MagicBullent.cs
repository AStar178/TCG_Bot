using UnityEngine;

public class MagicBullent : MonoBehaviour 
{
    public AbilityWeapons magic;
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
        Damage damage = new Damage();

        damage.AdDamage = magic.GetPWM().DamageAd * 0.1f;
        damage.ApDamage = magic.GetPWM().DamageAp * 1.1f;
        damage.Ad_DefenceReduser = magic.GetPWM().AmoroReduse;
        damage.Mp_DefenceReduser = magic.GetPWM().MagicReduse;
        damage.PlayerRefernce = magic.GetPlayer();
        hpValue.HpValueChange(damage , out var result);
        var s = Instantiate(magic.GetPWM().OnMagicHit , other.transform.position , Quaternion.identity);
        bool pornOnline = false;
        if (damage.AdDamage < damage.ApDamage)
            pornOnline = true;
        magic.GetPWM().OnDealDamage( ((int)damage.AdDamage + (int)damage.ApDamage)  , other.transform.position , pornOnline , result );
        Destroy(s , 6);
        Destroy(this.gameObject);
    }

}