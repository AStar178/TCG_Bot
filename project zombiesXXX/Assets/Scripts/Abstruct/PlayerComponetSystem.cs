using UnityEngine;

public abstract class PlayerComponetSystem : MonoBehaviour {

    public Player Player => Player.Current;
    public float GetHpCurrent => Player.PlayerState.ResultValue.HpCurrent;
    public float SetHpCurrent(float nexw) => Player.PlayerState.CalculatedValue.HpCurrent = nexw;
}