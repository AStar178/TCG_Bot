using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerState : PlayerComponetSystem {
    
    
    [SerializeField] public State BaseValue;
    [SerializeField] public State CalculatedValue;
    private State ChangeValue;
    public State ResultValue;
    public int Luck = 1;
    public float InvisableTime;
    public List<StateScriptAbleObject> IteamsAdd = new List<StateScriptAbleObject>();
    public List<StateScriptAbleObject> IteamsMulty = new List<StateScriptAbleObject>();
    public List<IteamPassive> Passiveiteams = new List<IteamPassive>();
    public IteamSkill[] Skill;
    public IteamPassive ChampinPassive;
    public IteamSkill ChampinSkillQ;
    public IteamSkill ChampinSkillE;
    public IteamSkill ChampinSkillR;
    [SerializeField] Transform IteamSpawn;
    bool startSemelisane;

    public bool Combat;

    private void Start() {
        Skill = new IteamSkill[7];
        AddIteamPassive(ChampinPassive);
        AddIteamSkill(ChampinSkillQ);
        AddIteamSkill(ChampinSkillE);
        AddIteamSkill(ChampinSkillR);
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
        OderAllIteams();
    }
    public void OderAllIteams()
    {
        Passiveiteams = Passiveiteams.OrderBy( s => s.Oderlayer * -1 ).ToList();
    }
    private void Update() {
        if (startSemelisane == false)
            return;
        State state = new State();
        state = CalculatedValue;
        
        for (int i = 0; i < Passiveiteams.Count; i++)
        {
            state = Passiveiteams[i].OnUpdate(this , ref CalculatedValue , ref state);
        }
        ResultValue = state;
        if ( Player.PlayerInputSystem.Q )
        {
            if (Skill[0] != null)
            {
                Skill[0].OnUseSkill(this);
            }
            Player.PlayerInputSystem.Q = false;
        }
        if ( Player.PlayerInputSystem.E )
        {
            if (Skill[1] != null)
            {
                Skill[1].OnUseSkill(this);
            }
            Player.PlayerInputSystem.E = false;
        }
        if ( Player.PlayerInputSystem.n1 )
        {
            if (Skill[2] != null)
            {
                Skill[2].OnUseSkill(this);
            }
            Player.PlayerInputSystem.n1 = false;
        }
        if ( Player.PlayerInputSystem.n2 )
        {
            if (Skill[3] != null)
            {
                Skill[3].OnUseSkill(this);
            }
            Player.PlayerInputSystem.n2 = false;
        }
        if ( Player.PlayerInputSystem.n3 )
        {
            if (Skill[4] != null)
            {
                Skill[4].OnUseSkill(this);
            }
            Player.PlayerInputSystem.n3 = false;
        }
        if ( Player.PlayerInputSystem.n4 )
        {
            if (Skill[5] != null)
            {
                Skill[5].OnUseSkill(this);
            }
            Player.PlayerInputSystem.n4 = false;
        }
        if ( Player.PlayerInputSystem.n5 )
        {
            if (Skill[6] != null)
            {
                Skill[6].OnUseSkill(this);
            }
            Player.PlayerInputSystem.n5 = false;
        }
        
    }
    public void AddIteamPassive(IteamPassive passiveIteam)
    {
        if (passiveIteam == null)
            return;
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
        OderAllIteams();
    }
    int SkillsAmount;
    public void AddIteamSkill(IteamSkill skill)
    {
        if (skill == null)
            return;
        SkillsAmount++;
        if ( SkillsAmount > Skill.Length )
        {
            SkillsAmount--;
            return;
        }
        var iteasssss = Instantiate(skill , Vector3.zero , Quaternion.identity);
        iteasssss.transform.SetParent(IteamSpawn);
        iteasssss.OnStart(this);
        Skill[SkillsAmount - 1] = iteasssss;
        OderAllIteams();
    }
    public void AddIteam(StateScriptAbleObject state)
    {
        if(state.ModeMulity == true)
        {
            AddNewIteamMulty(state);
            return;
        }
        AddNewIteamAdd(state);
        OderAllIteams();
    }
    private void AddNewIteamAdd(StateScriptAbleObject state)
    {
        IteamsAdd.Add(state);
        StartNoramleCalculater();
        OderAllIteams();
    }
    private void AddNewIteamMulty(StateScriptAbleObject state)
    {
        IteamsAdd.Add(state);
        StartNoramleCalculater();
        OderAllIteams();
    }

}