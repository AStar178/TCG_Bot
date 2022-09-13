using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mystrial : MonoBehaviour
{
    public List<MoveSets> n;
    Rigidbody2D rb;
    float Timer;

    public GameObject Bullet;

    private IEnumerator Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        for (int i=0 ; i < n.Count ; i++)
        {

            Timer = n[i].Time;
            if (n[i].TP == true)
            {
                TP(n[i].Pos, n[i].Time);
            }
            if (n[i].Run == true)
            {
                Vector2 plus = gameObject.transform.position;
                Run(plus, n[i].Pos, n[i].Time);
            }
            if (n[i].Go == true)
            {
                Go(n[i].Pos, n[i].Time);
            }

            if (n[i].Attack == true)
            {
                Attack();
            }

            yield return new WaitForSeconds(Timer);
        }
    }

    public void Run(Vector2 Plus,Vector2 Pos, float Time)
    {
        rb.DOMove(Plus + Pos, Time);
    }

    public void Go(Vector2 Pos, float Time)
    {
        rb.DOMove(Pos, Time);
    }

    public void Attack()
    {
        GameObject B = Instantiate(Bullet, transform.position, Quaternion.identity);
        Vector3 targetPos = FindObjectOfType<PlayerMove>().transform.position;
        B.transform.DOMove(targetPos, .5f);
        Destroy(B, .5f);
    }

    public void TP(Vector2 TPp, float Timer)
    {
        transform.position = TPp;
        new WaitForSeconds(Timer);
    }
}

[System.Serializable]
public struct MoveSets
{
    public Vector2 Pos;
    public float Time;
    public bool Run;
    public bool Go;
    public bool Attack;
    public bool TP;
    public bool Stomp;
}