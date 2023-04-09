using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletN : MonoBehaviour
{
    public float damage;
    public GameObject Gun;
    public GameObject Father;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == Father)
            return;
        if (collision.gameObject == Gun)
            return;

        collision.gameObject.TryGetComponent(out HpScript hp);
        if (hp != null)
        {
            hp.Damage(damage);
            print("Dealed " + damage + " damage");
        }

        Destroy(gameObject);
    }
}
