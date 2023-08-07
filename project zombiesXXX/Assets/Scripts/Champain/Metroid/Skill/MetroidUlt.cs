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
        if ( metr.Energy <= 0 )
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
            MetroidEffect.Current.StartLazer();
            on = true;
        }
        var s = Vector3.Distance(MetroidEffect.Current.lineRenderer.transform.position , playerState.Player.PlayerTargetSystem.Target.transform.position) < 5f ? MetroidEffect.Current.transform.parent.forward  : MetroidEffect.Current.transform.parent.forward * 5;
        MetroidEffect.Current.lineRenderer.SetPosition(0 , MetroidEffect.Current.transform.position);
        MetroidEffect.Current.lineRenderer.SetPosition(1 , MetroidEffect.Current.transform.position + s);
        MetroidEffect.Current.lineRenderer.SetPosition(2 , playerState.Player.PlayerTargetSystem.Target.transform.position);

        if (xcxzc > 0)
            return;

        var Damage = CreatDamage(playerState.ResultValue.Damage , playerState , out var crited);
        var enemy = playerState.Player.PlayerTargetSystem.Target.GetComponent<EnemyHp>();

        enemy.TakeDamage( Damage );
        metr.DamageEnergy(EnerhyCost);
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
                MetroidEffect.Current.StopLazer();
                on = false;
            }
        

    }

}