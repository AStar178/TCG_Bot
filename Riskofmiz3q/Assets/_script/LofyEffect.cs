using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class LofyEffect : MonoBehaviour
{
    [SerializeField] Playermovement movement;
    [SerializeField] MultiAimConstraint MultiAimConstraint;
    [SerializeField] Transform target;
    [SerializeField] Transform playerforward;
    [SerializeField] Material material;
    [SerializeField] float Sad;
    [SerializeField] Animator animationX;
    [SerializeField] float owo;
    float s;
    void Update() 
    {
        s = 0.3f;
        bool sa = false;
        if (movement.MovementDir.magnitude > 0)
        {
            s = Sad + Random.Range(-0.2f , 0.2f);
            sa = true;
        }
        animationX.SetBool("run" , sa);
        material.SetFloat("_Float_4" , Mathf.Lerp(material.GetFloat("_Float_4") , s , 0.03f));

        Vector3 look = (target.transform.position - playerforward.transform.position).normalized * -1;
        MultiAimConstraint.weight = Mathf.Lerp(MultiAimConstraint.weight , Vector3.Dot(playerforward.transform.forward , look) < owo ? 0 : 1 , 0.1f);
    }

}
