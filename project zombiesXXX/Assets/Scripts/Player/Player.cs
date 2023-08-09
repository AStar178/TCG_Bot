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
    public LayerMask Enemy;
    public TMPro.TMP_Text text;
    private void Awake() {
        
        Current = this;

    }
    private void Update() {
        text.text = "FPS:" + 1f / Time.deltaTime;
    }
}
