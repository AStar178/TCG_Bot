using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using System;

public class Spirit : MonoBehaviour
{
    [HideInInspector]
    public bool ok;
    public float Modifire = 15;
    private Rigidbody2D rb;
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public GoGo Jojo;
    float AttackCooldown;
    [HideInInspector]
    public Transform JojoTarget;
    [SerializeField] SpriteRenderer renderers;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ok == true)
        {

            var yes = Physics2D.OverlapCircle(transform.position, Jojo.StandAggroRange, Jojo.GetPlayer().PlayerTarget.EnemyLayer);
            if (yes != null)
            {
                if (yes.TryGetComponent<EnemyHp>(out EnemyHp hp))
                    target = yes.transform;
            }

            Run();

            if (AttackCooldown <= 0)
            {
                if (Jojo.attacking == true)
                {
                    if (JojoTarget != null)
                        MoveAttack(JojoTarget);
                    else Jojo.attacking = false;
                }
                else
                { MoveAttack(target); }
            }
            else { AttackCooldown -= Time.deltaTime; }
        }
    }


    void MoveAttack(Transform target)
    {
        if (target != null)
        {
            if (Vector2.Distance(gameObject.transform.position, target.position) < .5f)
            {
                AttackCooldown = Jojo.GetPlayer().PlayerWeaponManger.AttackSpeed / 100;
                var Bullet = Instantiate(EnemyStatic.magicBullet, target.position, Quaternion.identity);
                var cp = Bullet.GetComponent<PlayerSimpBullet>();
                Bullet.GetComponent<SpriteRenderer>().sprite = null;
                cp.magic = Jojo;
                cp.target = target;
                #region hide that from my eyes
                cp.damage = Jojo.CreatDamage(Jojo.GetWeaponManger().DamageAd * ((Modifire + Jojo.SkillUpAddAmount) / 10), Jojo.GetWeaponManger().DamageAp * ((Modifire + Jojo.SkillUpAddAmount) / 10), Jojo.GetWeaponManger().AmoroReduse, Jojo.GetWeaponManger().MagicReduse);
                #endregion
                cp.SetDamage = true;
            }
        }
    }

    void Run()
    {

        if (JojoTarget != null)
        {
            if (Vector2.Distance(gameObject.transform.position, target.position) >= .4f)
                rb.velocity = (JojoTarget.position - transform.position).normalized * Jojo.GetPlayer().PlayerMoveMent.moveSpeed;
        }
        else if (target != null && JojoTarget == null)
        {
            if (Vector2.Distance(gameObject.transform.position, target.position) >= .4f)
                rb.velocity = (target.position - transform.position).normalized * Jojo.GetPlayer().PlayerMoveMent.moveSpeed;
        }
        else if (target == null && JojoTarget == null)
        {
            if (Vector2.Distance(gameObject.transform.position, player.transform.position) >= 1f)
                rb.velocity = (player.transform.position - transform.position).normalized * Jojo.GetPlayer().PlayerMoveMent.moveSpeed;
            else if (Vector2.Distance(gameObject.transform.position, player.transform.position) <= .9f)
                rb.velocity = (player.transform.position - transform.position).normalized * 0;
        }
    }

    public async void Spawn()
    {
        await EnemyStatic.Wait(.1f);
        gameObject.transform.DOScale(new Vector3(1, 1, 1), .4f);
        ok = true;
    }

    public void Retreat()
    {
        Jojo.Spit = null;
        ok = false;
        gameObject.transform.DOScale(new Vector3(0,0,0), .5f);
        Destroy(gameObject, .6f);
    }
}