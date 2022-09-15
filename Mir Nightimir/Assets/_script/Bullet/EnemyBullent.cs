using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullent : MonoBehaviour
{
    public Damage damage;
    bool yes;
    float zzzz = 0.1f;
    private void Update() 
    {
        zzzz -= Time.deltaTime;
        if (zzzz < 0)
            yes = true;
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (yes == false) { return; }
        if (!other.TryGetComponent(out IHpValue hpValue)) { return; } 


        hpValue.HpValueChange(damage);

        Destroy(this.gameObject);
    }
}
