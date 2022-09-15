using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullent : MonoBehaviour
{
    public Damage damage;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        
        if (!other.TryGetComponent(out IHpValue hpValue)) { return; } 


        hpValue.HpValueChange(damage);

        Destroy(this.gameObject);
    }
}
