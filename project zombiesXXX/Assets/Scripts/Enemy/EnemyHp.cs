using System;
using UnityEngine;

public class EnemyHp : MonoBehaviour, IDamageAble
{
    [SerializeField] public float CurrentHp;
    [SerializeField] public float MaxHp;
    public Action<DamageData> TakeDamageEvent;
    public Action KilledEvent;
    private void Awake() {
        CurrentHp = MaxHp;
    }
    void IDamageAble.TakeDamage(DamageData Data)
    {
        TakeDamageEvent?.Invoke(Data);
        CurrentHp -= Data.DamageAmount;
        float a = -Data.DamageAmount;
        Minus.Instance.CreatCoustomTextPopup(a.ToString(), transform.position, Color.red);
        if (CurrentHp < 0)
        {
            Killed();
        }
    }

    private void Killed()
    {
        KilledEvent?.Invoke();
        Destroy(gameObject);
    }
}