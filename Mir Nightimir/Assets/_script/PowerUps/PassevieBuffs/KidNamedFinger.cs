using System;
using UnityEngine;

public class KidNamedFinger : AbilityPowerUps 
{
    public static KidNamedFinger Singiliton;
    public float CoolDown;
    float c;
    [SerializeField] GameObject BlackHole;
    [SerializeField] private float speedd;

    public override void OnPowerUp(Player player)
    {
        base.OnPowerUp(player);

        KidNamedFinger.Singiliton.CoolDown += 25f;

        if (isHim)
            return;

        GetPlayer().abilityPowerUps.Remove(this); 
        Destroy(this.gameObject);
    }

    public override void OnPowerUpUpdate()
    {
        if (isHim == false)
            return;

        Transform target = GetPlayer().PlayerTarget.target;

        if (target == null)
            return;

        if ( c < 0 ) { shoot( target ); c = (100/CoolDown); }

        c -= Time.deltaTime;

    }

    private void shoot(Transform target)
    {

        var blackhol = Instantiate( BlackHole , GetPlayer().Body.position , Quaternion.identity );

        blackhol.GetComponent<BlackHole>().magic = GetPlayer().PlayerWeaponManger.CurrentWeapons;
        blackhol.GetComponent<Rigidbody2D>().AddForce( ( target.position - GetPlayer().Body.position ).normalized * speedd );

    }

    public override void OnFirstTime(Player player)
    {
        base.OnFirstTime(player);

        Singiliton = this;
    }



}