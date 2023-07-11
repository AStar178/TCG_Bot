using System;
using UnityEngine;

public class EnemyHp : MonoBehaviour, IDamageAble
{
    [SerializeField] public float CurrentHp;
    [SerializeField] public float MaxHp;
    private void Awake() {
        CurrentHp = MaxHp;
    }
    void IDamageAble.TakeDamage(DamageData Data)
    {
        CurrentHp -= Data.DamageAmount;
        if (CurrentHp < 0)
        {
            Killed();
        }
    }

    private void Killed()
    {
        throw new NotImplementedException();
    }
}