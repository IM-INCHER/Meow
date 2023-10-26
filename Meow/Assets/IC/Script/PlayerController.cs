using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Cat_State
{
    Solid,
    Liquid,
    Gas
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
    private BoxCollider2D collider;

    void Start()
    {
        state = Cat_State.Solid;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();

        isSlope = false;
        isRight = true;
    }

    void Awake()
    {
        jumpCount = 1;
    }

    void Update()
    {
        GroundChk();
        Flip();
        Move();
        Jump();
        ChangeState();
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
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isLongJump = false;
        }

        if(isJumping == true || isLongJump == true)
        {
            anim.SetBool("Jump_R", true);
        }
        else if(isJumping == false || isLongJump == false)
        {
            anim.SetBool("Jump_R", false) ;
        }
    }

    private void FixedUpdate()
    {
        if (isLongJump && rb.velocity.y > 0) //Long Jump 코드
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
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime);

        Vector2 rayStart = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, 1, groundMask);

        SlopeChk(hit);
        if (isGround)
        {
            if(state == Cat_State.Solid)
            {
                this.transform.position = new Vector3(transform.position.x, hit.point.y + 1.33f / 2f + 0.1f, transform.position.z);
                rb.gravityScale = 0f;

                transform.up = hit.normal;
            }
        }
        else
        {
            rb.gravityScale = 5f;
        }

        //if (horizontalInput == 0)
        //    rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        //else
        //    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Flip()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)  //오른쪽 Idle, Move
        {
            isRight = true;

            anim.SetBool("Move_R", true);
            anim.SetBool("Move_L", false);
            anim.SetBool("Idle_L", false);
        }
        else
        {
            Debug.Log("오른쪽");
            anim.SetBool("Idle_R", true);
            anim.SetBool("Move_R", false);

        }

        //if (Input.GetAxisRaw("Horizontal") < 0) ////왼쪽 Idle, Move
        //{
        //    isRight = false;

        //    anim.SetBool("Move_L", true);
        //    anim.SetBool("Move_R", false);
        //    anim.SetBool("Idle_R", false);

        //}
        //else
        //{
        //    anim.SetBool("Idle_L", true);
        //    anim.SetBool("Move_L", false);
        //}
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

    void ChangeState()
    {
        if(Input.GetKeyDown(KeyCode.Z) && !isJumping && isGround)
        {
            if(state == Cat_State.Solid)
            {
                if (isRight)
                    anim.SetTrigger("Melt_R");
                else
                    anim.SetTrigger("Melt_L");

                collider.isTrigger = false;

                state = Cat_State.Liquid;
            }
            else if(state == Cat_State.Liquid)
            {
                anim.SetTrigger("Harden");

                collider.isTrigger = true;

                state = Cat_State.Solid;
            }
            
        }
    }
}
