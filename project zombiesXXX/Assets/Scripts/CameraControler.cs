using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class CameraControler : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public PlayerInputSystem StarterAssetsInputs;
    public Cinemachine3rdPersonFollow thirdPersonController;
    [SerializeField] Transform sundir;
    [SerializeField] float min , max;
    private void Awake() {
        thirdPersonController = cinemachineVirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
    }

    void Update()
    {
        thirdPersonController.CameraDistance += StarterAssetsInputs.Zoom * 0.1f;
        thirdPersonController.CameraDistance = Mathf.Clamp(thirdPersonController.CameraDistance , min , max);
        Shader.SetGlobalVector("_sundir" , sundir.forward);
    }
}
