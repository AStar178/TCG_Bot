using UnityEngine;

public abstract class PassiveIteam : MonoBehaviour {
    public Sprite Icon;
    public string namex;
    public string dependencies;
    public virtual void OnStart(PlayerState playerState)
    {

    }
    public virtual State OnUpdateAdd(PlayerState playerState , State state)
    {
        return state;
    }
    public virtual State OnUpdateMultiy(PlayerState playerState , State state)
    {
        return state;
    }
    public virtual void OnUseSkill(PlayerState playerState)
    {

    }

    public virtual void OnDrop(PlayerState playerState)
    {

    }
    public bool useskill;
    public Sprite IconSkill;
    public string namexSkill;
    public string dependenciesSkill;
}