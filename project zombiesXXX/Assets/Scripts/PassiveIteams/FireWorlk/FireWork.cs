using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FireWork : IteamPassive
{
    public static FireWork Current;
    [SerializeField] float DamageAmount = 10;
    [SerializeField] int SpawnRaket = 5;
    [SerializeField] GameObject Rocket;
    int attacktime;
    private void Awake() {
        Current = this;
    }
    private void OnEnable() {
        PlayerState.OnAtuoAttackDealDamage += OnAtuo;
    }

    private async void OnAtuo(DamageData data, EnemyHp hp)
    {
        attacktime++;

        if (attacktime < 4)
            return;
        
        for (int i = 0; i < SpawnRaket; i++)
        {
            
            var s = Instantiate(Rocket , PlayerGetpos + new Vector3(0 , 2 , 0) , Quaternion.identity);
            s.transform.eulerAngles = new Vector3( -90 , 0 , 0 );

            await Task.Delay(250);

        }


        attacktime = 0;

    }

    private void OnDisable() {
        PlayerState.OnAtuoAttackDealDamage -= OnAtuo;
    }
    
    public void OnHit(EnemyHp hp)
    {
        var s = CreatDamage( DamageAmount , PlayerState , out var crited );
        hp.TakeDamage(s);
        PlayerState.OnAbilityAttackDealDamage?.Invoke( s , hp );
    }
}
