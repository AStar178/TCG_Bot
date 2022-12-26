using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField] Transform GroundCheak;
    [SerializeField] float radius;
    public float MovementSpeed =1;
    public float Gravity = 9.8f;
    public float JumpStinge;
    public LayerMask Grounds;
    private float velocity = 0;
 
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
 
    private void Update()
    {
        // player movement - forward, backward, left, right
        float horizontal = Input.GetAxis("Horizontal") * MovementSpeed;
        float vertical = Input.GetAxis("Vertical") * MovementSpeed;
        characterController.Move((transform.right * horizontal + transform.forward * vertical) * Time.deltaTime);

        bool Ground = Physics.Raycast(transform.position , Vector3.down , radius , Grounds);
        // Gravity
        if(Ground)
        {
            velocity = 0;
            if (Input.GetKeyDown(KeyCode.Space))
                velocity += JumpStinge * Time.deltaTime;        
        }
        else
        {
            velocity -= Gravity * Time.deltaTime;
        }
        characterController.Move(new Vector3(0, velocity, 0));
    }

}
