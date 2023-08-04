using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FlamingHell : IteamPassive
{
    
    public List<BURN> BORNIGGALIVE = new List<BURN>();
    [SerializeField] float BURNAMOUNT;
    [SerializeField] float BURNDAMAGE;
    [SerializeField] ParticleSystem BurnObject;

    float x;
    private void OnEnable() {
        
        PlayerState.OnAtuoAttackDealDamage += BURNTHEM;

    }

    private void Update() {
        
            
            var owo = new List<BURN>();
            x -= Time.deltaTime;
            for (int i = 0; i < BORNIGGALIVE.Count; i++)
            {
                if (BORNIGGALIVE[i].enemyHp != null)
                    BORNIGGALIVE[i].refEffect.transform.position = BORNIGGALIVE[i].enemyHp.transform.position;
                var s = new BURN();
                s.tc = BORNIGGALIVE[i].tc - Time.deltaTime;
                s.enemyHp = BORNIGGALIVE[i] .enemyHp;
                s.refEffect = BORNIGGALIVE[i] .refEffect;
                BORNIGGALIVE[i] = s;
                if (BORNIGGALIVE[i].tc < 0)
                    {
                        BORNIGGALIVE[i].refEffect.Stop();
                        Destroy(BORNIGGALIVE[i].refEffect , 5);

                        owo.Add(BORNIGGALIVE[i]);
                    }

            }

            for (int i = 0; i < owo.Count; i++)
            {
                BORNIGGALIVE.Remove(owo[i]);  
            }
            
            var owod = new List<BURN>();
            if (x > 0)
                return;
            for (int i = 0; i < BORNIGGALIVE.Count; i++)
            {
                if (BORNIGGALIVE[i].enemyHp == null)
                {
                    BORNIGGALIVE[i].refEffect.Stop();
                    Destroy(BORNIGGALIVE[i].refEffect , 5);
                    owod.Add(BORNIGGALIVE[i]);
                    continue;
                }
                BORNIGGALIVE[i].enemyHp.TakeDamage(CreatDamageWithOutCrit( BURNDAMAGE , PlayerState ));

            }
            for (int i = 0; i < owod.Count; i++)
            {
                BORNIGGALIVE.Remove(owod[i]);  
            }
        x = 0.25f;
    }

    private void BURNTHEM(DamageData data, EnemyHp hp)
    {
        
        for (int i = 0; i < BORNIGGALIVE.Count; i++)
        {
            if ( BORNIGGALIVE[i].enemyHp.gameObject.GetInstanceID() == hp.gameObject.GetInstanceID() )
            {
                var b = new BURN();
                b = BORNIGGALIVE[i];
                b.tc = BURNAMOUNT;

                BORNIGGALIVE[i] = b;
                return;
            }

        }
        var x = new BURN();
            x.SetTime(BURNAMOUNT);
            x.enemyHp = hp;
            x.refEffect = Instantiate(BurnObject , Vector3.zero , Quaternion.identity);
            x.refEffect.transform.localScale = x.enemyHp.transform.localScale;
            BORNIGGALIVE.Add( x );
    }

    private void OnDisable() {
        
        PlayerState.OnAtuoAttackDealDamage -= BURNTHEM;

    }

}
[Serializable]
public struct BURN
{
    public float tc;
    public EnemyHp enemyHp;
    public ParticleSystem refEffect;
    public void SetTime(float s)
    {
        tc = s;
    }
}