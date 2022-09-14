using UnityEngine;

public class PlayerWeaponManger : MonoBehaviour 
{    
    public GameObject OnMeeleHit;
    public float AttackSpeed;
    public float DamageAd;
    public float DamageAp;
    public float AmoroReduse;
    public float MagicReduse;
    [SerializeField] private AbilityWeapons CurrentWeapons;
    [HideInInspector] public float attackSpeed;

    private void Start() {
        if (CurrentWeapons == null) { return; }

        CurrentWeapons.StartAbilityWp(Player.Singleton);
    }
    private void Update() 
    {
        if(attackSpeed > 0)
        {
            attackSpeed -= Time.deltaTime;
        }

        CurrentWeapons.UpdateAbilityWp();
    }
    public void DealDamage(IHpValue enemyHp , Transform pos)
    {
        CurrentWeapons.DealDamage(enemyHp , pos); 
    }
    public void SweichWeapon(GameObject newWeapons)
    {
        CurrentWeapons.StopAbilityWp();
        var newweapos = Instantiate(newWeapons , transform.position , Quaternion.identity);
        CurrentWeapons = newWeapons.GetComponent<AbilityWeapons>();
    }

}