using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Player : MonoBehaviour
{
    public static Player Current;
    public StarterAssetsInputs StarterAssetsInputs;
    public PlayerState PlayerState;
    public FindTarget findTarget;
    public ThirdPersonController PlayerThirdPersonController;

    private void Awake() {
        
        Current = this;

    }
}
