using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpScript : MonoBehaviour
{
    public float Hp;

    public void Damage(float dmg)
    {
        Hp -= dmg;
        print("Toke " + dmg + " damage");
        if (Hp <= 0)
        {
            Hp = 0;
            Destroy(gameObject);
        }
    }
}
