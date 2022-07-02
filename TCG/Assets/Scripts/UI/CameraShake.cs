using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraShake : MonoBehaviour
{
    
    private CinemachineBasicMultiChannelPerlin camera;
    private float Time;
    [SerializeField] private float TimeFull;


    private void Awake() {
        camera = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }


    public void CameraShake_Func(Vector2 shake)
    {
        camera.m_AmplitudeGain = shake.x;

        Time = shake.x;
    }

    
    void Update()
    {
        if (camera.m_AmplitudeGain != 0) 
        {
            Time = Mathf.Lerp(Time , 0 , TimeFull);

            camera.m_AmplitudeGain = Time; 
        }
            
    }
}
