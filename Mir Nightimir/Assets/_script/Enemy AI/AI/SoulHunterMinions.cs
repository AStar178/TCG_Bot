using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using System;

public class SoulHunterMinions : MonoBehaviour
{
    public Transform target;
    public GameObject player;
    public Sprite ProjectileImage;
    [HideInInspector]
    public AbilityWeapons Master;
    [HideInInspector]
    public float AttackCooldown;
    public float SetAttackCooldown;
    public Damage damage;
    public float TimeToDeath;
    Vector3 RunTo;
    public float MinX;
    public float MinY;
    public float MaxX;
    public float MaxY;
    [SerializeField] SpriteRenderer renderers;

    // Start is called before the first frame update
    void Start()
    {
        AttackCooldown = SetAttackCooldown;
        Run();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeToDeath <= 0)
        {
            Destroy(gameObject);
            Master.GetComponent<SoulHunter>().SoulsDominions.Remove(this);
        }

        TimeToDeath -= Time.deltaTime;

        var yes = Physics2D.OverlapCircle(transform.position, 4, Master.GetPlayer().PlayerTarget.EnemyLayer);
        if (yes != null)
        {
            if (yes.TryGetComponent<EnemyHp>(out EnemyHp hp))
                target = yes.transform;
        }

        if (AttackCooldown <= 0)
        {
            if (target != null)
            {
                AttackCooldown = SetAttackCooldown;
                var Bullet = Instantiate(EnemyStatic.magicBullet, target.position, Quaternion.identity);
                var cp = Bullet.GetComponent<PlayerSimpBullet>();
                if (ProjectileImage != null)
                {
                    Bullet.GetComponent<SpriteRenderer>().sprite = ProjectileImage;
                }
                cp.magic = Master;
                cp.target = target;
            }
        }
        else { AttackCooldown -= Time.deltaTime; }
    }

    async void Run()
    {
        if (TimeToDeath > 1.7)
        {
            Vector3 PlayPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            EnemyStatic.randomVector2(RunTo, player.transform.position, MinX, MinY, MaxX, MaxY);
            gameObject.transform.DOMove(PlayPos + RunToCheak(RunTo), .5f);
        }
        await EnemyStatic.Wait(.7f);
        Run();
    }

    private Vector3 RunToCheak(Vector3 runTo)
    {
        if (Vector2.Distance( player.transform.position , transform.position ) < 2)
        {
            var s = (player.transform.position - transform.position).normalized;
            var rand = Mathf.Atan2( s.y , s.x );
            var news = rand * 0.75f;
            var dir  = new Vector2( Mathf.Cos( news ) , Mathf.Sin( news ) );
            return dir; 
        }
        return runTo;
    }
}
