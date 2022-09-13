using UnityEngine;

[CreateAssetMenu(fileName = "State" , menuName = "ScrpitableObject/State")]
public class State : ScriptableObject
{
    public float AdDamage;
    public float ApDamage;
    public float Ap_Defence;
    public float Mp_Defence;
    public float Ap_DefenceReduser;
    public float Mp_DefenceReduser;
    public float MaxHpAmount;
    public float MoveSpeed;
}
