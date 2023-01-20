using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    
    public float MovementSpeed;

    [SerializeField] Transform oritansin;
    [SerializeField] Rigidbody rb;
    [SerializeField] float RayCastRang;
    [SerializeField] float GroundDrag;
    [SerializeField] LayerMask Ground;
    bool Grounded;

    float hInput;
    float vInput;

    public Vector3 MovementDir;

    public float JumpForce;
    public float JumpCoolDown;
    public float airMultiyplayer;
    bool readytojump;

    [SerializeField] KeyCode jumpKey;
    [SerializeField] Material sheeeeeeeeeeeh;
    void Start()
    {
        readytojump = true;
        rb.freezeRotation = true;
    }

    
    void Update()
    {
        Grounded = Physics.Raycast(transform.position , Vector3.down , RayCastRang , Ground);
        
        GetInpute();
        SpeedControle();

        if (Input.GetKey(jumpKey) && readytojump && Grounded)
        {
            readytojump = false;

            jump();

            RestJumpDelay();
        }
            

        if (Grounded)
            rb.drag = GroundDrag;
        else
            rb.drag = 0; 
    }

    private void FixedUpdate() {
        MovePlayer();
    }
    private void GetInpute()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");
    }
    private void MovePlayer()
    {
        MovementDir = oritansin.forward * vInput + oritansin.right * hInput;
        float S = Grounded == true ? 1 : airMultiyplayer;
        rb.AddForce(MovementDir.normalized * MovementSpeed * 10 * S , ForceMode.Force);
    }
    private void SpeedControle()
    {
        Vector3 flatvelocity = new Vector3(rb.velocity.x , 0 , rb.velocity.z);

        if (flatvelocity.magnitude > MovementSpeed)
        {
            rb.velocity = new Vector3( flatvelocity.normalized.x * MovementSpeed , rb.velocity.y , flatvelocity.normalized.z * MovementSpeed );
        }
    }
    private void jump()
    {
        rb.velocity = new Vector3(rb.velocity.x , 0 ,rb.velocity.z);

        rb.AddForce(transform.up * JumpForce , ForceMode.Impulse);
    }
    private async void RestJumpDelay()
    {
        float t = JumpCoolDown;
        while ( t > 0 )
        {
            t -= Time.deltaTime;
            await Task.Yield();
        }
        RestJumpImudley();
    }

    public void RestJumpImudley()
    {
        readytojump = true;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position , Vector3.down * RayCastRang);
    }
}
