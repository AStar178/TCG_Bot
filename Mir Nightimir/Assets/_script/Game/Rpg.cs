using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Rpg
{
    public static float Tau => 6.28318530718f;
    public static Damage CreatDamage( float ad, float ap, float amoroReduse, float magicReduse , Player player , IHpValue hp , Transform pos )
    {
        Damage damage = new Damage();

        damage.AdDamage = ad;
        damage.ApDamage = ap;
        damage.Ad_DefenceReduser = amoroReduse;
        damage.Mp_DefenceReduser = magicReduse;
        damage.PlayerRefernce = player;
        damage.GameObjectRefernce = player.PlayerHp;

        return damage;
    }
    
    public static Vector2 TryToGetSpawnPos(Vector2 posin , Transform transform , int x , int y)
    {
        posin.x = Random.value > 0.5f ?
            -Random.Range(0 - transform.position.x , x/2 - transform.position.x):
            Random.Range(0 + transform.position.x , x/2 + transform.position.x);
            posin.y = Random.value > 0.5f ?
            -Random.Range(0 - transform.position.y , y/2 - transform.position.y):
            Random.Range(0 + transform.position.y , y/2 + transform.position.y);

        if (Physics2D.OverlapBox( posin , Vector2.one , 0 ) != null) { return TryToGetSpawnPos( new Vector2( transform.position.x , transform.position.y ) , transform , x , y ); }

        return posin;
    }

    public static List<Vector2> CreatMultipleDir(int amount)
    {
        List<Vector2> vector2s = new List<Vector2>();

        for (int i = 0; i < amount; i++)
        {
            var t = i/(float)amount;
            var raduis = Tau * t;
            Vector2 dir = new Vector2( Mathf.Cos( raduis ) , Mathf.Sin( raduis ) ).normalized;
            vector2s.Add(dir);   
        }

        return vector2s;
    }

    public static float HpMax( float currentHp , float MaxHP )
    {
        return currentHp / MaxHP;
    }
    public static Damage CreatDamage(float ad, float ap, float amoroReduse, float magicReduse)
    {
        Damage damage = new Damage();

        damage.AdDamage = ad;
        damage.ApDamage = ap;
        damage.Ad_DefenceReduser = amoroReduse;
        damage.Mp_DefenceReduser = magicReduse;
        return damage;
    }

    public static void SetupBullet(GameObject b , State stat , IHpValue enemyHp , GameObject self)
    {
        Damage damage = new Damage();

        damage.AdDamage = stat.AdDamage;
        damage.ApDamage = stat.ApDamage;
        damage.Ad_DefenceReduser = stat.Ad_DefenceReduser < 0 ? 1 : stat.Ad_DefenceReduser;
        damage.ApDamage = stat.Mp_DefenceReduser < 0 ? 1 : stat.Mp_DefenceReduser;
        var bullet = b.AddComponent<EnemyBullent>();
        bullet.damage = damage;
        bullet.damage.GameObjectRefernce = enemyHp;
        bullet.layer = Get_BulitLayer(self);

    }
    private static List<int> Get_BulitLayer(GameObject b)
    {
        List<int> list = new List<int>();
        if (b.layer == (int)Rpg.EnemyTeam.Player)
        {
            list.Add( 7 );
            return list;
        }

        list.Add ( 10 );
        list.Add ( 6 );
        return list;
    }

    public enum EnemyTeam
    {

        Player = 10,
        Enemy = 7

    }
    
    public enum allLayers
    {
        player = 6,
        enemylayer = 7,
        playerSimp = 10,
        Chest = 9,
        Grave = 11,
        PlayerBulit = 8
    }
}