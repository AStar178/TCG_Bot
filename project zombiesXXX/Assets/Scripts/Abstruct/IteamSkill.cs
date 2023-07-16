using UnityEngine;

public abstract class IteamSkill : Iteam {
    public bool useskill;
    public Sprite IconSkill;
    public string namexSkill;
    public string dependenciesSkill;
    public virtual void OnUseSkill(PlayerState playerState)
    {
        playerState.Combat = true;
    }


}