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
    public SpriteRenderer SpriteRenderer;

    // Update is called once per frame
    void Update()
    {

        if (target == null)
        {
            Destroy(gameObject);
            EnemyStatic.CreatCoustomTextPopup("Dissapeared", gameObject.transform.position,Color.gray);
            return;
        }
        
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);
        if (Vector2.Distance(target.transform.position, transform.position) <= .5f)
        {
            BozzHP.Currenthp = BozzHP.Currenthp + Healing + BozzHP.MaxHp * 0.1f;
            if (BozzHP.Currenthp > BozzHP.MaxHp)
            {
                BozzHP.Currenthp = BozzHP.MaxHp;
            }
            EnemyStatic.CreatCoustomTextPopup("+" + Healing, target.transform.position, Color.green);
            Destroy(gameObject);
        }
        SpriteUpdaye();
    }
    private void SpriteUpdaye()
    {
        if ( Vector2.Dot( ( target.transform.position - transform.position ).normalized , Vector2.left) == 0 )
            return;
            
        if (Vector2.Dot( ( target.transform.position - transform.position ).normalized , Vector2.left) < -0.1f)
        {
            SpriteRenderer.flipX = true;       
            return;
        } 
        SpriteRenderer.flipX = false;
    }
}
