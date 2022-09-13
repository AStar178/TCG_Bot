using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMoveMent : MonoBehaviour
{
    [SerializeField] Rigidbody2D body3d;
    [SerializeField] float moveSpeed;
    float movementX;
    float movementY;


    void Update()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");

        body3d.velocity = new Vector2(movementX , movementY).normalized * moveSpeed;
    }
}
