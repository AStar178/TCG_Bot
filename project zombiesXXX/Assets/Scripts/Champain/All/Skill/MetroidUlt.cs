using UnityEngine;

public class MetroidUlt : IteamSkill {

    [SerializeField] float CouldDown = 2;
    [SerializeField] float AttackSpeed = 0.1f;
    [SerializeField] float EnerhyCost;
    float xcxzc;
    float tt;
    bool on;
    private MetroidEnergy metr;

    private void Start() {
        
        metr = Player.Current.PlayerEffect.GetComponent<MetroidEnergy>();

    }
    private void Update() {
        
        if (Player.Current.PlayerInputSystem.RButtonValue == 0)
        {
            DIESDASDASDAS();
        }

    }
    public override void OnUseSkill(PlayerState playerState)
    {
        tt -= Time.deltaTime;
        xcxzc -= Time.deltaTime;
        if ( metr.Energy < EnerhyCost )
        {
            DIESDASDASDAS();
            return;
        }
            
        if (playerState.Player.PlayerTargetSystem.Target == null)
        {
            DIESDASDASDAS();
            return;
        }
        if (tt > 0)
        {
            DIESDASDASDAS();
            return;
        }

        if (on == false)
        {
            Player.Current.PlayerEffect.StartLazer();
            on = true;
        }
        var s = Vector3.Distance(playerState.Player.PlayerEffect.lineRenderer.transform.position , playerState.Player.PlayerTargetSystem.Target.transform.position) < 5f ? playerState.Player.PlayerEffect.lineRenderer.transform.parent.forward  : playerState.Player.PlayerEffect.lineRenderer.transform.parent.forward * 5;
        playerState.Player.PlayerEffect.lineRenderer.SetPosition(0 , playerState.Player.PlayerEffect.lineRenderer.transform.position);
        playerState.Player.PlayerEffect.lineRenderer.SetPosition(1 , playerState.Player.PlayerEffect.lineRenderer.transform.position + s);
        playerState.Player.PlayerEffect.lineRenderer.SetPosition(2 , playerState.Player.PlayerTargetSystem.Target.transform.position);

        if (xcxzc > 0)
            return;

        var Damage = CreatDamage(playerState.ResultValue.Damage , playerState , out var crited);
        var enemy = playerState.Player.PlayerTargetSystem.Target.GetComponent<EnemyHp>();

        enemy.TakeDamage( Damage );
        metr.LostEnergy(EnerhyCost);
        playerState.OnAtuoAttackDealDamage?.Invoke(Damage , enemy);
        if (crited == true)
        {
            playerState.OnAbilityAttackDealDamage?.Invoke(Damage , enemy);
        }


        xcxzc = AttackSpeed;
        InCombat();


        base.OnUseSkill(playerState);
    }

    private void DIESDASDASDAS() {
        
        
            if (on == true)
            {
                tt = CouldDown;
                Player.Current.PlayerEffect.StopLazer();
                on = false;
            }
        

    }

}