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

    [SerializeField]
    private bool isSlope;
    private bool isGround;
    private bool isRight;

    private Vector2 perp;
    private float angle;

    private Cat_State state;

    Rigidbody2D rb;
    Animator anim;

    void Start()
    {
        state = Cat_State.Idle;
        rb = GetComponent<Rigidbody2D>();
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
        Flip();
        Move();

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

    public void Jump()
    {
        rb.velocity = Vector2.up * JumpPower;
    }

    private void FixedUpdate()
    {
        if (isLongJump && rb.velocity.y > 0)
        {
            rb.gravityScale = 2.0f;
        }
        else
        {
            rb.gravityScale = 5.0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Box")
        {
            Debug.Log("박스");
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
        //레이캐스팅
        Vector2 rayStart = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, 1, groundMask);
        RaycastHit2D frontHit = Physics2D.Raycast(frontRay.position, isRight ? Vector2.right : Vector2.left, 1, groundMask);
        if (hit || frontHit)
        {
            if (frontHit)
                SlopeChk(frontHit);
            else if (hit)
                SlopeChk(hit);
        }
        else
        {
            isSlope = false;
            isGround = false;
        }

        float horizontalInput = Input.GetAxis("Horizontal");

        if (isSlope && !isJumping)
            rb.velocity = perp * moveSpeed * horizontalInput * -1f;
        else if (!isSlope && !isJumping && isGround)
            rb.velocity = new Vector2(moveSpeed * horizontalInput, 0);
        else
            rb.velocity = new Vector2(moveSpeed * horizontalInput, rb.velocity.y);

        if (horizontalInput == 0)
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        else
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Flip()
    {
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            isRight = false;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
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
