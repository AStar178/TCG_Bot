using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMove : MonoBehaviour
{
    [Header("Input")]
    public KeyCode JumpButoon;
    public KeyCode Run;

    [Header("Movement")]
    public float walkSpeed;
    public bool HoldToRun;
    public float SprintSpeed;
    bool readyToSprint = true;
    private float moveSpeed;
    private bool Sprinting;
    public bool Penguin;

    public float groundDrag;

    public float jumpForce;
    public float JumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;

    [Header("Ground Check")]
    public float playerHight;
    public LayerMask whatIsGround;
    [SerializeField] bool onGround;

    [Header("Slope Handing")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState moveState;
    public enum MovementState
    {
        walking,
        sprinting,
        air
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        // ground check
        onGround = Physics.Raycast(transform.position, Vector3.down, playerHight * 0.5f + 0.1f, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();

        // Dicreese friction when hit the ground
        if (onGround)
        {
            // Check if Penguin is not on
            if (!Penguin)
                rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    // Input Manager
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // When jump
        if (Input.GetKey(JumpButoon) && readyToJump && onGround)
        {
            print("attemp to Jumping");
            // Set false so No more jump in the mid Air
            readyToJump = false;

            // Excute Jump
            Jump();
            
            // Reset The Jump after Creation Time
            Invoke(nameof(ResetJump), JumpCooldown);
        }

    }
    
    // Control Moving
    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // on Ground
        if (onGround)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in the air
        else if (!onGround)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        // turn gravity off when slope
        rb.useGravity = !OnSlope();
    }

    // Limit the Speed
    private void SpeedControl()
    {
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }
        else
        {
            // Check the speed
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // Limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitVel.x, rb.velocity.y, limitVel.z);
            }
        }

    }

    // Reset Spring so we can sprint again
    private void ResetSprint()
    {
        readyToSprint = true;
    }

    // Jump and Jump reset
    private void Jump()
    {
        exitingSlope = true;

        // Reset Y Velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }

    // Movement state handler
    private void StateHandler()
    {
        #region Sprinting Handler
        if (HoldToRun)
        {
            if (Input.GetKey(Run) && moveState == MovementState.walking)
            {
                Sprinting = true;
            }
            if (Input.GetKeyUp(Run) && moveState == MovementState.walking)
            {
                Sprinting = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(Run) && moveState == MovementState.sprinting)
            {
                Sprinting = !Sprinting;
            }

            if (Input.GetKeyDown(Run) && moveState == MovementState.walking)
            {
                Sprinting = !Sprinting;
            }
        }

        if (rb.velocity.magnitude < 4)
            Sprinting = false;

        #endregion

        // Mode - Sprinting
        if (readyToSprint && onGround && Sprinting)
        {
            moveState = MovementState.sprinting;
            moveSpeed = SprintSpeed;
        }

        // Mode - Walk
        else if (onGround)
        {
            moveState = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        // Mode - Air
        else if (!onGround)
        {
            moveState = MovementState.air;
        }

    }

    #region Slop
    // Hiting Slope
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHight * 0.5f + 0.1f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    // Getting the slope angle
    private Vector3 getSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
    #endregion
}
