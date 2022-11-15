using UnityEngine;

public class PlayerSimpBullet : MonoBehaviour
{
    public AbilityWeapons magic;
    public Transform target;
    public Damage damage;
    public float DevidedAmount = 1f;
    public float MultyAmount = 1f;
    public float PlusAmount = 0f;
    public bool SetDamage;
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
        if (SetDamage == false)
        {
            damage = magic.CreatDamage((magic.GetWeaponManger().DamageAd / DevidedAmount) * MultyAmount + PlusAmount, (magic.GetWeaponManger().DamageAp / DevidedAmount) * MultyAmount + PlusAmount, magic.GetWeaponManger().AmoroReduse, magic.GetWeaponManger().MagicReduse);
        }
        hpValue.HpValueChange(damage, out var result);

        magic.GetWeaponManger().OnDealDamage(((int)damage.AdDamage + (int)damage.ApDamage), other.transform.position, damage.type, result);
        Destroy(this.gameObject);
    }
}
