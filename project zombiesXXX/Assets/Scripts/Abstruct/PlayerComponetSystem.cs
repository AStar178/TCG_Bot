using UnityEngine;

public abstract class PlayerComponetSystem : MonoBehaviour {

    public Player GetPlayer()
    {
        return Player.Current;
    }

}