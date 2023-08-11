using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.VFX;

public class Turret : IteamSkill {

    [SerializeField] private GameObject Turretx;
    private List<Turrents> turrents = new List<Turrents>();

    public float Coldown;
    public float DamageAmount;
    public float Attackspeed;
    public float SpawnRange = 10;
    float xczx;
    
    public override void OnStart(PlayerState playerState)
    {
        playerState.ShowForwardIndecater = true;
    }
    public override void OnUseSkill(PlayerState playerState)
    {
        SpawnTurret();

        base.OnUseSkill(playerState);

    }
    private void Update() {
        if (xczx > 0)
            xczx -= Time.deltaTime;
        
        Icons.SetCooldown(xczx, Coldown);

        Icons.SetIconMode( DistanceCheakPlayerCameraRayCast(SpawnRange) );
        
            
        
            
        
        for (int i = 0; i < turrents.Count; i++)
        {
            turrents[i].CoolDown -= Time.deltaTime;
            var s = Physics.OverlapSphere( turrents[i].transform.position , turrents[i].AttackRange , EnmeyLayer ).OrderBy( s  => Vector3.Distance(s.transform.position , turrents[i].transform.position) ).FirstOrDefault();

            if (s == null)
                continue;
            
            turrents[i].Aim.transform.LookAt(s.transform.position);
            if (turrents[i].CoolDown < 0)
            {
                var x = CreatDamage( DamageAmount , PlayerState , out var crited );
                var c = s.GetComponent<EnemyHp>();
                c.TakeDamage(x);
                turrents[i].PlayEffect();
                PlayerState.OnAtuoAttackDealDamage?.Invoke( x , c );
                turrents[i].CoolDown = Attackspeed;
            }

        }

    }
    private void SpawnTurret()
    {
        if (xczx > 0)
            return;
        
        
        if (raycastHit.collider == null)
            return;

        if (!DistanceCheakPlayerCameraRayCast(SpawnRange))
            return;

        xczx = Coldown;
        var Turrent = Instantiate(Turretx , raycastHit.point , Quaternion.identity);
        var s = Turrent.GetComponent<Turrents>();
        s.Aim.localEulerAngles = new Vector3( 0 , UnityEngine.Random.Range(0 , 360) );

        turrents.Add(s);
    }
}
