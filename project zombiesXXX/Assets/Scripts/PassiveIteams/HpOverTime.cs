using UnityEngine;

public class HpOverTime : PassiveIteam {

    [SerializeField] float HellingTimer = 1;
    float t;
    public override State OnUpdateAdd(PlayerState playerState , State state)
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