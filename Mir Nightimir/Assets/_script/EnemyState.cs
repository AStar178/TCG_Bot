using UnityEngine;

public class EnemyState : MonoBehaviour {
    
    public State State;
    public EnemyHp EnemyHp;
    [HideInInspector]
    public TESTei tei;
    [HideInInspector]
    public Turret yurret;

    private void Start() {
        EnemyHp = gameObject.GetComponent<EnemyHp>();
        gameObject.TryGetComponent<Turret>(out Turret nodle);
        gameObject.TryGetComponent<TESTei>(out TESTei Makaroni);

        if (nodle != null)
        {
            yurret.Range = State.AggroRange;
            yurret.stat = State;
        }

        if (Makaroni != null)
        {
            tei.Speed = State.MoveSpeed;
            tei.Range = State.AggroRange;
            tei.state = State;
        }

        EnemyHp.MaxHp = State.MaxHpAmount;
        EnemyHp.Currenthp = State.MaxHpAmount;
        EnemyHp.Amoro = State.Amoro;
        EnemyHp.MagicResest = State.MagicReset;
    }

}