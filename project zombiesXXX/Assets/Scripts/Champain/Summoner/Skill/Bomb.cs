using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Bomb : MonoBehaviour
{
    [SerializeField] private VisualEffect effect;
    [SerializeField] float ShakeIntense;
    [SerializeField] float ShakeTime;

    private void Awake()
    {
        effect.Stop();
    }

    private void StartExplotion()
    {
        effect.Play();
        Player.Current.CameraControler.CameraShakers(ShakeIntense, ShakeTime);
    }
}
