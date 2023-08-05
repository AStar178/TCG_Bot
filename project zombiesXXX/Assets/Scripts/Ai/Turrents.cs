using UnityEngine;
using UnityEngine.VFX;

public class Turrents : MonoBehaviour
{
    public Transform Aim;
    public Transform Shootpos;
    [SerializeField] public VisualEffect[] Effect;
    public float CoolDown;
    public float AttackRange;

    public void PlayEffect()
    {
        for (int i = 0; i < Effect.Length; i++)
        {
            Effect[i].Play();
        }
    }

}