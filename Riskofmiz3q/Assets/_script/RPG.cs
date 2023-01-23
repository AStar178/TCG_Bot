using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class RPG
{
    public static void CreateDamageColision(Vector3 CreatePos, Vector3 Size, Damage damage)
    {
        List<Collider2D> a = Physics2D.OverlapBoxAll(CreatePos, Size, LayerMask.GetMask("Enemy")).ToList();

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

    public static Vector3 TransformToVector3(Transform transform)
    {
        return new Vector3(transform.position.x, transform.position.y, transform.position.z);
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