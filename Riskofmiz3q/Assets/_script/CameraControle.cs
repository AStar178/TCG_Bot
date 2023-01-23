using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControle : MonoBehaviour
{
    [SerializeField] Camera cameras;
    [SerializeField] Transform ora;
    [SerializeField] Playermovement movement; 
    [SerializeField] Transform player;
    [SerializeField] Transform playerobjetc;
    private void Start() => cameras = Camera.main;
    void Update()
    {

        Vector3 normalized = player.transform.position - new Vector3(transform.position.x , player.position.y , transform.position.z);
        ora.forward = normalized.normalized;
        
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");
        Vector3 dwadwa = ora.forward * vInput + ora.right * hInput;

        if (dwadwa != Vector3.zero)
            playerobjetc.transform.forward = Vector3.Lerp(playerobjetc.transform.forward , dwadwa.normalized , Time.deltaTime * 7);

        // ora.localRotation = Quaternion.Lerp( ora.localRotation , Quaternion.LookRotation(new Vector3(normalized.x , 0 ,normalized.z)) , 0.1f ); 
    }
}
