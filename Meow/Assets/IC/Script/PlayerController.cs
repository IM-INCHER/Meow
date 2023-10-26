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
    public LayerMask WallMask;

    int health = 3;

    [SerializeField]
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

    //점프
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

        if (col.gameObject.tag == "Platform")
        {
            isJumping = false;
        }
    }

    //이동
    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector2 rayStart = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, 1.3f, groundMask);

        if (state == Cat_State.Solid)
        {
            Vector2 startPos = new Vector2(transform.position.x, transform.position.y - 0.3f);
            RaycastHit2D fornthit = Physics2D.Raycast(startPos, isRight ? Vector2.right : Vector2.left, 0.7f, WallMask);

            if (fornthit)
            {
                Debug.Log(fornthit.collider.name);

                if (fornthit.collider.name == "Wall") { }
                else transform.Translate(new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime);
            }
            else
                transform.Translate(new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime);

        }
        else if (state == Cat_State.Liquid)
        {
            Vector2 startPos = new Vector2(transform.position.x, transform.position.y - 0.3f);
            RaycastHit2D fornthit = Physics2D.Raycast(startPos, isRight ? Vector2.right : Vector2.left, 1.3f, groundMask);

            SlopeChk(fornthit);
            Debug.DrawRay(startPos, isRight ? Vector2.right : Vector2.left * 1.3f, Color.red);

            //if (fornthit)
            //    //Debug.Log(liquidhit.collider.name);

            if (!isSlope)
            {
                transform.Translate(new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime);
            }
        }

        SlopeChk(hit);
        if (isGround)
        {
            if(state == Cat_State.Solid)
            {
            }
            this.transform.position = new Vector3(transform.position.x, hit.point.y + 1.33f / 2f + 0.1f, transform.position.z);
            rb.gravityScale = 0f;

            if(state == Cat_State.Liquid && !isSlope)
                rb.velocity = Vector2.zero;

            transform.up = hit.normal;
        }
        else
        {
            rb.gravityScale = 5f;
        }
    }

    //좌우확인
    void Flip()
    {
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if(state == Cat_State.Solid)
                sr.flipX = true;

            isRight = false;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            if (state == Cat_State.Solid)
                sr.flipX = false;

            isRight = true;
        }
    }

    //경사로 체크
    void SlopeChk(RaycastHit2D hit)
    {
        perp = Vector2.Perpendicular(hit.normal).normalized;
        angle = Vector2.Angle(hit.normal, Vector2.up);

        Debug.Log(angle);

        if (angle != 0 && angle < 90) isSlope = true;
        else isSlope = false;
    }

    //땅인지 체크
    void GroundChk()
    {
        isGround = Physics2D.OverlapCircle(transform.position, 1, groundMask);
    }

    //모드변경
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
                //rb.isKinematic = false;

                state = Cat_State.Liquid;

                collider.size = new Vector2(collider.size.x, collider.size.y / 2);
                collider.offset = new Vector2(0, -0.34f);
            }
            else if(state == Cat_State.Liquid)
            {
                anim.SetTrigger("Harden");

                collider.isTrigger = true;

                //rb.isKinematic = true;

                state = Cat_State.Solid;

                collider.size = new Vector2(1.33f, 1.5f);
                collider.offset = new Vector2(0, 0);
            }
        }
    }
}
