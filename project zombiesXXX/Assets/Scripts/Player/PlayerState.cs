using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : PlayerComponetSystem {
    
    
    public float BaseDamage;
    public float BaseHpMax;
    public float BaseSprintSpeed = 8;
    public float BaseAttackSpeed;
    public float BaseJumpAmount = 1.2f;
    public float BaseDeffece;
    
    [HideInInspector] public float CalculatedDamage;
    [HideInInspector] public float CalculatedHpMax;
    [HideInInspector] public float CalculatedHpCurrent;
    [HideInInspector] public float CalculatedSprintSpeed;
    [HideInInspector] public float CalculatedAttackSpeed;
    [HideInInspector] public float CalculatedJumpAmount;
    [HideInInspector] public float CalculatedDeffece;
    
    [HideInInspector] public float ResultDamage;
    [HideInInspector] public float ResultHpMax;
    [HideInInspector] public float ResultHpCurrent;
    [HideInInspector] public float ResultSprintSpeed;
    [HideInInspector] public float ResultAttackSpeed;
    [HideInInspector] public float ResultJumpAmount;
    [HideInInspector] public float ResultDeffece;
    public float Luck = 1;
    public List<StateScriptAbleObject> IteamsAdd = new List<StateScriptAbleObject>();
    public List<StateScriptAbleObject> IteamsMulty = new List<StateScriptAbleObject>();
    public List<PassiveIteam> Passiveiteams = new List<PassiveIteam>();
    private void Start() {

        for (int i = 0; i < Passiveiteams.Count; i++)
        {
            Passiveiteams[i].OnStart(this);
        }
        StartNoramleCalculater();
        CalculatedHpCurrent = ResultHpMax;
        ResultHpCurrent = ResultHpMax;
 
    }

    public void StartNoramleCalculater()
    {
        ApplyBaseState();
        AddAllScriptAbleObjectITEMSBuffs();
        MulityAllScriptAbleObjectITEMSBuffs();
        ApplyResult();
    }
    private void ApplyResult()
    {
        ResultDamage = CalculatedDamage;
        ResultHpMax = CalculatedHpMax;
        ResultSprintSpeed = CalculatedSprintSpeed;
        ResultAttackSpeed = CalculatedAttackSpeed;;
        ResultJumpAmount = CalculatedJumpAmount;
        ResultDeffece = CalculatedDeffece;
    }

    private void ApplyBaseState()
    {
        CalculatedDamage = BaseDamage;
        CalculatedHpMax = BaseHpMax;
        CalculatedSprintSpeed = BaseSprintSpeed;
        CalculatedAttackSpeed = BaseAttackSpeed;
        CalculatedJumpAmount = BaseJumpAmount;
        CalculatedDeffece = BaseDeffece;
        Luck = 1;
    }

    private void AddAllScriptAbleObjectITEMSBuffs()
    {
        for (int i = 0; i < IteamsAdd.Count; i++)
        {
            CalculatedDamage += IteamsAdd[i].Damage;
            CalculatedHpMax += IteamsAdd[i].Hp;
            CalculatedSprintSpeed += IteamsAdd[i].SprintSpeed;
            CalculatedAttackSpeed += IteamsAdd[i].AttackSpeed;
            CalculatedJumpAmount += IteamsAdd[i].JumpAmount;
            CalculatedDeffece += IteamsAdd[i].Deffece;
            Luck += IteamsAdd[i].Luck;
        }
    }
    private void MulityAllScriptAbleObjectITEMSBuffs()
    {
        for (int i = 0; i < IteamsMulty.Count; i++)
        {
            CalculatedDamage *= IteamsMulty[i].Damage == 0 ? 1 : IteamsMulty[i].Damage;
            CalculatedHpMax *= IteamsMulty[i].Hp == 0 ? 1 : IteamsMulty[i].Hp;
            CalculatedSprintSpeed *= IteamsMulty[i].SprintSpeed == 0 ? 1 : IteamsMulty[i].SprintSpeed;
            CalculatedAttackSpeed *= IteamsMulty[i].AttackSpeed == 0 ? 1 : IteamsMulty[i].AttackSpeed;
            CalculatedJumpAmount *= IteamsMulty[i].JumpAmount == 0 ? 1 : IteamsMulty[i].JumpAmount;
            CalculatedDeffece *= IteamsMulty[i].Deffece == 0 ? 1 : IteamsMulty[i].Deffece;
            Luck *= IteamsMulty[i].Luck == 0 ? 1 : IteamsMulty[i].Luck;
        }
    }
    private void Update() {
        for (int i = 0; i < Passiveiteams.Count; i++)
        {
            Passiveiteams[i].OnUpdate(this);
        }
    }
    public void AddIteamPassive(PassiveIteam passiveIteam)
    {
        passiveIteam.OnStart(this);
        Passiveiteams.Add(passiveIteam);
    }
    public void AddIteam(StateScriptAbleObject state)
    {
        if(state.ModeMulity == true)
        {
            AddNewIteamMulty(state);
            return;
        }
        AddNewIteamAdd(state);
    }
    private void AddNewIteamAdd(StateScriptAbleObject state)
    {
        IteamsAdd.Add(state);
        StartNoramleCalculater();
    }
    private void AddNewIteamMulty(StateScriptAbleObject state)
    {
        IteamsAdd.Add(state);
        StartNoramleCalculater();
    }


}