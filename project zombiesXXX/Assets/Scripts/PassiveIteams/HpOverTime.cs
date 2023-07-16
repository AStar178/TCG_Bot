using UnityEngine;

public class HpOverTime : IteamPassive {

    [SerializeField] float HellingTimer = 1;
    float t;
    public override State OnUpdate(PlayerState playerState , ref State Calcuelated , ref State state)
    {
        t -= Time.deltaTime;

        if (t > 0)
            return state;
        t = HellingTimer;
        state.HpCurrent += playerState.ResultValue.HpMax * 0.01f;
        state.HpCurrent = Mathf.Clamp(state.HpCurrent , 0 , playerState.ResultValue.HpMax);
        return state;
    }
}