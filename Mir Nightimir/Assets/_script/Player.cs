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
    [SerializeField] public GameObject OnLevelEffect;
    [SerializeField] Transform PowerUps;
    public int CurrentLevel = 1;
    public int XpMax = 100;
    public int CurrentXp = 0;
    public int CurrentCoins = 0;
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

    public void GiveStuff(int xp , int coins)
    {
        CurrentXp += xp;

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
        abilityPowerUps.Add ( power );
        power.OnPowerUp( this );
        
        
        CulculateAllBuffs();
    }

    public void CulculateBuffsAgain() => CulculateAllBuffs();

    private void CulculateAllBuffs()
    {

        PlayerMoveMent.moveSpeed =  ( dexterity * 0.25f ) < PlayerState.MoveSpeed ? PlayerState.MoveSpeed : ( PlayerState.MoveSpeed + MoveSpeedBuff )  * ( dexterity * 0.25f );
        PlayerHp.MaxHp = ( vitality * 0.9f ) < PlayerState.MaxHpAmount ? PlayerState.MaxHpAmount : ( PlayerState.MaxHpAmount + HpBuff ) * ( vitality * 1.25f );
        PlayerHp.Amoro = ( vitality * 0.6f ) < PlayerState.Amoro  ? PlayerState.Amoro  : ( PlayerState.Amoro + AmoroBuff ) * ( vitality * 0.6f );
        PlayerHp.MagicResest = ( intelligence * 0.4 ) < PlayerState.MagicReset ? PlayerState.MagicReset : ( PlayerState.MagicReset + MagicReseted ) * ( intelligence * 0.4f );
        PlayerWeaponManger.AttackSpeed = PlayerState.AttackSpeed * ( dexterity + ( AttackSpeedBuff * 0.25f ) );
        PlayerWeaponManger.AmoroReduse = PlayerState.Ad_DefenceReduser * strength;
        PlayerWeaponManger.MagicReduse = PlayerState.Mp_DefenceReduser * intelligence + ( MagicReduseBuff * 0.25f );
        PlayerWeaponManger.DamageAd = PlayerState.AdDamage * strength + ( DamageAdBuff * 0.25f );
        PlayerWeaponManger.DamageAp = PlayerState.ApDamage * intelligence;
        PlayerWeaponManger.MaxMana = ( Fart * 0.5f ) < PlayerState.MaxMana ? PlayerState.MaxMana : PlayerState.MaxMana * ( Fart * 0.5f );
        PlayerWeaponManger.ManaRejyAmount = ( Fart * 0.75f) < PlayerState.ManaRejyAmount ? PlayerState.ManaRejyAmount : PlayerState.ManaRejyAmount * ( Fart * 0.75f );

        PlayerWeaponManger.TimeToGetMana = PlayerState.TimeToGetMana;

        UpdateUI();

    }

    public void OnHpChanged()
    {
        
        for (int i = 0; i < abilityPowerUps.Count; i++)
        {   
            abilityPowerUps[i].OnHpChange();
        }        

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
}
