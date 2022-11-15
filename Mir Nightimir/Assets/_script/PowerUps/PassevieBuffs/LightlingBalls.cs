using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;
using System.Threading.Tasks;

public class LightlingBalls : AbilityPowerUps 
{
    public static LightlingBalls Singiliton;
    public float Change = 0.1f;
    [SerializeField] GameObject effect;

    public float offset;

    public override void OnPowerUp(Player player)
    {
        base.OnPowerUp(player);

        if (!isHim)
            LightlingBalls.Singiliton.Change += 0.1f;
            
        if (isHim)
            return;

        GetPlayer().abilityPowerUps.Remove(this); 
        Destroy(this.gameObject);
    }

    public override Damage DamaModifayer(Damage damage, Transform target, IHpValue hpValue)
    {

        if ( Random.value < Change )
        {
            LightlingDamage( damage , target , hpValue );
        }

        return damage;
    }

    private async void LightlingDamage(Damage damage, Transform target, IHpValue hpValue)
    {
        Debug.Log("S");
        Collider2D[] collider2D1 = Physics2D.OverlapCircleAll(GetPlayer().Body.position,
                                                            GetPlayer().PlayerWeaponManger.CurrentWeapons.GetAttackRange() + 3,
                                                            GetPlayer().PlayerTarget.EnemyLayer);

        for (int i = 0; i < collider2D1.Length; i++)
        {
            if ( collider2D1[i] == null ) { continue; }
            if ( collider2D1[i].TryGetComponent<IHpValue>(out var hpValue1) == false )
                continue;

            hpValue1.HpValueChange( damage , out var result );

            GetPlayer().PlayerWeaponManger.CreatCoustomTextPopup( ((int)damage.AdDamage + (int)damage.ApDamage).ToString() , collider2D1[i].transform.position , Color.yellow );

            var effecta = Instantiate( effect , i == 0 ? GetPlayer().Body.transform.position : ( collider2D1[i].transform.position +  (( GetPlayer().Body.position  - collider2D1[i].transform.position ).normalized * -6 ) ) , Quaternion.identity );

            effecta.transform.LookAt( collider2D1[i].transform );
                
            Destroy(effecta , 3);
            await Task.Delay(200);
        }
    }

    public override void OnFirstTime(Player player)
    {
        base.OnFirstTime(player);

        Singiliton = this;
    }



}