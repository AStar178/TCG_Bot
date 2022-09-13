using System.Collections;
using System.Collections.Generic;
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

    public override void start()
    {
        base.start();
        project = EnemyStatic.project;
    }

    public override void update()
    {
        if (Vector2.Distance(gameObject.transform.position, target.transform.position) <= Range)
        { transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime); }
        if (Vector2.Distance(gameObject.transform.position, target.transform.position) <= 1 + Range * 0.1f)
        { transform.position = Vector2.MoveTowards(transform.position, target.transform.position, -Speed * Time.deltaTime); ATTACK = true; }
        else ATTACK = false;


        if (ATTACK == true)
        {
            if (TBS <= 0)
            {
                Instantiate(project, transform.position, Quaternion.identity);
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
