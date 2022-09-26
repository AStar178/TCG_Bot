using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class SoulHunterMinions : MonoBehaviour
{
    public GameObject target;
    public GameObject player;
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
        }

        TimeToDeath -= Time.deltaTime;

        if (AttackCooldown <= 0)
        {
            if (target != null)
            {

                AttackCooldown = SetAttackCooldown;
                GameObject B = Instantiate(EnemyStatic.project, target.transform.position, Quaternion.identity);
                EnemyBullent bullet = B.GetComponent<EnemyBullent>();
                B.layer = gameObject.layer == (int)Rpg.EnemyTeam.Player ? (int)Rpg.EnemyTeam.Player : (int)Rpg.EnemyTeam.Enemy;
                bullet.damage = damage;
                bullet.damage.GameObjectRefernce = target.GetComponent<EnemyHp>();
                bullet.layer = Get_BulitLayer(gameObject);
                var tween = B.transform.DOMove(target.transform.position, .5f);
                EnemyStatic.KillTween(.5f, tween, B);
            }
        }
        else { AttackCooldown -= Time.deltaTime; }
    }

    private List<int> Get_BulitLayer(GameObject b)
    {
        List<int> list = new List<int>();
        if (b.layer == (int)Rpg.EnemyTeam.Player)
        {
            list.Add(7);
            return list;
        }

        list.Add(10);
        list.Add(6);
        return list;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent<EnemyHp>(out EnemyHp enemyHp);

        if (enemyHp != null)
        {
            target = enemyHp.gameObject;
        }
        else target = null;
    }

    async void Run()
    {
        Vector3 PlayPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        EnemyStatic.randomVector2(RunTo, player.transform.position, MinX, MinY, MaxX, MaxY);
        gameObject.transform.DOMove(PlayPos + RunTo, .5f);
        await EnemyStatic.Wait(.7f);
        Run();
    }
}
