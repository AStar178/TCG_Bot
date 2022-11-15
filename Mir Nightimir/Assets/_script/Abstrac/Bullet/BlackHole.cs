using UnityEngine;

public class BlackHole : MonoBehaviour 
{
    public AbilityWeapons magic;
    [SerializeField] float forse;

    public void Update() 
    {

        if ( magic is null)
            return;

        transform.localEulerAngles += Vector3.forward * ( 1f + Random.Range( -0.25f , 0.25f ) );


        Collider2D[] pos = Physics2D.OverlapCircleAll( transform.position , 6 , magic.GetPlayer().PlayerTarget.EnemyLayer );

        for (int i = 0; i < pos.Length; i++)
        {
            
            pos[i].transform.position = Vector3.Lerp( pos[i].transform.position , transform.position , forse );

        }

    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if ( other.TryGetComponent<IHpValue>( out var hpValue ) == false )
            return;

        var damage = Rpg.CreatDamage( magic.GetWeaponManger().DamageAd * 2 , magic.GetWeaponManger().DamageAp * 2 , magic.GetWeaponManger().AmoroReduse , magic.GetWeaponManger().MagicReduse , magic.GetPlayer() );

        hpValue.HpValueChange(damage , out var result);
  
        magic.GetWeaponManger().OnDealDamage( ((int)damage.AdDamage + (int)damage.ApDamage)  , other.transform.position , damage.type , result );

    }
    private void Start() {
        Destroy( gameObject , 10 );
    }

}