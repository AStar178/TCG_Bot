using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyHp : MonoBehaviour , IHpValue
{ 
    [SerializeField] State EnemyState;
    [SerializeField] private Rigidbody2D rigidbody2d;
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

        if ( rigidbody2d != null )
            rigidbody2d.AddForce( damage.knockback );

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
            if (AreadyGiveXp == true && reve == false)
            {
                result = DamageResult.DEID;
                return;
            }
            if (damage.PlayerRefernce != null)
            {
                AreadyGiveXp = true;
                damage.PlayerRefernce.GiveStuff( xpAmount == 0 ? 0 : + Random.Range( 0 , 100 ) , coinsAmount == 0 ? 0 : + Random.Range( 0 , 10 ) );
            }

                
            result = DamageResult.Killed;
            if (Player.Singleton.PlayerWeaponManger.CurrentWeapons.WeaponName == "Necromanser" && reve == false)
            {
                GraveStone();
                return;
            }
                Destroy(this.gameObject);
            return;
        }
        result = DamageResult.DealadDamaged;
        SpriteRendererOnTakeDamageEffect();
        
    }
    Sprite sprite;
    bool reve;
    bool AreadyGiveXp;

    private void GraveStone()
    {
        sprite = spriteRenderer.sprite;
        spriteRenderer.sprite = AIStatic.GraveStoneSprit;
        gameObject.layer = (int)Rpg.allLayers.Grave;
        if (TryGetComponent<TESTei>(out var tESTei))
            tESTei.enabled = false;

        Destoryreve();
    }

    private async void Destoryreve()
    {
        float waitTime = 15;
        while (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            await Task.Yield();
        }
        if (reve == false)
            Destroy(this.gameObject);
    }

    public void NecromanserISHAHAHAH( LayerMask layerMask )
    {
        reve = true;
        spriteRenderer.material.SetColor( "_Color" , Color.green * 10 );
        spriteRenderer.sprite = sprite;
        Currenthp = MaxHp;
        if (TryGetComponent<TESTei>(out var tESTei))
        {
            tESTei.enabled = true;
            List<int> list = new List<int>();
            list.Add((int)Rpg.allLayers.enemylayer);
            tESTei.ChangeTargetSelecting( layerMask , list , Rpg.EnemyTeam.Player );
        }
            
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
    private void Start() => StartHp();

    private void StartHp()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        gameObject.TryGetComponent<Turret>(out Turret nodle);
        gameObject.TryGetComponent<TESTei>(out TESTei Makaroni);

        MaxHp = EnemyState.MaxHpAmount;
        Currenthp = EnemyState.MaxHpAmount;
        Amoro = EnemyState.Amoro;
        MagicResest = EnemyState.MagicReset;

        if (nodle != null)
        {
            Turret b = gameObject.GetComponent<Turret>();
            b.Range = EnemyState.AggroRange;
            b.stat = EnemyState;
        }

        if (Makaroni != null)
        {
            TESTei b = gameObject.GetComponent<TESTei>();
            b.Speed = EnemyState.MoveSpeed;
            b.Range = EnemyState.AggroRange;
            b.state = EnemyState;
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
