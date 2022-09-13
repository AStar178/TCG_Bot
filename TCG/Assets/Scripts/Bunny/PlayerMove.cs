using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public bool CanMove = true;
    public bool CanJump = true;
    public bool Menu = false;

    public Rigidbody2D rb;
    public float GS;
    float horizontal;
    float vertical;
    bool onGround;
    public Transform groundCheck;
    public int speed;
    public int JumpPower;
    bool isJumping;

    bool isJumpingPres;
    public float defualtJumpP;
    float ZZzzzzzzzzzzzzzzzzzz;

    public float wallCheckRadios;
    public float checkRadios;
    public float DeathRadios;
    public LayerMask Ground;
    public LayerMask Wall;
    public LayerMask Enemy;

    bool isTouching;
    public Transform WallCheck;
    bool wallSliding;
    public float wallSlidingSpeed;

    public GameObject UI;

    private void Start()
    {
        GS = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove == true)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxis("Vertical");

            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumpingPres = true;
                ZZzzzzzzzzzzzzzzzzzz = defualtJumpP;
            }

            if (ZZzzzzzzzzzzzzzzzzzz > 0) { ZZzzzzzzzzzzzzzzzzzz -= Time.deltaTime; }
            else if (ZZzzzzzzzzzzzzzzzzzz <= 0) { isJumpingPres = false; }


            if (isJumpingPres && !isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpPower);
                isJumping = true;
                isJumpingPres = false;
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                rb.gravityScale = GS * 6;
            if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
                rb.gravityScale = GS;
        }

        if (CanJump == true)
        {
            if (Physics2D.OverlapCircle(WallCheck.position, wallCheckRadios, Wall) == true)
            { isTouching = true; }
            else isTouching = false;

            onGround = Physics2D.OverlapCircle(groundCheck.position, checkRadios, Ground);

            if (isTouching == true && onGround == false)
            {
                wallSliding = true;
            }
            else wallSliding = false;

            if (wallSliding)
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));

            if (onGround == true)
            {
                isJumping = false;
            }
            else if (onGround == false && wallSliding == false) { isJumping = true; }

            if (isTouching == true)
                isJumping = false;
        }

        if (Menu)
        {
            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            if (Input.GetKeyDown(KeyCode.Q))
                Application.Quit();
        }
    }

    private void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(WallCheck.position, DeathRadios, Enemy) == true)
        {
            Dead();
        }
    }

    IEnumerator wait(float f)
    {
        yield return new WaitForSeconds(f);
    }

    public void Dead()
    {
        gameObject.GetComponent<AudioSource>().volume = 0;
        UI.SetActive(true);
        CanMove = false;
        CanJump = false;
        Menu = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadios);
    }
}
