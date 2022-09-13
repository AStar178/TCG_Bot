using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour, IHpValue
{
    public float MaxHp;
    public float Currenthp;

    private void Start() {
        Currenthp = MaxHp;
    }
    public void HpValueChange(Damage damage)
    {
        
    }
}