using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RangeED : TESTei
{
    float shot;
    float timeBShot; 

    private float TBS;
    public float STBS;
    private float FixSecond;
    private float FixSecondN;
    bool ATTACK = false;

    public GameObject project;
    Tween tween;
    public override void start()
    {
        base.start();
        project = EnemyStatic.project;
    }

    public override void update()
    {
        if (Vector2.Distance(gameObject.transform.position, target.transform.position) <= Range)
        { transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime); ATTACK = true; }
        else ATTACK = false;
        if (Vector2.Distance(gameObject.transform.position, target.transform.position) <= 1 + Range * 0.1f)
        { transform.position = Vector2.MoveTowards(transform.position, target.transform.position, -Speed * Time.deltaTime); }


        if (ATTACK == true)
        {
            if (TBS <= 0)
            {
                GameObject B = Instantiate(project, transform.position, Quaternion.identity);
                Vector3 targetPos = FindObjectOfType<PlayerMoveMent>().transform.position;
                if (tween != null)
                    tween.Kill();
                tween = B.transform.DOMove(targetPos, .5f);
                Destroy(B, .5f);
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
    }
}
