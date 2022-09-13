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
    private void Start() {
        Currenthp = MaxHp;
    }
    private void Update() {

        if (delayTime <= 0)
            return;
    
        delayTime -= Time.deltaTime;
    }
    public void HpValueChange(Damage damage)
    {
        if (delayTime >= 0)
            return;
        

        delayTime = DelayDamageTakeTime;
        float AdDamageAmount = 100 - player.PlayerState.Ap_Defence;
        Currenthp -= damage.AdDamage * ( AdDamageAmount / 100 );

    }
}