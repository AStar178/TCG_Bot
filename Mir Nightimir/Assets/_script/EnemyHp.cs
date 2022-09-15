using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyHp : MonoBehaviour , IHpValue
{ 
    [SerializeField] EnemyState EnemyState;

    public float MaxHp;
    public float Currenthp;
    public float Amoro;
    public float MagicResest;
    public float DelayDamageTakeTime;
    float delayTime;
    [SerializeField] SpriteRenderer spriteRenderer;
    public void HpValueChange(Damage damage)
    {

        float AdDamageAmount = 100 - Amoro;
        Currenthp -= damage.AdDamage * ( AdDamageAmount / 100 );
        float ApDamageAmount = 100 - MagicResest;
        Currenthp -= damage.ApDamage * ( AdDamageAmount / 100 );
        SpriteRendererOnTakeDamageEffect();
    }
    private void Update() {
        if (delayTime < 0) { spriteRenderer.enabled = true;  return; }
        delayTime -= Time.deltaTime;
    }

    private async void SpriteRendererOnTakeDamageEffect()
    {
        if (delayTime > 0) { return; }
        delayTime = DelayDamageTakeTime;
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
