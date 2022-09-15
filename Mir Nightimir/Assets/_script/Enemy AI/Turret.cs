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
    public LayerMask TarLayer;
    private Collider2D Tar;

    private float TBS;
    public float STBS;
    private float FixSecond;
    private float FixSecondN;
    public float BulletSpeed = 1;
    bool ATTACK = false;

    public GameObject project;

    public void Start()
    {
        STBS = 1 / stat.AttackSpeed;
        Range = stat.AggroRange;
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
    }

    private void SetupBullet(GameObject b)
    {
        Damage damage = new Damage();

        damage.AdDamage = stat.AdDamage;
        damage.ApDamage = stat.ApDamage;
        damage.Ad_DefenceReduser = stat.Ad_DefenceReduser;
        damage.ApDamage = stat.Mp_DefenceReduser;
        var bullet = b.AddComponent<EnemyBullent>();
        bullet.damage = damage;
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
