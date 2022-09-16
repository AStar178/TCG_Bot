using UnityEngine;

public class EnemyState : MonoBehaviour {
    
    public State State;
    public EnemyHp EnemyHp;
    public TESTei tei;

    private void Start() {
        tei = gameObject.GetComponent<TESTei>();

        EnemyHp.MaxHp = State.MaxHpAmount;
        EnemyHp.Currenthp = State.MaxHpAmount;
        tei.Speed = State.MoveSpeed;
        tei.Range = State.AggroRange;
        tei.state = State;
        EnemyHp.Amoro = State.Amoro;
        EnemyHp.MagicResest = State.MagicReset;
    }

}