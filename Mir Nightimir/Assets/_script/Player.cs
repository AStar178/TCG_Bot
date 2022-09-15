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
    public int CurrentLevel = 1;
    public int XpMax = 100;
    public int CurrentXp = 0;
    public int CurrentCoins = 0;



    public void GiveStuff(int xp , int coins)
    {
        CurrentXp += xp;

        if (CurrentXp >= XpMax)
            LevelUp();

        CurrentCoins += coins;
    }

    private void LevelUp()
    {
        while (CurrentXp >= XpMax)
        {
            CurrentXp -= XpMax;
            CurrentLevel++;
        }

    }

    private void Awake() => Singleton = this;

    private void Start()
    {
        PlayerMoveMent.moveSpeed = PlayerState.MoveSpeed;
        PlayerHp.MaxHp = PlayerState.MaxHpAmount;
        PlayerHp.Currenthp = PlayerState.MaxHpAmount;
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
    }

}
