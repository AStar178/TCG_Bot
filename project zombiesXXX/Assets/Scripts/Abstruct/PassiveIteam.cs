using UnityEngine;

public abstract class PassiveIteam : MonoBehaviour {
    
    public StateScriptAbleObject stateScriptAbleObject;
    public virtual void OnStart(PlayerState playerState)
    {
        playerState.AddIteam(stateScriptAbleObject);
    }
    public virtual void OnUpdate(PlayerState playerState)
    {

    }
    public virtual void OnDrop(PlayerState playerState)
    {

    }
}