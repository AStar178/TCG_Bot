using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class MakaroniAI : TESTei
{
    private bool afk;
    public GameObject Slugs;
    public EnemyHp Hp;

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

    public override void start()
    {
        base.start();
        time = Cooldown;
    }

    public override async void update()
    {
        if (afk == false)
        {
            if (NoChase == false)
            {
                if (Vector2.Distance(gameObject.transform.position, target.transform.position) <= Range && Vector2.Distance(gameObject.transform.position, target.transform.position) > .35f)
                { transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime); }
            }
            if (Lung == true)
            {
                Lun();
            }
            if (time >= 0)
            {
                time = time - Time.deltaTime;
            }
        }

        if (time <= 0)
        {
            if (LowSummonHealth >= Hp.Currenthp)
            {
                afk = true;
                time = Cooldown;
                // Spawn Healing Orbs or Healing Minions
                summon = Random.Range(MinSummon, MaxSummon);
                for (int i = 0; i < summon; i++)
                {
                    randomVector2(transform.position, MinX, MaxX, MinY, MaxY);
                    GameObject b = Instantiate(Slugs, rand, Quaternion.identity);
                    b.GetComponent<Makalaka>().target = gameObject; b.GetComponent<Makalaka>().Speed = MinionSpeed;
                    b.GetComponent<Makalaka>().Healing = Healing; b.GetComponent<Makalaka>().BozzHP = Hp;
                }
                await Wait(afkD);
                afk = false;
            }
        }

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
