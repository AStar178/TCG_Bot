using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Singleton;
    [SerializeField] public State PlayerState;
    [SerializeField] public PlayerMoveMent PlayerMoveMent;
    [SerializeField] public PlayerHp PlayerHp;

    private void Awake() => Singleton = this;

    private void Start()
    {
        PlayerMoveMent.moveSpeed = PlayerState.MoveSpeed;
        PlayerHp.MaxHp = PlayerState.MaxHpAmount;
    }

}
