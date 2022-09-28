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
    public float MeleeAttackSpeed = 1;
    public Collider2D target;
    public List<int> targetLayer = new List<int>();
    public LayerMask targetLayerMask;
    public bool HitTheWall;
    public float WallCheck;
    public LayerMask Wall = 13;

    public bool Coward;
    public float CowardenessRange;
    public bool Lung;
    public float LungRange;
    public float LungCooldown;
    [HideInInspector]
    public float lungColdown;
    public bool NoChase;
    float AttackSpeedTime;
    [SerializeField] protected SpriteRenderer SpriteRenderer;
    public void Awake()
    {
        awake();
    }

    public virtual void awake()
    {

    }

    public virtual void ChangeTargetSelecting(LayerMask layer , List<int> enemylayer , Rpg.EnemyTeam team)
    {   
        
        gameObject.layer = ((int)team);
        targetLayerMask = layer;
        targetLayer = enemylayer;
        target = null;

    }

    public void Start()
    {
        start();
    }

    public virtual void start()
    {
        lungColdown = LungCooldown;
    }

    public void Update()
    {
        AttackSpeedTime -= Time.deltaTime;
        if (target == null )
        {
            target = GetTarget();
            return;
        }
        if ( IsEnemy(target) == false )
        {
            target = GetTarget();
            return;
        }
            
        update();
    }

    public virtual Collider2D GetTarget()
    {
        return Physics2D.OverlapCircle( transform.position , Range , targetLayerMask );
    }

    public virtual void update()
    {
        if (SpriteRenderer != null)
            SpriteFreeFire();
        if (NoChase == false)
        {
            if (HitTheWall == false)
            {
                if (Vector2.Distance(gameObject.transform.position, target.transform.position) <= Range && Vector2.Distance(gameObject.transform.position, target.transform.position) >= .35f)
                { transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime); }
                else
                {
                    target = Physics2D.OverlapCircle(transform.position, Range, targetLayerMask);
                }
            }
                
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
        if (target == null) { return; }
        if (Vector2.Dot( ( target.transform.position - transform.position ).normalized , Vector2.left ) < -0.1f)
        {
            SpriteRenderer.flipX = true;            
            return;
        } 
        SpriteRenderer.flipX = false;
    }
    protected List<int> Get_BulitLayer()
    {
        List<int> list = new List<int>();
        if (gameObject.layer == (int)Rpg.EnemyTeam.Player)
        {
            list.Add( 7 );
            return list;
        }

        list.Add ( 10 );
        list.Add ( 6 );
        return list;
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        if (AttackSpeedTime > 0) { return; }
        if ( IsEnemy( other ) == false ) { return; }
        if (other.TryGetComponent<IHpValue>(out var Hp))
        {
            Damage damage = new Damage();
            damage.AdDamage = state.AdDamage;
            Hp.HpValueChange(damage, out var result);
            AttackSpeedTime = MeleeAttackSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D yes)
    {
        if (yes != null)
        {
            if (yes.gameObject.layer == Wall)
                HitTheWall = true;
        }
        else { HitTheWall = false; }
    }

    private bool IsEnemy(Collider2D other)
    {
        
        for (int i = 0; i < targetLayer.Count; i++)
        {
            if ( targetLayer[i] == other.gameObject.layer )
                return true;
        }

        return false;
    }

#if UNITY_EDITOR
    public void OnDrawGizmosSelected()
    {

        Handles.DrawWireDisc(transform.position, Vector3.forward, Range);

    }
#endif
}

