using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Player : MonoBehaviour
{
    public static Player Current;
    public PlayerState PlayerState;
    public ThirdPersonController PlayerThirdPersonController;

    private void Awake() {
        
        Current = this;

    }
}
