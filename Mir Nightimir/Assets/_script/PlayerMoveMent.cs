using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMoveMent : MonoBehaviour
{
    [SerializeField] Rigidbody2D body3d;
    [SerializeField] public SpriteRenderer SpriteRenderer;
    public float moveSpeed;
    [SerializeField] private Transform BulletSpawnPos;
    [SerializeField] private Vector2 pos;
    float movementX;
    float movementY;

    void Update()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");
    
        body3d.velocity = new Vector2(movementX , movementY).normalized * moveSpeed;
        SpriteUpdaye();
        CheatOpean();
    }

    private void CheatOpean()
    {
        if ( Input.GetKeyDown( KeyCode.E ) )
        {
            print ("e");
            var Chest = Physics2D.OverlapCircle( transform.position , 1 );
            if ( Chest.TryGetComponent<Chests>( out Chests coins ) )
            {
                print ("E");
                coins.Purchist( Player.Singleton );
            }
                
        }
    
    }

    private void SpriteUpdaye()
    {
        if (new Vector2(movementX , movementY).normalized == Vector2.zero) { return; }

        if ( Vector2.Dot(new Vector2(movementX , movementY).normalized , Vector2.right) == 0 )
            return;
            
        if (Vector2.Dot(new Vector2(movementX , movementY).normalized , Vector2.right) < -0.1f)
        {
            SpriteRenderer.flipX = true;
            BulletSpawnPos.localPosition = ( new Vector2( pos.x * -1 , pos.y ) );            
            return;
        } 
        SpriteRenderer.flipX = false;
        BulletSpawnPos.localPosition = pos;
    }
}
