using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMoveMent : MonoBehaviour
{
    [SerializeField] Rigidbody2D body3d;
    [SerializeField] public SpriteRenderer SpriteRenderer;
    [SerializeField] Player player;
    public float moveSpeed;
    [SerializeField] private Transform BulletSpawnPos;
    [SerializeField] private Vector2 pos;
    float movementX;
    float movementY;

    public LayerMask ChestLayer;

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
            var Chest = Physics2D.OverlapCircle( transform.position , 1 , ChestLayer );
            if ( Chest.TryGetComponent<Chests>( out Chests coins ) )
            {
                coins.Purchist( player );
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
