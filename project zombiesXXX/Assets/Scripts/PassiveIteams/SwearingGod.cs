using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwearingGod : IteamPassive
{

    [SerializeField]
    public List<String> RealyBadWorld = new List<string>();
    [SerializeField] Color color;
    [SerializeField] float GlowIntensaty;
    public override void OnStart(PlayerState state)
    {

        state.OnDamageTaken += OnDamaged;

    }
    private void OnDisable() {

        PlayerState.OnDamageTaken -= OnDamaged;

    }

    private void OnDamaged(DamageData data)
    {

        if (UnityEngine.Random.value <= 0.25)
        {   
            RPGStatic.Instance.CreatCoustomTextPopupOnlyUpMatrial(RealyBadWorld.OrderBy( s => UnityEngine.Random.value ).FirstOrDefault() , data.enemyHp.transform.position , Color.white , GlowIntensaty);
        }   
            
    }
}
