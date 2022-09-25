using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class EnemyBullent : MonoBehaviour
{
    public Damage damage;
    public List<int> layer;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (layer.Count == 0) { return; }

        if ( GETREAL(other) == false ) { return; }

        if ( !other.TryGetComponent(out IHpValue hpValue) ) {  return; } 
        if ( damage.GameObjectRefernce == hpValue ) { return; }
        hpValue.HpValueChange(damage , out var result);

        Destroy(this.gameObject);
    }

    private bool GETREAL(Collider2D other)
    {
        
        for (int i = 0; i < layer.Count; i++)
        {
            if ( layer[i] == other.gameObject.layer )
                return true;
        }

        return false;
    }
}
