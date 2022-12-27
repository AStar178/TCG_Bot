using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class bulit : MonoBehaviour
{
    [SerializeField] LayerMask Mask;
    [SerializeField] VisualEffect visualEffect;
    [SerializeField] int Damage;
    private float tsss = 20;

    private void OnTriggerEnter(Collider other) {
        
        bool Trues = UnityExtensions.Contains( Mask , other.gameObject.layer );

        if ( Trues )
            Colided(other); 

    }
    private void Update() {

        if (tsss > 0)
        {
            tsss -= Time.deltaTime;
            return;
        }
        tsss = 20;
        EnemySpawner.Bulits.Release(gameObject);
    }

    private void Colided(Collider other)
    {
        if ( other.TryGetComponent(out EnemyHp enemy) )
        {
            var owo = EnemySpawner.Bulitseffect.Get();
            owo.gameObject.transform.position = transform.position;

            enemy.TakeDamage(Damage);
            DestroyObjectInTime(owo.gameObject);

            EnemySpawner.Bulits.Release(gameObject);
        }
    }

    private async void DestroyObjectInTime(GameObject dsad)
    {
        float f = 10;

        while ( f > 0)
        {
            f -= Time.deltaTime;
            await Task.Yield();
        }

        EnemySpawner.Bulitseffect.Release(dsad.gameObject );
    }
}
 
