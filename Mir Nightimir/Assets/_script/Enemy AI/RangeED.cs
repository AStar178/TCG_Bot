using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class RangeED : TESTei
{
    private float TBS;
    public float STBS;
    private float FixSecond;
    private float FixSecondN;
    public Sprite ProjectileImage;
    bool ATTACK = false;

    public GameObject project;
    [SerializeField] EnemyHp enemyHp;
    public override void start()
    {
        base.start();
        project = EnemyStatic.project;
    }

    public override void update()
    {
        if (NoChase == false)
        {
            // check if hit a wall
            if (HitTheWall == false)
            {
                if (Vector2.Distance(gameObject.transform.position, target.transform.position) <= Range)
                { transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime); }
            }
            if (Vector2.Distance(gameObject.transform.position, target.transform.position) <= Range)
            { ATTACK = true; }
            else ATTACK = false;
            if (Vector2.Distance(gameObject.transform.position, target.transform.position) <= 1 + Range * 0.1f)
            { transform.position = Vector2.MoveTowards(transform.position, target.transform.position, -Speed * Time.deltaTime); }
        }
        if (ATTACK == true)
        {
            if (TBS <= 0)
            {
                GameObject B = Instantiate(project, transform.position, Quaternion.identity);
                if (ProjectileImage != null)
                {
                    B.GetComponent<SpriteRenderer>().sprite = ProjectileImage;
                }
                B.layer = gameObject.layer == (int)Rpg.EnemyTeam.Player ? (int)Rpg.EnemyTeam.Player : (int)Rpg.EnemyTeam.Enemy;
                Rpg.SetupBullet(B , state , enemyHp , this.gameObject);
                var tween = B.transform.DOMove(target.transform.position, .5f);
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
        if (Lung == true)
        {
            Lun();
        }
        if (Coward == true)
        {
            Cowar();
        }
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
