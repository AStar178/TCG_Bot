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
        ResetStrons();

        List<AbilityPowerUps> abilityPower = new List<AbilityPowerUps>(); 
        abilityPower.AddRange( PowerUps.GetComponents<AbilityPowerUps>() );
        abilityPowerUps = abilityPower;
        for (int i = 0; i < abilityPower.Count; i++)
        {
            abilityPower[i].OnPowerUp ( this );
        }
        RESETFORREAL();
        
    }
    private void Update() {
        

        for (int i = 0; i < abilityPowerUps.Count; i++)
        {
            abilityPowerUps[i].OnPowerUpUpdate();
        }
        
    }
    public void AddPowerUp( AbilityPowerUps upgrateObject )
    {
        ResetStrons();

        PowerUps.gameObject.AddComponent<AbilityPowerUps>();
        List<AbilityPowerUps> abilityPower = new List<AbilityPowerUps>(); 
        abilityPower.AddRange( PowerUps.GetComponents<AbilityPowerUps>() );
        abilityPowerUps = abilityPower;
        for (int i = 0; i < abilityPower.Count; i++)
        {
            abilityPower[i].OnPowerUp ( this );
        }
        RESETFORREAL();
    }

    private void RESETFORREAL()
    {
        PlayerMoveMent.moveSpeed = ( dexterity * 0.25f ) < PlayerMoveMent.moveSpeed ? PlayerMoveMent.moveSpeed : PlayerMoveMent.moveSpeed *= ( dexterity * 0.25f );
        PlayerHp.MaxHp = ( vitality * 0.9f ) < PlayerHp.MaxHp ? PlayerHp.MaxHp : PlayerHp.MaxHp *= ( vitality * 1.25f );
        PlayerHp.Amoro = ( vitality * 0.6f ) < PlayerHp.Amoro  ? PlayerHp.Amoro  : PlayerHp.Amoro  *= ( vitality * 0.6f );
        PlayerHp.MagicResest = ( intelligence * 0.4 ) < PlayerHp.MagicResest ? PlayerHp.MagicResest : PlayerHp.MagicResest *= ( intelligence * 0.4f );
        PlayerWeaponManger.AttackSpeed *= ( dexterity );
        PlayerWeaponManger.AmoroReduse *= strength;
        PlayerWeaponManger.MagicReduse *= intelligence;
        PlayerWeaponManger.DamageAd *= strength;
        PlayerWeaponManger.DamageAp *= intelligence;
        PlayerWeaponManger.MaxMana = ( Fart * 0.5f ) < PlayerWeaponManger.MaxMana ? PlayerWeaponManger.MaxMana : PlayerWeaponManger.MaxMana *= ( Fart * 0.5f );
        PlayerWeaponManger.ManaRejyAmount = ( Fart * 0.75f) < PlayerWeaponManger.ManaRejyAmount ? PlayerWeaponManger.ManaRejyAmount : PlayerWeaponManger.ManaRejyAmount *= ( Fart * 0.75f );

        PlayerWeaponManger.TimeToGetMana = PlayerState.TimeToGetMana;

        UpdateUI();
    }

    private void ResetStrons()
    {
        PlayerMoveMent.moveSpeed = PlayerState.MoveSpeed;
        PlayerHp.MaxHp = PlayerState.MaxHpAmount;
        PlayerHp.Currenthp = PlayerState.MaxHpAmount;
        PlayerHp.Amoro = PlayerState.Amoro;
        PlayerHp.MagicResest = PlayerState.MagicReset;
        PlayerTarget.Raduis = PlayerState.AggroRange;
        PlayerWeaponManger.AttackSpeed = PlayerState.AttackSpeed;
        PlayerWeaponManger.AmoroReduse = PlayerState.Ad_DefenceReduser;
        PlayerWeaponManger.MagicReduse = PlayerState.MagicReset;
        PlayerWeaponManger.DamageAd = PlayerState.AdDamage;
        PlayerWeaponManger.DamageAp = PlayerState.ApDamage;
        PlayerWeaponManger.MaxMana = PlayerState.MaxMana;
        PlayerWeaponManger.CurrentMana = PlayerState.MaxMana;
        PlayerWeaponManger.ManaRejyAmount = PlayerState.ManaRejyAmount;
        PlayerWeaponManger.TimeToGetMana = PlayerState.TimeToGetMana;



        UpdateUI();
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
