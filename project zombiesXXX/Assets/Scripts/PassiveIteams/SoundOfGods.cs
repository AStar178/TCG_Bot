using UnityEngine;

public class SoundOfGods : IteamPassive {
    
    int stackAmount;
    [SerializeField] float Timer;
    private void OnEnable() {
        
        Player.Current.PlayerState.OnAbilityAttackDealDamage += AbilityUsed;

    }

    private void AbilityUsed(DamageData data, EnemyHp hp)
    {
        stackAmount++;
        t = Timer;

        if (stackAmount < 4)
            return;

        Player.Current.PlayerState.CalculatedValue.HpCurrent += Player.Current.PlayerState.CalculatedValue.HpMax * 0.01f;
        Player.Current.PlayerState.CalculatedValue.HpCurrent = Mathf.Clamp( Player.Current.PlayerState.CalculatedValue.HpCurrent , 0 , Player.Current.PlayerState.CalculatedValue.HpMax );
    }

    private void OnDisable() {
        
        Player.Current.PlayerState.OnAbilityAttackDealDamage -= AbilityUsed;

    }
    float t;
    public override State OnUpdate(PlayerState playerState, ref State CalucatedValue, ref State state)
    {
        stackAmount = Mathf.Clamp(stackAmount , 0 , 4);
        t -= Time.deltaTime;
        if(t < 0)
        {
            stackAmount = 0;
        }

        if (stackAmount >= 4)
        {
            state.Crit += 1;
            state.CritDamageMulty += 2;
        }


        return base.OnUpdate(playerState, ref CalucatedValue, ref state);
    }


}