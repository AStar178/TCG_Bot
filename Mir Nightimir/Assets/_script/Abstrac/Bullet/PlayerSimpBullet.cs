using UnityEngine;

public class PlayerSimpBullet : MonoBehaviour
{
    public AbilityWeapons magic;
    public Transform target;
    [SerializeField] Rigidbody2D rigidbod;
    [SerializeField] float Speed = 1;
    [SerializeField] bool yellow;
    private void Update()
    {
        if (yellow)
            transform.rotation = Quaternion.Euler(transform.rotation.x + 0, transform.rotation.y + 0, transform.rotation.z + 0.1f);
        if (target == null) { return; }


        rigidbod.velocity = (target.position - transform.position).normalized * Speed;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!other.TryGetComponent(out IHpValue hpValue)) { return; }
        var damage = magic.CreatDamage(magic.GetWeaponManger().DamageAd, magic.GetWeaponManger().DamageAp, magic.GetWeaponManger().AmoroReduse, magic.GetWeaponManger().MagicReduse);
        hpValue.HpValueChange(damage, out var result);

        magic.GetWeaponManger().OnDealDamage(((int)damage.AdDamage + (int)damage.ApDamage), other.transform.position, damage.type, result);
        Destroy(this.gameObject);
    }
}
