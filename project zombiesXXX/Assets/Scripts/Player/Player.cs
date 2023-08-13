using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Player : MonoBehaviour
{
    public static Player Current;
    public PlayerInputSystem PlayerInputSystem;
    public PlayerState PlayerState;
    public PlayerTargetSystem PlayerTargetSystem;
    public PlayerEffectControler PlayerEffect;
    public ThirdPersonCam PlayerThirdPersonController;
    public UIManager UIManager;
    public CameraControler CameraControler;
    public GameObject Target;
    public LayerMask Enemy;
    public GameObject PlayerMap;
    public GameObject EnemyMap;

    private void Awake() {
        
        Current = this;

    }
}
