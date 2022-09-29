using System;
using UnityEngine;

public class EnemyState : MonoBehaviour {
    

    public State State;
    public EnemyHp EnemyHp;
    [HideInInspector]
    public TESTei tei;
    [HideInInspector]
    public Turret yurret;

    private void Awake() {
        gameObject.TryGetComponent<Turret>(out Turret nodle);
        gameObject.TryGetComponent<TESTei>(out TESTei Makaroni);


        EnemyHp.MaxHp = State.MaxHpAmount;
        EnemyHp.Currenthp = State.MaxHpAmount;
        EnemyHp.Amoro = State.Amoro;
        EnemyHp.MagicResest = State.MagicReset;

        if (nodle != null)
        {
            Turret b = gameObject.GetComponent<Turret>();
            b.Range = State.AggroRange;
            b.stat = State;
        }

        if (Makaroni != null)
        {
            TESTei b = gameObject.GetComponent<TESTei>();
            b.Speed = State.MoveSpeed;
            b.Range = State.AggroRange;
            b.state = State;
        }

    }

}