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
    public PlayerEffect PlayerEffect;
    public ThirdPersonCam PlayerThirdPersonController;
    public UIManager UIManager;
    public LayerMask Enemy;
    private void Awake() {
        
        Current = this;

    }
}
