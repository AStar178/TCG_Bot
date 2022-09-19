using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyHp : MonoBehaviour , IHpValue
{ 
    [SerializeField] State EnemyState;

    public float MaxHp;
    public float Currenthp;
    public float Amoro;
    public float MagicResest;
    public float DelayDamageTakeTime;
    [Range( 0 , 100 )]
    [SerializeField] public float BlockChanse;
    float delayTime;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject OnDieEffect;
    [SerializeField] int xpAmount;
    [SerializeField] int coinsAmount;
    [SerializeField] DamageResult resultOfBlocked;
    public void HpValueChange( Damage damage , out DamageResult result )
    {
        if ( Random.value < BlockChanse / 100 ) { result = resultOfBlocked; return; }

        float AdDamageAmount = 100 - ( Amoro - damage.Ad_DefenceReduser );
        Currenthp -= damage.AdDamage * ( AdDamageAmount / 100 );
        float ApDamageAmount = 100 - ( MagicResest - damage.Mp_DefenceReduser );
        Currenthp -= damage.ApDamage * ( ApDamageAmount / 100 );
        if (Currenthp <= 0)
        {
            if (OnDieEffect != null)
            {
                var objett = Instantiate(OnDieEffect , transform.position , Quaternion.identity);
                Destroy(OnDieEffect , 6);
            }
            if (damage.PlayerRefernce != null)
                damage.PlayerRefernce.GiveStuff( xpAmount == 0 ? 0 : + Random.Range( 0 , 100 ) , coinsAmount == 0 ? 0 : + Random.Range( 0 , 10 ) );
            Destroy(this.gameObject);
            result = DamageResult.Killed;
            return;
        }
        result = DamageResult.DealadDamaged;
        SpriteRendererOnTakeDamageEffect();
        
    }
    private void Update() {
        if (delayTime < 0) { if ( spriteRenderer != null ) { spriteRenderer.enabled = true; }  return; }
        delayTime -= Time.deltaTime;
    }

    private async void SpriteRendererOnTakeDamageEffect()
    {
        if (delayTime > 0) { return; }
        delayTime = DelayDamageTakeTime;
        float hey = 0.1f;
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
        if (spriteRenderer == null) { return; }
        if (spriteRenderer.enabled == true)
        {
            spriteRenderer.enabled = false;
            return;     
        }
        spriteRenderer.enabled = true;
    }

}
