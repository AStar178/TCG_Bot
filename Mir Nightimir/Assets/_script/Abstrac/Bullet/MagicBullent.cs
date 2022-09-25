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
            transform.rotation = Quaternion.Euler( transform.rotation.x + 0 , transform.rotation.y + 0 , transform.rotation.z + 0.1f );
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
        damage.type = magic.GetPlayer().DamageModifayer( hpValue , other.transform , damage );
        hpValue.HpValueChange(damage , out var result);
        var s = Instantiate(magic.GetPWM().OnMagicHit , other.transform.position , Quaternion.identity);
  
        magic.GetPWM().OnDealDamage( ((int)damage.AdDamage + (int)damage.ApDamage)  , other.transform.position , damage.type , result );
        Destroy(s , 6);
        Destroy(this.gameObject);
    }

}