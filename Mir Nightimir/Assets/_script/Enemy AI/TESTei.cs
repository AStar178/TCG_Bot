using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;
using System;

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
    public float lungColdown;
    public bool NoChase;
    [SerializeField] protected SpriteRenderer SpriteRenderer;
    public void Awake()
    {
        awake();
    }

    public virtual void awake()
    {

    }

    public void Start()
    {
        start();
    }

    public virtual void start()
    {
        target = FindObjectOfType<PlayerMoveMent>().gameObject;
        lungColdown = LungCooldown;
    }

    public void Update()
    {
        update();
    }

    public virtual void update()
    {
        if (SpriteRenderer != null)
            SpriteFreeFire();
        if (NoChase == false)
        {
            if (Vector2.Distance(gameObject.transform.position, target.transform.position) <= Range && Vector2.Distance(gameObject.transform.position, target.transform.position) >= .35f)
            { transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime); }
        }
        if (Coward == true)
        {
            Cowar();
        }
        if (Lung == true)
        {
            Lun();
        }
    }
    public virtual void Cowar()
    {
        if (Vector2.Distance(gameObject.transform.position, target.transform.position) <= CowardenessRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, -Speed * Time.deltaTime);
            NoChase = true;
        }
        else NoChase = false;
    }
    public virtual void Lun()
    {
        if (lungColdown > 0)
            lungColdown = lungColdown - Time.deltaTime;

        if (lungColdown <= 0)
        {
            if (Vector2.Distance(gameObject.transform.position, target.transform.position) <= LungRange)
            {
                lungColdown = LungCooldown;
                gameObject.transform.DOMove(target.transform.position, 1 / (Speed * 2));

                NoChase = true;
                new WaitForSeconds(1 / Speed);
                NoChase = false;
            }
        }
    }

    private void SpriteFreeFire()
    {
            
        if (Vector2.Dot( ( target.transform.position - transform.position ).normalized , Vector2.left ) < -0.1f)
        {
            SpriteRenderer.flipX = true;            
            return;
        } 
        SpriteRenderer.flipX = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IHpValue>(out var Hp))
        {
            Damage damage = new Damage();
            damage.AdDamage = state.AdDamage;
            Hp.HpValueChange(damage, out var result);
        }
    }

#if UNITY_EDITOR
    public void OnDrawGizmosSelected()
    {

        Handles.DrawWireDisc(transform.position, Vector3.forward, Range);

    }
#endif
}
