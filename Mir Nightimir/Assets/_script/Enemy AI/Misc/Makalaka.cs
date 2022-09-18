using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

public class Makalaka : MonoBehaviour
{
    public GameObject target;
    public float Speed;
    public float Healing;
    public EnemyHp BozzHP;

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            EnemyStatic.CreatCoustomTextPopup("Dissapeared", gameObject.transform.position, Color.gray);
            Destroy(gameObject);
            return;
        }
        if (Vector2.Distance(target.transform.position, transform.position) <= .5f)
        {
            BozzHP.Currenthp = BozzHP.Currenthp + Healing;
            EnemyStatic.CreatCoustomTextPopup("+" + Healing, target.transform.position, Color.green);
            Destroy(gameObject);
        }
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);
    }
}
