using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using System.Linq;

public class PlayerState : PlayerComponetSystem {

    [SerializeField]
    private bool Selected = false;

    [SerializeField]
    private string Name;
    [SerializeField] [TextArea]
    private string Description;
    public string GetName() => Name;
    public string GetDescription() => Description;
    [SerializeField]
    private bool hasSecondBar = false;
    public bool HasSecondBar() => hasSecondBar;
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
    public Action<DamageData , EnemyHp> OnAtuoAttackDealDamage;
    public Action<DamageData , EnemyHp> OnAbilityAttackDealDamage;
    public Action<DamageData, EnemyHp> OnCritied;
    public Action<DamageData, EnemyHp> OnKilledEnemy;
    public Action<DamageData> OnDamageTaken;
    public Action<DamageData> OnHealed;
    public bool ShowForwardIndecater;
    public bool Combat;

    private void Start() {
        if (Selected == false)
        {
            GetComponent<ThirdPersonCam>().Stop = true;
            GetComponent<ThirdPersonCam>().rb.useGravity = false;


            GetComponent<PlayerInputSystem>().cursorLocked = false;
            GetComponent<PlayerInputSystem>().cursorInputForLook = false;
            GetComponent<PlayerInputSystem>().SetCursorState(false);
            return; 
        }

        OnStart();
    }

    public void SetHero(GameObject Iteam, GameObject Incader)
    {
        Selected = true;
        IteamSpawn = Iteam.transform;
        incader = Incader;

        GetComponent<ThirdPersonCam>().Stop = false;
        GetComponent<ThirdPersonCam>().rb.useGravity = true;


        GetComponent<PlayerInputSystem>().cursorLocked = true;
        GetComponent<PlayerInputSystem>().cursorInputForLook = true;
        GetComponent<PlayerInputSystem>().SetCursorState(true);

        OnStart();
    }
    public void OnStart()
    {
        Instantiate(Player.PlayerMap, transform);
        var wadsadas = new List<StateScriptAbleObject>();
        for (int i = 0; i < IteamsAdd.Count; i++)
        {
            StateScriptAbleObject stateScriptAbleObjectxx = (StateScriptAbleObject)ScriptableObject.CreateInstance("StateScriptAbleObject");
            stateScriptAbleObjectxx.icone = IteamsAdd[i].icone;
            stateScriptAbleObjectxx.namex = IteamsAdd[i].namex;
            stateScriptAbleObjectxx.discrapsen = IteamsAdd[i].discrapsen;
            stateScriptAbleObjectxx.state = IteamsAdd[i].state;
            stateScriptAbleObjectxx.ModeMulity = IteamsAdd[i].ModeMulity;
            stateScriptAbleObjectxx.passiveIteam = IteamsAdd[i].passiveIteam;
            stateScriptAbleObjectxx.SkillIteam = IteamsAdd[i].SkillIteam;
            wadsadas.Add(stateScriptAbleObjectxx);
        }
        IteamsAdd.RemoveRange(0, IteamsAdd.Count);
        IteamsAdd.AddRange(wadsadas);
        var adasdasdasd = new List<StateScriptAbleObject>();
        for (int i = 0; i < IteamsMulty.Count; i++)
        {
            StateScriptAbleObject stateScriptAbleObjectxx = (StateScriptAbleObject)ScriptableObject.CreateInstance("StateScriptAbleObject");
            stateScriptAbleObjectxx.icone = IteamsMulty[i].icone;
            stateScriptAbleObjectxx.namex = IteamsMulty[i].namex;
            stateScriptAbleObjectxx.discrapsen = IteamsMulty[i].discrapsen;
            stateScriptAbleObjectxx.state = IteamsMulty[i].state;
            stateScriptAbleObjectxx.ModeMulity = IteamsMulty[i].ModeMulity;
            stateScriptAbleObjectxx.passiveIteam = IteamsMulty[i].passiveIteam;
            stateScriptAbleObjectxx.SkillIteam = IteamsMulty[i].SkillIteam;
            IteamsMulty.RemoveAt(i);
            adasdasdasd.Add(stateScriptAbleObjectxx);
        }
        IteamsMulty.RemoveRange(0, IteamsMulty.Count);
        IteamsMulty.AddRange(IteamsMulty);

        Skill = new IteamSkill[7];
        AddIteamPassive(ChampinPassive);
        AddIteamSkill(ChampinSkillQ);
        AddIteamSkill(ChampinSkillE);
        AddIteamSkill(ChampinSkillR);
        StartStartNoramleCalculater();
        CalculatedValue.HpCurrent = ResultValue.HpMax;
        ResultValue.HpCurrent = ResultValue.HpMax;

        Player.UIManager.SetHealth(ResultValue.HpCurrent, ResultValue.HpMax, this);
        List<IteamSkill> iteamSkills = new List<IteamSkill>(Skill);
        Player.UIManager.SetIcons(iteamSkills);
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
        CalculatedValue = BaseValue;
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
            CalculatedValue.Crit += IteamsAdd[i].state.Crit;
            CalculatedValue.CritDamageMulty += IteamsAdd[i].state.CritDamageMulty;
            Luck += IteamsAdd[i].state.Luck;
            if (IteamsAdd[i].ModeMulity == false && IteamsAdd[i].IteamAddAready == false) 
            { AddIteamPassive(IteamsAdd[i].passiveIteam); AddIteamSkill( IteamsAdd[i].SkillIteam ); IteamsAdd[i].IteamAddAready = true;  }
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
            CalculatedValue.Crit *= IteamsMulty[i].state.Crit == 0 ? 1 : IteamsMulty[i].state.Crit;
            CalculatedValue.CritDamageMulty *= IteamsMulty[i].state.CritDamageMulty == 0 ? 1 : IteamsMulty[i].state.CritDamageMulty;
            Luck *= IteamsMulty[i].state.Luck == 0 ? 1 : IteamsMulty[i].state.Luck;
            if (IteamsAdd[i].ModeMulity == true && IteamsAdd[i].IteamAddAready == false)
            { AddIteamPassive(IteamsMulty[i].passiveIteam); AddIteamSkill( IteamsAdd[i].SkillIteam ); IteamsAdd[i].IteamAddAready = true;  }
        }
        
    }
    private void ApplyResult()
    {
        ResultValue = CalculatedValue;
        CalculatedValue.HpCurrent = CalculatedValue.HpMax;
        OderAllIteams();
    }
    public void OderAllIteams()
    {
        Passiveiteams = Passiveiteams.OrderBy( s => s.Oderlayer * -1 ).ToList();
    }
    private void Update() {
        if (Selected == false)
            return;


        if (ShowForwardIndecater)
        {
            RenderInceter();
        }
        if (startSemelisane == false)
            return;
        State state = new State();
        state = CalculatedValue;
        CombatTimer();   
        for (int i = 0; i < Passiveiteams.Count; i++)
        {
            state = Passiveiteams[i].OnUpdate(this , ref CalculatedValue , ref state);
        }
        ResultValue = state;
        if ( Player.PlayerInputSystem.LeftButtonValue != 0 )
        {
            if (Skill[0] != null)
            {
                Skill[0].OnUseSkill(this);
            }
            Player.PlayerInputSystem.LeftButton = false;
        }
        if ( Player.PlayerInputSystem.RightButtonValue != 0 )
        {
            if (Skill[1] != null)
            {
                Skill[1].OnUseSkill(this);
            }
            Player.PlayerInputSystem.RightButton = false;
        }
        if ( Player.PlayerInputSystem.RButtonValue  != 0 )
        {
            if (Skill[2] != null)
            {
                Skill[2].OnUseSkill(this);
            }
            Player.PlayerInputSystem.R = false;
        }
        if ( Player.PlayerInputSystem.n1 )
        {
            if (Skill[3] != null)
            {
                Skill[3].OnUseSkill(this);
            }
            Player.PlayerInputSystem.n1 = false;
        }
        if ( Player.PlayerInputSystem.n2 )
        {
            if (Skill[4] != null)
            {
                Skill[4].OnUseSkill(this);
            }
            Player.PlayerInputSystem.n2 = false;
        }
        if ( Player.PlayerInputSystem.n3 )
        {
            if (Skill[5] != null)
            {
                Skill[5].OnUseSkill(this);
            }
            Player.PlayerInputSystem.n3 = false;
        }
        if ( Player.PlayerInputSystem.n4 )
        {
            if (Skill[6] != null)
            {
                Skill[6].OnUseSkill(this);
            }
            Player.PlayerInputSystem.n4 = false;
        }
        
    }

    [SerializeField] GameObject incader;
    public RaycastHit RaycastHitHit;
    private void RenderInceter()
    {
        
        Physics.Raycast( Player.Current.CameraControler.transform.position , Player.Current.CameraControler.transform.forward , out var hitInfo , Mathf.Infinity  , Player.PlayerThirdPersonController.GroundLayers );
        RaycastHitHit = hitInfo;
        if (hitInfo.collider == null)
        {
            incader.SetActive(false);
            return;
        }
            
        incader.transform.position = hitInfo.point;
        incader.SetActive(true);
    }

    public float xc;
    private void CombatTimer()
    {
        xc -= Time.deltaTime;
        if (xc < 0 && Combat == true)
        {
            Combat = false;
        }
        if (xc > 0 && Combat == false)
        {
            Combat = true;
        }
    }

    public void AddIteamPassive(IteamPassive passiveIteam)
    {
        if (passiveIteam == null)
            return;
        if (passiveIteam.itemAdded == true)
            return;
        for (int i = 0; i < Passiveiteams.Count; i++)
        {
           if ( ( passiveIteam.name + ("(Clone)") )== Passiveiteams[i].name)
            {
                Passiveiteams[i].level++;
                Debug.Log(passiveIteam.name);
                Passiveiteams[i].OnLevelUp(this);

                return;
           }
        }
        var iteasssss = Instantiate(passiveIteam , Vector3.zero , Quaternion.identity);
        iteasssss.transform.SetParent(IteamSpawn);
        iteasssss.OnStart(this);
        iteasssss.itemAdded = true;
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
        iteasssss.Icons = Player.UIManager.GetIcon(SkillsAmount - 1);
        OderAllIteams();
        List<IteamSkill> iteamSkills = new List<IteamSkill>(Skill);
        Player.UIManager.SetIcons(iteamSkills);
    }
    public void AddIteam(StateScriptAbleObject state)
    {
        if(state.ModeMulity == true)
        {
            AddNewIteamMulty(ref state);
            return;
        }
        AddNewIteamAdd(ref state);
        OderAllIteams();
    }
    private void AddNewIteamAdd(ref StateScriptAbleObject state)
    {
        StateScriptAbleObject stateScriptAbleObject = (StateScriptAbleObject)ScriptableObject.CreateInstance("StateScriptAbleObject");
        stateScriptAbleObject.icone = state.icone;
        stateScriptAbleObject.namex = state.namex;
        stateScriptAbleObject.discrapsen = state.discrapsen;
        stateScriptAbleObject.state = state.state;
        stateScriptAbleObject.ModeMulity = state.ModeMulity;
        stateScriptAbleObject.passiveIteam = state.passiveIteam;
        stateScriptAbleObject.SkillIteam = state.SkillIteam;
        IteamsAdd.Add(stateScriptAbleObject);
        StartNoramleCalculater();
        OderAllIteams();
    }
    private void AddNewIteamMulty(ref StateScriptAbleObject state)
    {
        StateScriptAbleObject stateScriptAbleObject = (StateScriptAbleObject)ScriptableObject.CreateInstance("StateScriptAbleObject");
        stateScriptAbleObject.icone = state.icone;
        stateScriptAbleObject.namex = state.namex;
        stateScriptAbleObject.discrapsen = state.discrapsen;
        stateScriptAbleObject.state = state.state;
        stateScriptAbleObject.ModeMulity = state.ModeMulity;
        stateScriptAbleObject.passiveIteam = state.passiveIteam;
        stateScriptAbleObject.SkillIteam = state.SkillIteam;
        IteamsAdd.Add(stateScriptAbleObject);
        StartNoramleCalculater();
        OderAllIteams();
    }

    public void OnDamageTake(DamageData data)
    {
        Player.UIManager.SetHealth(ResultValue.HpCurrent, ResultValue.HpMax, this);
        OnDamageTaken?.Invoke(data);
    }
    public void OnHeal(DamageData data)
    {
        Player.UIManager.SetHealth(ResultValue.HpCurrent, ResultValue.HpMax, this);
        OnHealed?.Invoke(data);
    }
}