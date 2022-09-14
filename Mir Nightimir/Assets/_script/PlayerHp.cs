using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerHp : MonoBehaviour, IHpValue
{
    [SerializeField] Player player;
    [SerializeField] SpriteRenderer spriteRenderer;
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
        {
            spriteRenderer.enabled = true;
            return;
        }

        delayTime -= Time.deltaTime;
    }
    public void HpValueChange(Damage damage)
    {
        if (delayTime > 0)
            return;
        
        SpriteRendererOnTakeDamageEffect();
        delayTime = DelayDamageTakeTime;
        float AdDamageAmount = 100 - Amoro;
        Currenthp -= damage.AdDamage * ( AdDamageAmount / 100 );
        float ApDamageAmount = 100 - MagicResest;
        Currenthp -= damage.ApDamage * ( AdDamageAmount / 100 );

    }

    private async void SpriteRendererOnTakeDamageEffect()
    {
        float hey = 0.1f;
        print("Yse");
        while (delayTime > 0)
        {
            hey -= Time.deltaTime;
            if ( hey <  0)
            {
                hey = 0.1f;
                Sprite2();
            }

            await Task.Yield();
        }
    }

    private void Sprite2()
    {
        if (spriteRenderer.enabled == true)
        {
            spriteRenderer.enabled = false;
            return;     
        }
        spriteRenderer.enabled = true;
    }
}