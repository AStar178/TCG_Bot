using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playershoot : MonoBehaviour
{
    [SerializeField] Camera cameras;
    [SerializeField] Transform ora;
    [SerializeField] Playermovement movement; 
    private void Start() => cameras = Camera.main;
    void Update()
    {
        if (movement.MovementDir.magnitude == 0)
            return;

        Vector3 normalized = (cameras.transform.position - ora.transform.position).normalized * -1;
        ora.localRotation = Quaternion.Lerp( ora.localRotation , Quaternion.LookRotation(new Vector3(normalized.x , 0 ,normalized.z)) , 0.1f ); 
    }
}
