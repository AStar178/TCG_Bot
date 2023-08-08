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
    public Transform LookRoot { get; private set; }
    [SerializeField] Transform sundir;
    [SerializeField] float min , max;
    [SerializeField] Vector3 CombatOffset;
    private Vector3 newValue;
    private void Awake() {
        thirdPersonController = cinemachineVirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        LookRoot = thirdPersonController.LookAtTarget;
    }

    void Update()
    {
        thirdPersonController.CameraDistance += StarterAssetsInputs.Zoom * 0.1f;
        thirdPersonController.CameraDistance = Mathf.Clamp(thirdPersonController.CameraDistance , min , max);
        Shader.SetGlobalVector("_sundir" , sundir.forward);

        thirdPersonController.ShoulderOffset = Vector3.Lerp(thirdPersonController.ShoulderOffset, newValue, 5 * Time.deltaTime);
    }

    public void CombatMode()
    {
        newValue = CombatOffset;
    }

    public void OutCombatMode()
    {
        newValue = Vector3.zero;
    }
}
