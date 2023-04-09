using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Throw Effect", menuName = "Items/Throw Effect")]
public class ThrowEffect : ScriptableObject
{
    [SerializeField]
    private bool PushOnCollid;
    [SerializeField]
    private float PushForce;
    [SerializeField]
    private bool DestroyOnCollid;

    public bool pushOnCollid => PushOnCollid;
    public float pushForce => PushForce;
    public bool destroyOnCollid => DestroyOnCollid;
}
