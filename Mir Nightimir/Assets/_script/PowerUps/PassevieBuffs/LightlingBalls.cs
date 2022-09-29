using System;
using UnityEngine;

public class LightlingBalls : AbilityPowerUps 
{
    public static LightlingBalls Singiliton;
    public float CritialAmount = 1.75f;
    [SerializeField] GameObject Circle;
    public override void OnPowerUp(Player player)
    {
        base.OnPowerUp(player);

        SaftyCircle.Singiliton.CritialAmount += 0.25f;

        if (isHim)
            return;

        GetPlayer().abilityPowerUps.Remove(this); 
        Destroy(this.gameObject);
    }
    // public override Damage DamaModifayer(Damage damage, Transform target, IHpValue hpValue)
    // {
        
    // }
    public override void OnFirstTime(Player player)
    {
        base.OnFirstTime(player);

    }



}