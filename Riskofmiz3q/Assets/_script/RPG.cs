using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class RPG
{
    public static void CreateDamageColision(Vector3 CreatePos, float Size, Damage damage)
    {
        List<Collider> a = Physics.OverlapSphere(CreatePos, Size, LayerMask.GetMask("Enemy")).ToList();

        // add damage after adding HP script

        /* List<EnemyHP> e = new List<EnemyHP>();
        foreach (Collider2D collision2D in a)
        {
            if (collision2D.TryGetComponent<EnemyHP>(out var es) == true)
                e.Add(es);
        }
        foreach (EnemyHP enemy in e)
        {
            enemy.TakeDamage(damage);
        }
        */
    }

    public static void CreateBullet(Transform transform, GameObject BulletEffect, GameObject onHit, Damage damage, float explosionSize, float force, float forceUpward = 0)
    {
        Vector3 forceToAdd = transform.forward * force + transform.up * forceUpward;
        GameObject bullet = RougeLiter.Create(6, RougeLiter.ObjectHolder.Blank, transform.position);
        RougeLiter.Create(6, BulletEffect, bullet.transform.position).transform.parent = bullet.transform;
        bullet.GetComponent<Rigidbody>().AddForce(forceToAdd, ForceMode.Impulse);
        bullet.GetComponent<Bullet>().Damage = damage;
        bullet.GetComponent<Bullet>().Size = explosionSize;
        bullet.GetComponent<Bullet>().onHit = onHit;
        bullet.GetComponent<Bullet>().target = LayerMask.GetMask("Enemy");
    }

    public static Vector3 TransformToVector3(Transform transform)
    {
        return new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    public static Damage CreateDamage(int damage, DamageType damageType) 
    {
        Damage dam = new Damage();
        dam.damage = damage;
        dam.DamageType = damageType;
        return dam;
    }

}

public class Damage
{
    public int damage;
    public DamageType DamageType;
}

public enum DamageType
{
    Physical,
    Fire,
    Bleed
}