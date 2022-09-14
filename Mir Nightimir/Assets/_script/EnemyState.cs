using UnityEngine;

public class EnemyState : MonoBehaviour {
    
    public State State;
    public EnemyHp EnemyHp;
    public TESTei tei;

    private void Start() {
        EnemyHp.MaxHp = State.MaxHpAmount;
        EnemyHp.Currenthp = State.MaxHpAmount;
        tei.Speed = State.MoveSpeed;
        tei.Range = State.AggroRange;
        EnemyHp.Amoro = State.Amoro;
        EnemyHp.MagicResest = State.MagicReset;
    }

}