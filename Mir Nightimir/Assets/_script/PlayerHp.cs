using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour, IHpValue
{
    [SerializeField] Player player;
    public float MaxHp;
    public float Currenthp;
    public float DelayDamageTakeTime;
    float delayTime;
    public float Amoro;
    public float MagicResest;

    private void Start() {
        Currenthp = MaxHp;
    }
    private void Update() {

        if (delayTime < 0)
            return;
    
        delayTime -= Time.deltaTime;
    }
    public void HpValueChange(Damage damage)
    {

        if (delayTime > 0)
            return;
        

        delayTime = DelayDamageTakeTime;
        float AdDamageAmount = 100 - Amoro;
        Currenthp -= damage.AdDamage * ( AdDamageAmount / 100 );
        float ApDamageAmount = 100 - MagicResest;
        Currenthp -= damage.ApDamage * ( AdDamageAmount / 100 );

    }
}