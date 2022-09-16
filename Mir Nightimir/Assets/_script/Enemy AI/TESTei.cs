using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class TESTei : MonoBehaviour
{
    public State state;
    public float Speed;
    public float Range;
    public GameObject target;

    public bool Coward;
    public float CowardenessRange;
    public bool Lung;
    public float LungRange;
    public float LungCooldown;
    [HideInInspector]
    public bool NoChase;

    private void Awake()
    {
        awake();
    }

    public virtual void awake()
    {

    }

    private void Start()
    {
        start();
    }

    public virtual void start()
    {
        target = FindObjectOfType<PlayerMoveMent>().gameObject;
    }

    private void Update()
    {
        update();
    }

    public virtual void update()
    {
        if (NoChase == false)
        {
            if (Vector2.Distance(gameObject.transform.position, target.transform.position) <= Range)
            { transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime); }
        }
        if (Coward == true)
        {
            if (Vector2.Distance(gameObject.transform.position, target.transform.position) <= CowardenessRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, -Speed * Time.deltaTime);
                NoChase = true;
            }
            else NoChase = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IHpValue>(out var Hp))
        {
            Damage damage = new Damage();
            damage.AdDamage = state.AdDamage;
            Hp.HpValueChange(damage);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {

        Handles.DrawWireDisc(transform.position, Vector3.forward, Range);

    }
#endif
}
