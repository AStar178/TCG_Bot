using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public State stat;
    private Collider2D Tar;
    public float Range;

    private float TBS;
    public float STBS;
    private float FixSecond;
    private float FixSecondN;
    public float BulletSpeed = 1;
    bool ATTACK = false;
    [SerializeField] bool Frendly = false;
    [SerializeField] EnemyHp enemyHp;
    [SerializeField] SpriteRenderer renderers;
    public GameObject project;

    public LayerMask LayerMaskEnemy;
    public LayerMask LayerMaskFreandy;

    public void Start()
    {
        STBS = 1 / stat.AttackSpeed;
        Range = stat.AggroRange;
        project = EnemyStatic.project;
    }

    public void Update()
    {
        if (Frendly == false) { gameObject.layer = 7; }
        if (Frendly == true) { gameObject.layer = 0; }
        Tar = Physics2D.OverlapCircle(transform.position, Range, Frendly ? LayerMaskEnemy : LayerMaskFreandy);

        if (Tar != null) { ATTACK = true; }
        else { ATTACK = false; }

        if (ATTACK == true)
        {
            if (TBS <= 0)
            {
                GameObject B = Instantiate(project, transform.position, Quaternion.identity);
                SetupBullet( B );
                var tween = B.transform.DOMove(Tar.transform.position, .5f);

                KillTween(.5f , tween , B);
                if (FixSecond >= FixSecondN)
                {
                    TBS = STBS;
                    FixSecond = 0;
                }
                else
                {
                    FixSecond += Time.deltaTime;
                }
            }
            else
            {
                TBS -= Time.deltaTime;
            }
        }
        if (Tar == null) { return; }
        SpriteUpdaye();
    }
    private void SpriteUpdaye()
    {
        if ( Vector2.Dot( ( Tar.transform.position - transform.position ).normalized , Vector2.right) == 0 )
            return;
            
        if (Vector2.Dot( ( Tar.transform.position - transform.position ).normalized , Vector2.right) < -0.1f)
        {
            renderers.flipX = true;       
            return;
        } 
        renderers.flipX = false;
    }

    private void SetupBullet(GameObject b)
    {
        Damage damage = new Damage();

        damage.AdDamage = stat.AdDamage;
        damage.ApDamage = stat.ApDamage;
        damage.Ad_DefenceReduser = stat.Ad_DefenceReduser < 0 ? 1 : stat.Ad_DefenceReduser;
        damage.ApDamage = stat.Mp_DefenceReduser < 0 ? 1 : stat.Mp_DefenceReduser;
        var bullet = b.AddComponent<EnemyBullent>();
        bullet.damage = damage;
        bullet.damage.GameObjectRefernce = enemyHp;
        bullet.layerMask = Frendly ? 7 : 6;
    }

    private async void KillTween(float v, Tween tween , GameObject b)
    {
        float zz = v;
        while (zz > 0)
        {
            zz -= Time.deltaTime;
            await Task.Yield();
        }
        tween.Kill();
        Destroy(b);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {

        Handles.DrawWireDisc(transform.position, Vector3.forward, Range);

    }
#endif
}
