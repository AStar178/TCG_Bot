using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LofyEffect : MonoBehaviour
{
    [SerializeField] Playermovement movement;
    [SerializeField] Material material;
    [SerializeField] float Sad;
    [SerializeField] Animator animationX;
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
    }

}
