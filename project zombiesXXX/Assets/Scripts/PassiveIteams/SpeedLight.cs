using System;
using System.Threading.Tasks;
using UnityEngine;

public class SpeedLight : IteamPassive {

    public ParticleSystem particleSystemLighting2;
    ParticleSystem particleSystemc;
    public Material particalMatrial;
    float pepe;
    public override void OnStart(PlayerState playerState)
    {
        var s = Instantiate(particleSystemLighting2 , PlayerState.transform);
        particleSystemc = s;
        
        PlayerState.OnAtuoAttackDealDamage += OnATuo;
    }

    private void OnDisable() {

        PlayerState.OnAtuoAttackDealDamage -= OnATuo;

    }

    private void OnATuo(DamageData data, EnemyHp hp)
    {
        pepe += 3;
        pepe = Mathf.Clamp( pepe , 0 , 9 );
        
        KillCoulDown();
    }
    public override State OnUpdate(PlayerState playerState , ref State CalucatedValue , ref State state)
    {
        state.AttackSpeed += state.AttackSpeed * Mathf.InverseLerp(0 , 9 , pepe) * 2;
        return state;
    }
    bool started;
    private async void KillCoulDown()
    {
        if (started)
            return;

        while (pepe > 0)
        {
            pepe -= Time.deltaTime;
            particalMatrial.SetFloat( "_WOW" ,  Mathf.InverseLerp(0 , 9 , pepe));
            if (Player.Current.PlayerEffect.meshFilter != null)
            {
                var mesh = new Mesh();
                Player.Current.PlayerEffect.meshFilter.BakeMesh(mesh);
                var shape = particleSystemc.shape;
                shape.enabled = true;
                shape.shapeType = ParticleSystemShapeType.Mesh;
                shape.mesh = mesh;
            }
            else
            {
                particleSystemc.transform.localPosition = Vector3.up * 1.65f;
            }
                 
            started = true;
            await Task.Yield();
        }
        started = false;
    }
}