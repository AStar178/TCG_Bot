using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Singleton;
    [SerializeField] public State PlayerState;
    [SerializeField] public PlayerMoveMent PlayerMoveMent;
    [SerializeField] public PlayerTarget PlayerTarget;
    [SerializeField] public PlayerWeaponManger PlayerWeaponManger;
    [SerializeField] public PlayerHp PlayerHp;
    [SerializeField] public List<AbilityPowerUps> abilityPowerUps;
    [SerializeField] public UiEvent uiEvent;
    [SerializeField] public UpgrateEvent upgrateEvent;
    [SerializeField] public GameObject OnLevelEffect;
    [SerializeField] public Transform Body;
    [SerializeField] Transform PowerUps;
    public int CurrentLevel = 1;
    public int XpMax = 100;
    public int CurrentCoins = 0;
    public float CurrentXp = 0;
    public float strength = 1; //damage
    public float vitality = 1; //health + armor
    public float dexterity = 1; //attack speed (maybe + move ment speed)
    public float intelligence = 1;  //magic damage + magic resist
    public float Fart = 1; //mana , mana rejey

   
    public float MoveSpeedBuff;
    public float HpBuff;
    public float AmoroBuff; 
    public float MagicReseted; 
    public float AttackSpeedBuff; 
    public float MagicReduseBuff; 
    public float DamageAdBuff; 
    [Range(1 , 1000)]
    public float XpGainBuff = 1;

    public void GiveStuff(int xp , int coins)
    {
        CurrentXp += ( xp * XpGainBuff );

        if (CurrentXp >= XpMax)
            LevelUp();

        CurrentCoins += coins;
        UpdateUI();
    }

    private void LevelUp()
    {
        while (CurrentXp >= XpMax)
        {
            CurrentXp -= XpMax;
            CurrentLevel++;
            XpMax = (int)( 100 * ( CurrentLevel * 0.2f ) );
            PlayerHp.Currenthp = PlayerHp.MaxHp;
            PlayerWeaponManger.CurrentMana = PlayerWeaponManger.MaxMana;
            if (OnLevelEffect != null)
                OnLevelEffectFunc();
        }

    }

    private void OnLevelEffectFunc()
    {
        var yes = Instantiate( OnLevelEffect , PlayerHp.transform.position , Quaternion.identity );
        PlayerWeaponManger.CreatCoustomTextPopup( "LEVEL UP" , PlayerHp.transform.position );
        Destroy( yes , 6 );

    }

    public Damage DamageModifayer( IHpValue hpValue , Transform target , Damage damage )
    {
        var trawe = new DamageType();
        trawe = DamageType.AD;
        var sdamage = damage;

        if (damage.AdDamage < damage.ApDamage)
            trawe = DamageType.AP;
        
        for (int i = 0; i < abilityPowerUps.Count; i++)
        {

            var damages = abilityPowerUps[i].DamaModifayer( damage , target , hpValue );
            sdamage = damages;
            if (damages.type == DamageType.Critial)
                trawe = DamageType.Critial;
            
            sdamage.type = trawe;
        }
        sdamage.type = trawe;
        sdamage.type = trawe;
        CulculateAllBuffs();
        return sdamage;
    }

    private void Awake() => Singleton = this;

    private void Start()
    {

        List<AbilityPowerUps> abilityPower = new List<AbilityPowerUps>(); 
        abilityPower.AddRange( PowerUps.GetComponentsInChildren<AbilityPowerUps>() );
        abilityPowerUps = abilityPower;
        for (int i = 0; i < abilityPower.Count; i++)
        {
            abilityPower[i].OnPowerUp ( this );
        }

        CulculateAllBuffs();

        PlayerWeaponManger.CurrentMana = PlayerWeaponManger.MaxMana;

    }
    private void Update() {
        

        for (int i = 0; i < abilityPowerUps.Count; i++)
        {
            abilityPowerUps[i].OnPowerUpUpdate();
        }
        
    }
    public void AddPowerUp( GameObject upgrateObject )
    {
        
        var owo = Instantiate( upgrateObject );
        owo.transform.SetParent( PowerUps );
        var power = owo.GetComponent<AbilityPowerUps>();
        CheakForWeHaveThisArealdy( power );
        abilityPowerUps.Add ( power );
        power.OnPowerUp( this );
        
        
        CulculateAllBuffs();
        OnUpgrateUis( power );
    }

    private void CheakForWeHaveThisArealdy(AbilityPowerUps power)
    {
        for (int i = 0; i < abilityPowerUps.Count; i++)
        {
            if ( power.name == abilityPowerUps[i].name )
            {
                return;           
            }
        }
        power.OnFirstTime( this );
    }

    public void CulculateBuffsAgain() => CulculateAllBuffs();

    private void CulculateAllBuffs()
    {
        PlayerMoveMent.moveSpeed =  ( PlayerState.MoveSpeed * ( ( dexterity * 0.25 ) + MoveSpeedBuff ) ) < PlayerState.MoveSpeed ? PlayerState.MoveSpeed : ( PlayerState.MoveSpeed )  * ( ( dexterity * 0.25f ) + MoveSpeedBuff ) + CurrentLevel * 0.1f;
        PlayerHp.MaxHp = ( PlayerState.MaxHpAmount * ( vitality * 0.9f + HpBuff ) ) < PlayerState.MaxHpAmount ? PlayerState.MaxHpAmount : ( PlayerState.MaxHpAmount + HpBuff ) * ( vitality * 1.25f ) + CurrentLevel * 0.1f;
        PlayerHp.Amoro = ( PlayerState.Amoro * ( vitality * 0.6f + AmoroBuff ) ) < PlayerState.Amoro  ? PlayerState.Amoro  : ( PlayerState.Amoro + AmoroBuff ) * ( vitality * 0.6f ) + CurrentLevel * 0.1f;
        PlayerHp.MagicResest = (  MagicReseted * ( intelligence * 0.4 ) ) < PlayerState.MagicReset ? PlayerState.MagicReset : ( PlayerState.MagicReset + MagicReseted ) * ( intelligence * 0.4f ) + CurrentLevel * 0.1f;
        PlayerWeaponManger.AttackSpeed = PlayerState.AttackSpeed * ( dexterity + ( AttackSpeedBuff * 0.25f ) ) + CurrentLevel * 0.1f;
        PlayerWeaponManger.AmoroReduse = PlayerState.Ad_DefenceReduser * strength + CurrentLevel * 0.1f;
        PlayerWeaponManger.MagicReduse = PlayerState.Mp_DefenceReduser * intelligence + ( MagicReduseBuff * 0.25f ) + CurrentLevel * 0.1f;
        PlayerWeaponManger.DamageAd = PlayerState.AdDamage * strength + ( DamageAdBuff * 0.25f ) + CurrentLevel * 0.1f;
        PlayerWeaponManger.DamageAp = PlayerState.ApDamage * intelligence + CurrentLevel * 0.1f;
        PlayerWeaponManger.MaxMana = ( Fart * 0.5f ) < PlayerState.MaxMana ? PlayerState.MaxMana : PlayerState.MaxMana * ( Fart * 0.5f ) + CurrentLevel * 0.1f;
        PlayerWeaponManger.ManaRejyAmount = ( Fart * 0.75f) < PlayerState.ManaRejyAmount ? PlayerState.ManaRejyAmount : PlayerState.ManaRejyAmount * ( Fart * 0.75f ) + CurrentLevel * 0.1f;
        
        PlayerWeaponManger.TimeToGetMana = PlayerState.TimeToGetMana;

        UpdateUI();

    }

    public void OnHpChanged()
    {
        
        for (int i = 0; i < abilityPowerUps.Count; i++)
        {   
            abilityPowerUps[i].OnHpChange();
        }        

        CulculateAllBuffs();
    }

    public void UpdateUI()
    {
        UiEventData uiEventData = new UiEventData();
        uiEventData.CurrentHP = (int)PlayerHp.Currenthp;
        uiEventData.MaxHp = (int)PlayerHp.MaxHp;
        uiEventData.CurrentMana = (int)PlayerWeaponManger.CurrentMana;
        uiEventData.MaxMana = (int)PlayerWeaponManger.MaxMana;
        uiEventData.CurrentXp = (int)CurrentXp;
        uiEventData.MaxXp = (int)XpMax;
        uiEventData.CurrentLevel = CurrentLevel;
        uiEventData.CoinsAmount = CurrentCoins;

        uiEvent.Rasise( uiEventData );
    }
    private void OnUpgrateUis(AbilityPowerUps power)
    {
        var uy = power.GetDataUI();
        upgrateEvent?.Rasise( uy );

    }
    public Damage CreatDamage( float ad, float ap, float amoroReduse, float magicReduse, Transform pos )
    {
        Damage damage = new Damage();
        damage = Rpg.CreatDamage(  ad , ap , amoroReduse , magicReduse , this , PlayerHp , pos );
        var damage2 = DamageModifayer( PlayerHp , pos , damage );
        print( damage2.type );

        return damage2;
    }
}
