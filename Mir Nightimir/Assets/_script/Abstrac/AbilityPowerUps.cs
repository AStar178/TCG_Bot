using System;
using UnityEngine;
using UnityEngine.UI;

public class AbilityPowerUps : MonoBehaviour 
{
    Player player1;
    [SerializeField] Sprite Icon;
    [SerializeField] string Name;
    [SerializeField] string Discripsen;
    protected bool isHim;
    protected Player GetPlayer() => player1;

    public virtual void OnPowerUp(Player player)
    {
        player1 = player;
    }
    public virtual void OnFirstTime(Player player)
    {
        player1 = player;
        isHim = true;
    }
    public virtual void OnDealDamage(Transform target , IHpValue targetHp) 
    {

    }
    public virtual void OnHpChange()
    {
        
    }

    public virtual void OnPowerUpUpdate()
    {

    }


    public Sprite GetDataUIIcon()
    {
        return Icon;
    }
    public String GetName()
    {
        return Name;
    }

    public String GetDiscord()
    {
        return Discripsen;
    }
    public virtual Damage DamaModifayer(Damage damage, Transform target, IHpValue hpValue )
    {
        return damage;
    }
}