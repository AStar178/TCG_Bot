using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class Turret : TESTei
{
    public State stat;
    private Collider2D Tar;

    private float TBS;
    public float STBS;
    private float FixSecond;
    private float FixSecondN;
    public float BulletSpeed = 1;
    bool ATTACK = false;
    [SerializeField] EnemyHp enemyHp;
    [SerializeField] SpriteRenderer renderers;
    public GameObject project;
    public Sprite ProjectileImage;


    public override void start()
    {
        STBS = 1 / stat.AttackSpeed;
        Range = stat.AggroRange;
        project = AIStatic.project;
    }

    public override void update()
    {

        Tar = Physics2D.OverlapCircle(transform.position, Range, targetLayerMask);

        if (Tar != null) { ATTACK = true; }
        else { ATTACK = false; }

        if (ATTACK == true)
        {
            if (TBS <= 0)
            {
                GameObject B = Instantiate(project, transform.position, Quaternion.identity);
                if (ProjectileImage != null)
                {
                    B.GetComponent<SpriteRenderer>().sprite = ProjectileImage;
                }
                Rpg.SetupBullet( B , stat , enemyHp , this.gameObject );
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
}
