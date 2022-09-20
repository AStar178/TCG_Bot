using System;
using UnityEngine;

public static class Rpg
{
    public static Damage CreatDamage(float ad, float ap, float amoroReduse, float magicReduse , Player player)
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
}