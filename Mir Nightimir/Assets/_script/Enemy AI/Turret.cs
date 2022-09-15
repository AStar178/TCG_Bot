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
    public LayerMask TarLayer;
    private Collider2D Tar;
    public float Range;

    private float TBS;
    public float STBS;
    private float FixSecond;
    private float FixSecondN;
    bool ATTACK = false;

    public GameObject project;
    Tween tween;

    public void Start()
    {
        STBS = 1 / stat.AttackSpeed;
        Range = stat.AggroRange;
        // set bullet damage

        project = EnemyStatic.project;
    }

    public void Update()
    {
        Tar = Physics2D.OverlapCircle(transform.position, Range, TarLayer);

        if (Tar != null) { ATTACK = true; }
        else { ATTACK = false; }

        if (ATTACK == true)
        {
            if (TBS <= 0)
            {
                GameObject B = Instantiate(project, transform.position, Quaternion.identity);
                tween = B.transform.DOMove(Tar.transform.position, .5f);
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
