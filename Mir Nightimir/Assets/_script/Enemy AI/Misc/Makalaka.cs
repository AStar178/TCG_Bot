using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Makalaka : MonoBehaviour
{
    public GameObject target;
    public float Speed;
    public float Healing;
    public EnemyHp BozzHP;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);
        if (Vector2.Distance(target.transform.position, transform.position) <= .5f)
        {
            BozzHP.Currenthp = BozzHP.Currenthp + Healing;
            Destroy(gameObject);
        }
    }
}
