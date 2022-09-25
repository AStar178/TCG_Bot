using System;
using System.Collections.Generic;
using UnityEngine;

public static class Rpg
{
    public static Damage CreatDamage( float ad, float ap, float amoroReduse, float magicReduse , Player player , IHpValue hp , Transform pos )
    {
        Damage damage = new Damage();

        damage.AdDamage = ad;
        damage.ApDamage = ap;
        damage.Ad_DefenceReduser = amoroReduse;
        damage.Mp_DefenceReduser = magicReduse;
        damage.PlayerRefernce = player;
        damage.GameObjectRefernce = player.PlayerHp;
        damage.type = player.DamageModifayer( hp , pos , damage );

        return damage;
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