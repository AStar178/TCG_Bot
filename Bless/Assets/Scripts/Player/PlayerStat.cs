using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public int waitBeforeStamina;
    private int beforeStamina;
    public int waitBeforeMana;
    private int beforeMana;
    public bool Exhusted;
    public bool Drained;
    [Header("Attribut")]
    public int Agility = 10;
    public int Strength = 10;
    public int Endurance = 10;
    public int Intelligence = 10;
    public int Mind = 10;

    [Header("Stats")]
    [SerializeField]
    float Stamina = 100;
    [SerializeField]
    float StaminaRegen = 1;
    [SerializeField]
    float Mana = 100;
    [SerializeField]
    float ManaRegen = 1;

    [Header("Booleans")]
    public bool UsingStamina;
    public bool UsingMana;

    [Header("Private")]
    public float CurrentStamina = 0;
    public float CurrentMana = 0;

    // Start is called before the first frame update
    void Start()
    {
        StatsCheck();
        CurrentStamina = Stamina;
        CurrentMana = Mana;
        Static.UiManager.StaminaUI(CurrentStamina, Stamina);
        Static.UiManager.ManaUI(CurrentMana, Mana);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (beforeStamina > 0)
            beforeStamina--;
        else RegenerateStamina();
        if (beforeMana > 0)
            beforeMana--;
        else RegenerateMana();
    }

    public void StatsCheck()
    {
        Stamina = (Endurance * 10);
        StaminaRegen = (Endurance * .03f);
        Mana = 50 + (Mind * 5);
        ManaRegen = (Mind * .03f);
    }

    private void RegenerateStamina()
    {
        if (CurrentStamina < Stamina)
        {
            CurrentStamina += StaminaRegen;
            Static.UiManager.StaminaUI(CurrentStamina, Stamina);
        }

        else if (CurrentStamina > Stamina)
        {
            CurrentStamina = Stamina;
            Static.UiManager.StaminaUI(CurrentStamina, Stamina);
        }

        if (CurrentStamina >= Stamina / 4)
        {
            Exhusted = false;
        }
    }

    private void RegenerateMana()
    {
        if (CurrentMana < Mana)
        {
            CurrentMana += ManaRegen;
            Static.UiManager.ManaUI(CurrentMana, Mana);
        }

        else if (CurrentMana > Mana)
        {
            CurrentMana = Mana;
            Static.UiManager.ManaUI(CurrentMana, Mana);
        }

        if (CurrentMana >= Mana / 4)
        {
            Drained = false;
        }
    }

    public void DamageStamina(float UsedAmount)
    {
        if (Exhusted) return;

        if (CurrentStamina > 0)
        {
            if (CurrentStamina > UsedAmount)
            {
                CurrentStamina -= UsedAmount;
                beforeStamina = waitBeforeStamina;
            } else
            {
                CurrentStamina = 0;
                beforeStamina = waitBeforeStamina;
                Exhusted = true;
            }
        }
        if (CurrentStamina < 0)
        {
            CurrentStamina = 0;
        }
        if (CurrentStamina <= 1)
        {
            Exhusted = true;
        }
        Static.UiManager.StaminaUI(CurrentStamina, Stamina);
    }

    public void DamageMana(float UsedAmount)
    {
        if (CurrentMana > 0)
        {
            if (UsedAmount < CurrentMana)
            {
                CurrentMana -= UsedAmount;
                beforeMana = waitBeforeMana;
            } else
            {
                CurrentMana = 0;
                Drained = true;
                beforeMana = waitBeforeMana;
            }
        }
        if (CurrentMana < 0)
        {
            CurrentMana = 0;
        }
        if (CurrentMana <= 1)
        {
            Drained = true;
        }
        Static.UiManager.ManaUI(CurrentMana, Mana);

    }

    public void ResetStamina()
    {
        UsingStamina = false;
    }

    public void ResetMana()
    {
        UsingMana = false;
    }
}
