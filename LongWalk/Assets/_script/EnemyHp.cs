using System;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    [SerializeField] int MaxHp;
    [SerializeField] int CurrentHp;
    private void Start() {
        CurrentHp = MaxHp;
    }
    public void TakeDamage(int damage)
    {
        CurrentHp -= damage;

        if (CurrentHp <= 0)
            Died();
    }

    private void Died()
    {
        Destroy(gameObject);
    }
}