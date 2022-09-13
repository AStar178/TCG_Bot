using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTei : MonoBehaviour
{
    public State state;
    public float Speed;
    public float Range;
    GameObject target;

    private void Start()
    {
        target = FindObjectOfType<PlayerMoveMent>().gameObject;
    }

    private void Update()
    {
        if (Vector2.Distance(gameObject.transform.position, target.transform.position) <= Range)
        { transform.position = Vector2.MoveTowards(transform.position, target.transform.position, state.MoveSpeed * Time.deltaTime); }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IHpValue>(out var Hp))
        {
            Damage damage = new Damage();
            damage.AdDamage = state.AdDamage;
            Hp.HpValueChange(damage);
            Debug.Log("Naaaaaaaaaaaaaaaaaaaa~");
        }
    }
}
