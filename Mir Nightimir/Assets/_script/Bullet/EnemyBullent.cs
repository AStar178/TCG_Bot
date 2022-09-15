using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullent : MonoBehaviour
{
    public Damage damage;
    public Transform target;
    [SerializeField] Rigidbody2D rigidbod;
    public float Speed = 1;
    private void Start() {
        
        rigidbod = GetComponent<Rigidbody2D>();

    }
    private void Update() 
    {

        if (target == null) { return; }


        rigidbod.velocity = (target.position - transform.position).normalized * Speed;

    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        
        if (!other.TryGetComponent(out IHpValue hpValue)) { return; } 


        hpValue.HpValueChange(damage);

        Destroy(this.gameObject);
    }
}
