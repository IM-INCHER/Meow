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
    public bool isLongJump = false;
    public bool isJumping = false;

    public LayerMask groundMask;
    public LayerMask WallMask;

    int health = 3;

    [SerializeField]
    private bool isSlope;
    [SerializeField]
    private bool isGround;
    private bool isRight = true;
    private bool isMelting = false;

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
    }

    void Update()
    {
        GroundChk();
        Flip();
        Jump();
        Move();
        ChangeState();
    }

    private void FixedUpdate()
    {
    }

    //점프
    public void Jump()
    {
        if(state == Cat_State.Solid)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isJumping == false)
                {
                    rb.velocity = Vector2.zero;

                    rb.AddForce(Vector3.up * JumpPower, ForceMode2D.Impulse);
                    isGround = false;
                    isJumping = true;
                    isLongJump = true;
                    anim.SetTrigger("Jump");
                }
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                isLongJump = false;
            }

            if (isJumping && isGround)
            {
                isJumping = false;
                rb.velocity = Vector2.zero;
                anim.SetTrigger("Idle");
            }
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

                if (fornthit.collider.name == "Wall")
                {
                }
                else
                {
                    if(!isMelting)
                        transform.Translate(new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime);
                }
            }
            else
            {
                if (!isMelting)
                    transform.Translate(new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime);
            }

        }
        else if (state == Cat_State.Liquid)
        {
            Vector2 startPos = new Vector2(transform.position.x, transform.position.y - 0.3f);
            RaycastHit2D fornthit = Physics2D.Raycast(startPos, isRight ? Vector2.right : Vector2.left, 1.3f, groundMask);

            SlopeChk(fornthit);
            Debug.DrawRay(startPos, isRight ? Vector2.right : Vector2.left * 1.3f, Color.red);

            if (!isSlope && !isMelting)
            {
                transform.Translate(new Vector3(horizontalInput, 0, 0) * moveSpeed / 2 * Time.deltaTime);
            }
        }

        if (!isJumping)
        {
            if (horizontalInput != 0)
            {
                anim.SetBool("Move", true);
            }
            else
            {
                anim.SetBool("Move", false);
            }
        }

        if(!isJumping)
            SlopeChk(hit);

        if (isGround)
        {
            if(isJumping == false)
            {
                this.transform.position = new Vector3(transform.position.x, hit.point.y + 1.33f / 2f, transform.position.z);
                rb.gravityScale = 0f;

                if (state == Cat_State.Liquid && !isSlope)
                    rb.velocity = Vector2.zero;
            }

            if(!isJumping)
                transform.up = hit.normal;
        }

        if (!isGround)
        {
            if (isLongJump) //Long Jump 코드
            {
                rb.gravityScale = 3.0f;
            }
            else
            {
                rb.gravityScale = 6.0f;
            }
        }
    }

    //좌우확인
    void Flip()
    {
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            isRight = false;
            anim.SetBool("isRight", false);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            isRight = true;
            anim.SetBool("isRight", true);
        }
    }

    //경사로 체크
    void SlopeChk(RaycastHit2D hit)
    {
        perp = Vector2.Perpendicular(hit.normal).normalized;
        angle = Vector2.Angle(hit.normal, Vector2.up);

        //Debug.Log(angle);

        if (angle != 0 && angle < 90) isSlope = true;
        else isSlope = false;
    }

    //땅인지 체크
    void GroundChk()
    {
        if(rb.velocity.y <= 0)
        {
            Vector2 rayStart = transform.position;
            RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, 1f, groundMask);

            if (hit)
                isGround = true;
            else
                isGround = false;
        }
            


        //isGround = Physics2D.OverlapCircle(transform.position, 1, groundMask);
    }

    //모드변경
    void ChangeState()
    {
        if(Input.GetKeyDown(KeyCode.Z) && !isJumping && isGround)
        {
            isMelting = true;

            if(state == Cat_State.Solid)
            {
                anim.SetTrigger("Melt");

                collider.isTrigger = false;

                state = Cat_State.Liquid;

                collider.size = new Vector2(collider.size.x, collider.size.y / 2);
                collider.offset = new Vector2(0, -0.34f);
            }
            else if(state == Cat_State.Liquid)
            {
                anim.SetTrigger("Harden");

                collider.isTrigger = true;

                state = Cat_State.Solid;

                collider.size = new Vector2(1.33f, 1.5f);
                collider.offset = new Vector2(0, 0);
            }
        }
    }

    public void Melting()
    {
        isMelting = false;
    }
}
