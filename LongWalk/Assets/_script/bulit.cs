using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class bulit : MonoBehaviour
{
    [SerializeField] LayerMask Mask;
    [SerializeField] VisualEffect visualEffect;
    [SerializeField] int Damage;
    private void OnTriggerEnter(Collider other) {
        
        bool Trues = UnityExtensions.Contains( Mask , other.gameObject.layer );

        if ( Trues )
            Colided(other); 

    }

    private void Colided(Collider other)
    {
        if ( other.TryGetComponent(out EnemyHp enemy) )
        {
            var owo = Instantiate(visualEffect , transform.position , Quaternion.identity);
            enemy.TakeDamage(Damage);
            Destroy(owo , 10);

            Destroy(gameObject);
        }
    }
}
 
