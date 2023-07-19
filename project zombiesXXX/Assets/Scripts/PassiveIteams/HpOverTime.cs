using UnityEngine;

public class HpOverTime : IteamPassive {

    [SerializeField] float HellingTimer = 1;
    float t;
    [SerializeField] GameObject Effect;
    private GameObject effect;

    public override void OnDrop(PlayerState playerState)
    {
        Destroy(effect);
    }

    public override State OnUpdate(PlayerState playerState , ref State Calcuelated , ref State state)
    {
        if (effect == null)
            effect = Instantiate(Effect);

        effect.transform.position = playerState.Player.PlayerEffect.feetpos.position;

        t -= Time.deltaTime;

        if (t > 0)
            return state;

        t = HellingTimer;
        float f = playerState.CalculatedValue.HpCurrent + playerState.ResultValue.HpMax * 0.01f;
        f = Mathf.Clamp(f , 0 , playerState.ResultValue.HpMax);
        playerState.SetHpCurrent(f);
        return state;
    }
}