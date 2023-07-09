using UnityEngine;

public abstract class PassiveIteam : MonoBehaviour {
    
    public Sprite Icon;
    public string namex;
    public string dependencies;
    public virtual void OnStart(PlayerState playerState)
    {
        
    }
    public virtual void OnUpdate(PlayerState playerState)
    {

    }
    public virtual void OnDrop(PlayerState playerState)
    {

    }
}