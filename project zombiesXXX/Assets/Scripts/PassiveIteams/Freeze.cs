using System;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

public class Freeze : IteamPassive {

    [SerializeField] string debuffname;
    [SerializeField] float TimerAmount;
    [SerializeField] VisualEffect visualEffect;
    PlayerState playerState1;

    private void OnEnable() {
        Player.Current.PlayerState.OnAtuoAttackDealDamage += Freezing;  
    }
    private void OnDisable() {
        Player.Current.PlayerState.OnAtuoAttackDealDamage += Freezing;
    }

    private void Freezing(DamageData data , EnemyHp target)
    {
        var debuff = target.GetComponent<BasicEnemy>();
        
        
        /*debuff debuff = debuffs.Where( s => debuffname == s.Names ).FirstOrDefault();
        print(debuff.Names == debuffname);
        if (debuff.Names != debuffname)
        {
            AddDebuff(data , target);
            return;
        }*/
        debuff.SpeedSlow += 0.20f;
        

        if (debuff.SpeedSlow >= 1)
        {
            debuff.SpeedSlow = 0;
            data.DamageAmount = data.DamageAmount * 2;
            target.TakeDamage(data);
            var effet = Instantiate(visualEffect , target.transform.position , transform.localRotation);
            effet.Play();
            Destroy(effet , 5);
        }
        target.GetComponent<BasicEnemy>().SpeedSlow = debuff.SpeedSlow;
        target.MainMatrial.material.SetFloat("_FreezAmount" , debuff.SpeedSlow);
    }

    /*private void AddDebuff(DamageData data, EnemyHp target)
    {
        var debuff = new debuff();

        debuff.Names = debuffname;
        debuff.Value += 0.20f;
        debuff.Timer = TimerAmount;
        target.MainMatrial.material.SetFloat("Freeze" , debuff.Value);
        target.GetComponent<BasicEnemy>().SpeedSlow = debuff.Value;
        target.debuffss.Add(debuff);
    }*/
}