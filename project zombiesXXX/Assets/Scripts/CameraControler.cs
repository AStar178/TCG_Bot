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
        cam = GetComponent<CinemachineVirtualCamera>();
        IntesityOG = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain;
        c = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        NoiseOG = c.m_NoiseProfile;
    }

    void Update()
    {
        thirdPersonController.CameraDistance += StarterAssetsInputs.Zoom * 0.1f;
        thirdPersonController.CameraDistance = Mathf.Clamp(thirdPersonController.CameraDistance , min , max);
        Shader.SetGlobalVector("_sundir" , sundir.forward);

        thirdPersonController.ShoulderOffset = Vector3.Lerp(thirdPersonController.ShoulderOffset, newValue, 5 * Time.deltaTime);

        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            c.m_AmplitudeGain = Mathf.Lerp(startingIntensity, IntesityOG, 1 - (shakeTimer / shakeTimerTotal));
        }
        else if (shakeTimer <= 0 && c.m_NoiseProfile != NoiseOG)
            c.m_NoiseProfile = NoiseOG;
    }

    public void CombatMode()
    {
        newValue = CombatOffset;
    }

    public void OutCombatMode()
    {
        newValue = Vector3.zero;
    }

    private CinemachineVirtualCamera cam;
    CinemachineBasicMultiChannelPerlin c;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    private float IntesityOG;

    public NoiseSettings ShakeNoise;
    private NoiseSettings NoiseOG;

    public void CameraShakers(float intensity, float time)
    {
        c.m_NoiseProfile = ShakeNoise;
        c.m_AmplitudeGain = intensity;

        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
    }
}
