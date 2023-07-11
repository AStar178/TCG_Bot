using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerState : PlayerComponetSystem {
    
    
    [SerializeField] public State BaseValue;
    [SerializeField] public State CalculatedValue;
    public State ResultValue;
    public int Luck = 1;
    public List<StateScriptAbleObject> IteamsAdd = new List<StateScriptAbleObject>();
    public List<StateScriptAbleObject> IteamsMulty = new List<StateScriptAbleObject>();
    public List<PassiveIteam> Passiveiteams = new List<PassiveIteam>();
    [SerializeField] Transform IteamSpawn;
    bool startSemelisane;

    public bool Combat;

    private void Start() {
        StartStartNoramleCalculater();
        CalculatedValue.HpCurrent = ResultValue.HpMax;
        ResultValue.HpCurrent = ResultValue.HpMax;
    }
    public void StartStartNoramleCalculater()
    {
        ApplyBaseState();
        AddAllScriptAbleObjectITEMSBuffs();
        MulityAllScriptAbleObjectITEMSBuffs();
        ApplyResult();
        startSemelisane = true;
    }
    public void StartNoramleCalculater()
    {
        ApplyBaseState();
        AddAllScriptAbleObjectITEMSBuffs();
        MulityAllScriptAbleObjectITEMSBuffs();
        ApplyResult();
    }
    private void ApplyBaseState()
    {
        CalculatedValue.Damage = BaseValue.Damage;
        CalculatedValue.HpMax = BaseValue.HpMax;
        CalculatedValue.SprintSpeed = BaseValue.SprintSpeed;
        CalculatedValue.AttackSpeed = BaseValue.AttackSpeed;
        CalculatedValue.JumpAmount = BaseValue.JumpAmount;
        CalculatedValue.Deffece = BaseValue.Deffece;
        CalculatedValue.AttackRange = BaseValue.AttackRange;
        Luck = 1;
    }
    private void AddAllScriptAbleObjectITEMSBuffs()
    {
        for (int i = 0; i < IteamsAdd.Count; i++)
        {
            CalculatedValue.Damage += IteamsAdd[i].state.Damage;
            CalculatedValue.HpMax += IteamsAdd[i].state.HpMax;
            CalculatedValue.SprintSpeed += IteamsAdd[i].state.SprintSpeed;
            CalculatedValue.AttackSpeed += IteamsAdd[i].state.AttackSpeed;
            CalculatedValue.JumpAmount += IteamsAdd[i].state.JumpAmount;
            CalculatedValue.Deffece += IteamsAdd[i].state.Deffece;
            CalculatedValue.AttackRange += IteamsAdd[i].state.AttackRange;
            Luck += IteamsAdd[i].state.Luck;
            AddIteamPassive(IteamsAdd[i].passiveIteam);
        }
    }
    private void MulityAllScriptAbleObjectITEMSBuffs()
    {
        for (int i = 0; i < IteamsMulty.Count; i++)
        {
            CalculatedValue.Damage *= IteamsMulty[i].state.Damage == 0 ? 1 : IteamsMulty[i].state.Damage;
            CalculatedValue.HpMax *= IteamsMulty[i].state.HpMax == 0 ? 1 : IteamsMulty[i].state.HpMax;
            CalculatedValue.SprintSpeed *= IteamsMulty[i].state.SprintSpeed == 0 ? 1 : IteamsMulty[i].state.SprintSpeed;
            CalculatedValue.AttackSpeed *= IteamsMulty[i].state.AttackSpeed == 0 ? 1 : IteamsMulty[i].state.AttackSpeed;
            CalculatedValue.JumpAmount *= IteamsMulty[i].state.JumpAmount == 0 ? 1 : IteamsMulty[i].state.JumpAmount;
            CalculatedValue.Deffece *= IteamsMulty[i].state.Deffece == 0 ? 1 : IteamsMulty[i].state.Deffece;
            CalculatedValue.AttackRange *= IteamsMulty[i].state.AttackRange == 0 ? 1 : IteamsMulty[i].state.AttackRange;
            Luck *= IteamsMulty[i].state.Luck == 0 ? 1 : IteamsMulty[i].state.Luck;
            AddIteamPassive(IteamsMulty[i].passiveIteam);
        }
    }
    private void ApplyResult()
    {
        ResultValue.Damage = CalculatedValue.Damage;
        ResultValue.HpMax = CalculatedValue.HpMax;
        ResultValue.SprintSpeed = CalculatedValue.SprintSpeed;
        ResultValue.AttackSpeed = CalculatedValue.AttackSpeed;;
        ResultValue.JumpAmount = CalculatedValue.JumpAmount;
        ResultValue.Deffece = CalculatedValue.Deffece;
        ResultValue.AttackRange = CalculatedValue.AttackRange;
    }

    private void Update() {
        if (startSemelisane == false)
            return;
        State state = new State();
        state = CalculatedValue;
        for (int i = 0; i < Passiveiteams.Count; i++)
        {
            state = Passiveiteams[i].OnUpdateAddOverTime(this , ref CalculatedValue);
        }
        for (int i = 0; i < Passiveiteams.Count; i++)
        {
            Passiveiteams[i].OnUpdateMultiyMYHEADISDIEING(this , ref state);
        }
        ResultValue = state;
        for (int i = 0; i < Passiveiteams.Count; i++)
        {
            Passiveiteams[i].OnUpdate(this);
        }
    }
    public void AddIteamPassive(PassiveIteam passiveIteam)
    {
        for (int i = 0; i < Passiveiteams.Count; i++)
        {
           if ( passiveIteam.name + "(Clone)" == Passiveiteams[i].name )
           {
                Passiveiteams[i].level++;
                return;
           }
        }
        var iteasssss = Instantiate(passiveIteam , Vector3.zero , Quaternion.identity);
        iteasssss.transform.SetParent(IteamSpawn);
        iteasssss.OnStart(this);
        Passiveiteams.Add(iteasssss);
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