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
    public void HpValueChange(Damage damage , out DamageResult result)
    {
        if (delayTime > 0)
        {
            result = DamageResult.Flaid;
            return;
        }
            
        

        delayTime = DelayDamageTakeTime;
        float AdDamageAmount = 100 - ( Amoro - damage.Ad_DefenceReduser );
        Currenthp -= damage.AdDamage * ( AdDamageAmount / 100 ) ;
        float ApDamageAmount = 100 - ( MagicResest - damage.Mp_DefenceReduser );
        Currenthp -= damage.ApDamage * ( ApDamageAmount / 100 ) ;
        Currenthp = Mathf.Clamp( Currenthp , 0 , MaxHp );
        result = DamageResult.DealadDamaged;
        player.OnHpChanged();
        player.UpdateUI();
        SpriteRendererOnTakeDamageEffect();
    }

    private async void SpriteRendererOnTakeDamageEffect()
    {
        float hey = 0.2f;
        while (delayTime > 0)
        {
            hey -= Time.deltaTime;
            if ( hey <  0)
            {
                Sprite2();
                hey = 0.1f;
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