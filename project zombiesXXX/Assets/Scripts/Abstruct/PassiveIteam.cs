using UnityEngine;

public abstract class PassiveIteam : MonoBehaviour {
    public Sprite Icon;
    public string namex;
    public string dependencies;
    public virtual void OnStart(PlayerState playerState)
    {
        
    }
    public virtual void OnUpdateAdd(PlayerState playerState , State state)
    {

    }
    public virtual void OnUpdateMultiy(PlayerState playerState , State state)
    {

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