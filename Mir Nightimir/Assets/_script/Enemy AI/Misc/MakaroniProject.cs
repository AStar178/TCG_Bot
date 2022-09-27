using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MakaroniProject : MonoBehaviour
{
    public GameObject target;
    public async void Starto()
    {
        gameObject.transform.DOScale(3, 3);
        await EnemyStatic.Wait(3);

        var tween = transform.DOMove(target.transform.position, .5f);
        EnemyStatic.KillTween(.5f, tween, gameObject);
        await EnemyStatic.Wait(.48f);
    }
}
