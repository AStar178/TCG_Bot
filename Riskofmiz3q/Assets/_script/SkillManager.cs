using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillManager : MonoBehaviour
{
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (Input.GetKeyDown(RougeLiter.rougeLiter.InputManager.Skill_1))
        {
            OnSkill_1();
        }
        if (Input.GetKeyDown(RougeLiter.rougeLiter.InputManager.Skill_2))
        {
            OnSkill_2();
        }
        if (Input.GetKeyDown(RougeLiter.rougeLiter.InputManager.Skill_3))
        {
            OnSkill_3();
        }
        if (Input.GetKeyDown(RougeLiter.rougeLiter.InputManager.Skill_4))
        {
            OnSkill_4();
        }
    }

    public virtual void OnSkill_1()
    {
        
    }

    public virtual void OnSkill_2()
    {

    }

    public virtual void OnSkill_3()
    {

    }

    public virtual void OnSkill_4()
    {

    }
}
