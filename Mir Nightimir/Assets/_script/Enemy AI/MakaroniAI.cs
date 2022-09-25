using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using DG.Tweening;

public class MakaroniAI : TESTei
{
    private bool afk;
    public GameObject Slugs;
    public GameObject Ghos;
    public EnemyHp Hp;
    public GameObject MagicSpawn;
    public GameObject MagicHit;
    public GameObject SLimeSpawn;
    public GameObject GhosSpawn;

    #region Summon Fighter
    public float CooldownF;
    private float timeF;
    public float afkF;
    #endregion

    #region Summon Healing
    public float Cooldown;
    private float time;
    private Vector2 rand;

    public float afkD;
    public float LowSummonHealth;
    public float Healing;

    public float MinionSpeed;
    public float MinSummon;
    public float MaxSummon;
    private float summon;

    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;
    #endregion

    #region Shot
    private float TBS;
    public float ShotTimer;
    private float FixSecond;
    private float FixSecondN;
    private bool ATTACK = false;
    public Color bulletColor;
    public Sprite BulletSprite;
    private Vector3 LastPos;
    [SerializeField] EnemyHp enemyHp;
    private GameObject project;
    #endregion
    float speedy;
    Color oldColor;
    public override void start()
    {
        base.start();
        project = EnemyStatic.project;
        speedy = Speed;
        oldColor = SpriteRenderer.material.GetColor( "_Color" );
    }

    public override async void update()
    {
        if (SpriteRenderer != null)
            SpriteFreeFire();

        if (afk == true)
            transform.position = LastPos;
        if (afk == false)
        {
            LastPos = transform.position;
            if (NoChase == false)
            {
                if (Vector2.Distance(gameObject.transform.position, target.transform.position) <= Range &&
                    Vector2.Distance(gameObject.transform.position, target.transform.position) > .35f)
                { transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime); }
                if (Vector2.Distance(gameObject.transform.position, target.transform.position) <= Range) { ATTACK = true; }
                else { ATTACK = false; }
            }
            if (Lung == true)
            {
                Lun();
            }
            if (ATTACK == true)
            {
                if (TBS <= 0)
                {
                    if (FixSecond >= FixSecondN)
                    {
                        TBS = ShotTimer;
                        FixSecond = 0;
                    }
                    else
                    {
                        FixSecond += Time.deltaTime;
                    }
                    GameObject B = Instantiate(project, transform.position, Quaternion.identity);
                    GameObject E = Instantiate(MagicSpawn, B.transform.position, Quaternion.identity);
                    Destroy(E, 4);
                    B.layer = gameObject.layer;
                    B.GetComponent<SpriteRenderer>().color = bulletColor;
                    B.GetComponent<BoxCollider2D>().enabled = false;
                    B.AddComponent<CircleCollider2D>().isTrigger = true;
                    B.GetComponent<CircleCollider2D>().radius = 0.2f;
                    B.GetComponent<SpriteRenderer>().sprite = BulletSprite;
                    B.gameObject.transform.DOScale(3, 3);
                    await Wait(3);
                    Rpg.SetupBullet( B , state , enemyHp , this.gameObject );
            
                    var tween = B.transform.DOMove(target.transform.position , .5f);
                    EnemyStatic.KillTween(.5f, tween, B);
                    await Wait(.48f);
                }
                else
                {
                    TBS -= Time.deltaTime;
                }
            }
            if (time >= 0)
            {
                time = time - Time.deltaTime;
            }
            if (timeF >= 0)
            {
                timeF = timeF - Time.deltaTime;
            }
        }

        if (time <= 0)
        {
            if (LowSummonHealth >= Hp.Currenthp)
            {
                Hp.Currenthp += Hp.MaxHp * 0.1f;
                afk = true;
                Hp.BlockChanse = 100;
                SpriteRenderer.material.SetColor( "_Color" , Color.yellow * 10 );
                Speed = 0;
                time = Cooldown;
                // Spawn Healing Orbs or Healing Minions
                summon = Random.Range(MinSummon, MaxSummon);
                for (int i = 0; i < summon; i++)
                {
                    randomVector2(transform.position, MinX, MaxX, MinY, MaxY);
                    GameObject b = Instantiate(Slugs, rand, Quaternion.identity);
                    GameObject c = Instantiate(GhosSpawn, b.transform.position, Quaternion.identity);
                    b.GetComponent<Makalaka>().target = gameObject; b.GetComponent<Makalaka>().Speed = MinionSpeed;
                    b.GetComponent<Makalaka>().Healing = Healing; b.GetComponent<Makalaka>().BozzHP = Hp;
                    b.layer = gameObject.layer;
                    Destroy(c, 6);
                }
                await Wait(afkD);
                SpriteRenderer.material.SetColor( "_Color" , oldColor * 10 );
                Speed = speedy;
                Hp.BlockChanse = 0;
                afk = false;
            }
        }

        if (timeF <= 0 && Vector2.Distance(gameObject.transform.position, target.transform.position) <= Range)
        {
            afk = true;
            Hp.BlockChanse = 100;
            timeF = CooldownF;
            // Spawn Healing Orbs or Healing Minions
            summon = 3;
            for (int i = 0; i < summon; i++)
            {
                randomVector2(transform.position, MinX, MaxX, MinY, MaxY);
                GameObject b = Instantiate( Ghos, rand, Quaternion.identity);
                //GameObject c = Instantiate(Ghos, b.transform.position, Quaternion.identity);
                var effect = Instantiate( SLimeSpawn , b.transform.position , Quaternion.identity );
                b.GetComponent<TESTei>().ChangeTargetSelecting( targetLayerMask , targetLayer , gameObject.layer == (int)Rpg.EnemyTeam.Player ? Rpg.EnemyTeam.Player : Rpg.EnemyTeam.Enemy);
                b.GetComponent<TESTei>().target = target;
                Destroy( effect , 6 );
                //Destroy(c, 6);
            }
            await Wait(afkF);
            Hp.BlockChanse = 0;
            afk = false;
        }
    }

    private void SpriteFreeFire()
    {
        if (target == null) { return; }
        if (Vector2.Dot( ( target.transform.position - transform.position ).normalized , Vector2.left ) < -0.1f)
        {
            SpriteRenderer.flipX = true;            
            return;
        } 
        SpriteRenderer.flipX = false;
    }

    private async Task Wait(float Timez)
    {
        while (Timez > 0)
        {
            Timez -= Time.deltaTime;
            await Task.Yield();
        }
    }


    private void randomVector2(Vector2 v2, float minX, float maxX, float minY, float maxY)
    {
        rand = v2 + new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }
}
