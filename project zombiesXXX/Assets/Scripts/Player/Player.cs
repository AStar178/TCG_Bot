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
    public ThirdPersonController PlayerThirdPersonController;

    private void Awake() {
        
        Current = this;

    }
}
