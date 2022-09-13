using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMoveMent : MonoBehaviour
{
    [SerializeField] Rigidbody2D body3d;
    [SerializeField] SpriteRenderer SpriteRenderer;
    [SerializeField] float moveSpeed;
    float movementX;
    float movementY;

    void Update()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");
    
        body3d.velocity = new Vector2(movementX , movementY).normalized * moveSpeed;
        SpriteUpdaye();

    }

    private void SpriteUpdaye()
    {
        if (new Vector2(movementX , movementY).normalized == Vector2.zero) { return; }
        
        if (Vector2.Dot(new Vector2(movementX , movementY).normalized , Vector2.right) < -0.1f)
        {
            SpriteRenderer.flipX = true;
            return;
        } 
        SpriteRenderer.flipX = false;
    }
}
