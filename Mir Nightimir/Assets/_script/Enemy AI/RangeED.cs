using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class RangeED : TESTei
{
    float shot;
    float timeBShot; 
    [SerializeField] int PlayerLayer = 7;
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
        if (NoChase == false)
        {
            if (Vector2.Distance(gameObject.transform.position, target.transform.position) <= Range)
            { transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime); ATTACK = true; }
            else ATTACK = false;
            if (Vector2.Distance(gameObject.transform.position, target.transform.position) <= 1 + Range * 0.1f)
            { transform.position = Vector2.MoveTowards(transform.position, target.transform.position, -Speed * Time.deltaTime); }
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
        if (ATTACK == true)
        {
            if (TBS <= 0)
            {
                GameObject B = Instantiate(project, transform.position, Quaternion.identity);
                B.layer = PlayerLayer;
                Vector3 targetPos = FindObjectOfType<PlayerMoveMent>().transform.position;
                SetupBullet(B);
                var tween = B.transform.DOMove(targetPos, .5f);
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
    }
    private void SetupBullet(GameObject b)
    {
        Damage damage = new Damage();
        damage.AdDamage = state.AdDamage;
        damage.ApDamage = state.ApDamage;
        damage.Ad_DefenceReduser = state.Ad_DefenceReduser;
        damage.ApDamage = state.Mp_DefenceReduser;
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
}
