using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField] Camera cameras;
    [SerializeField] Transform ora;
    [SerializeField] Playermovement movement; 
    [SerializeField] Transform PlayerCombat;
    [SerializeField] Transform playerobjetc;
    private void Start() => cameras = Camera.main;
    void Update()
    {
        ora.forward = (movement.gameObject.transform.position - new Vector3(transform.position.x , movement.gameObject.transform.position.y , transform.position.z)).normalized;
        PlayerCombat.forward = (cameras.gameObject.transform.position - ora.transform.position).normalized * -1;

        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");
        Vector3 dwadwa = ora.forward * vInput + ora.right * hInput;

        if (dwadwa != Vector3.zero)
            playerobjetc.transform.forward = Vector3.Lerp(playerobjetc.transform.forward , dwadwa.normalized , Time.deltaTime * 12);

        // ora.localRotation = Quaternion.Lerp( ora.localRotation , Quaternion.LookRotation(new Vector3(normalized.x , 0 ,normalized.z)) , 0.1f ); 
    }
}
