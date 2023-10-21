using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Cat_State
{ 
    Idle,
    Move_Falt,
    Move_Slope,
    Jump
}

public class PlayerController : MonoBehaviour
{
    public Transform frontRay;

    public float moveSpeed = 5f;
    public float JumpPower;
    public float jumpCount;
    public bool isLongJump = false;
    public bool isJumping = false;

    public LayerMask groundMask;

    int health = 3;

    private bool isSlope;
    private bool isGround;
    private bool isRight;

    private Vector2 perp;
    private float angle;

    private Cat_State state;
    
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    void Start()
    {
        state = Cat_State.Idle;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        isSlope = false;
        isRight = true;
    }

    void Awake()
    {
        jumpCount = 1;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        GroundChk();
        Flip();
        Move();
        Jump();
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isJumping == false)
            {
                //state = Cat_State.Jump;
                isJumping = true;
                GetComponent<Rigidbody2D>().AddForce(Vector3.up * 450f);
                isLongJump = true;
                anim.SetBool("Jumping", true);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isLongJump = false;
            anim.SetBool("Jumping", false);
        }
    }

    private void FixedUpdate()
    {
        //if (isLongJump && rb.velocity.y > 0)
        //{
        //    rb.gravityScale = 2.0f;
        //}
        //else
        //{
        //    rb.gravityScale = 5.0f;
        //}
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Box")
        {
            Debug.Log("¹Ú½º");
            if (health > 0)
            {
                health -= 1;
                HPManager.hp -= 1;

            }
            else if (HPManager.hp == 0)
            {
                Destroy(gameObject);
            }
        }

        if (col.gameObject.tag == "ground")
        {
            isJumping = false;
        }
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime);

        Vector2 rayStart = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, 1, groundMask);

        SlopeChk(hit);
        if (isGround)
        {
            this.transform.position = new Vector3(transform.position.x, hit.point.y + 1.33f / 2f + 0.1f, transform.position.z);
            rb.gravityScale = 0f;

            transform.up = hit.normal;
        }
        else
        {
            rb.gravityScale = 5f;
        }

        if (horizontalInput == 0)
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        else
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Flip()
    {
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            sr.flipX = true;
            isRight = false;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            sr.flipX = false;
            isRight = true;
        }
    }

    void SlopeChk(RaycastHit2D hit)
    {
        perp = Vector2.Perpendicular(hit.normal).normalized;
        angle = Vector2.Angle(hit.normal, Vector2.up);

        if (angle != 0) isSlope = true;
        else isSlope = false;
    }

    void GroundChk()
    {
        isGround = Physics2D.OverlapCircle(transform.position, 1, groundMask);
    }
}
