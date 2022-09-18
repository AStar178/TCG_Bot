using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullent : MonoBehaviour
{
    public Damage damage;
    public int layerMask = 0;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if ( 0 == layerMask ) { return; }

        if ( other.gameObject.layer != layerMask ) { return; }

        if ( !other.TryGetComponent(out IHpValue hpValue) ) { return; } 


        hpValue.HpValueChange(damage);

        Destroy(this.gameObject);
    }
}
