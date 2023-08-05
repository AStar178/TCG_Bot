using System;
using UnityEngine;

public abstract class IteamSkill : Iteam {
    public bool useskill;
    public Sprite IconSkill;
    public string namexSkill;
    public string dependenciesSkill;
    public Icons Icons;
    public virtual void OnUseSkill(PlayerState playerState)
    {
       
    }

}