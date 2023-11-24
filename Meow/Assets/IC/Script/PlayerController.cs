using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction
{
    Center,
    Left,
    Right,
    Up,
    Down
}

public class PlayerController : MonoBehaviour
{
    public Transform frontRay;

    public float moveSpeed = 5f;
    public float JumpPower;
    public bool isLongJump = false;
    public bool isJumping = false;

    public LayerMask groundMask;

    [SerializeField]
    private bool isSlope;
    [SerializeField]
    private bool isGround;
    private bool isRight = true;
    private bool isMelting = false;

    private bool isCrushing = false;

    private Vector2 perp;
    private float angle;

    //private Cat_State state;

    [SerializeField]
    private Direction direction;
    
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private BoxCollider2D collider;

    //AudioSource audioSource;
    public AudioClip audioHardWalk;
    public AudioClip audioJump;
    public AudioClip audioMeltWalk;
    public AudioClip audioMelt;
    public AudioClip audioHard;

    void Start()
    {
        //GameManager.instance.catState = Cat_State.Solid;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        //audioSource = GetComponent<AudioSource>();

        isSlope = false;
        isRight = true;
    }

    void Awake()
    {
    }

    void Update()
    {
        if (GameManager.instance.catState != Cat_State.Fly)
        {
            GroundChk();
            Flip();
            Jump();
            Move();
            ChangeState();
        }
        else if(GameManager.instance.catState == Cat_State.Fly)
        {
            PipeMove();
        }
    }

    private void FixedUpdate()
    {
    }

    //����
    public void Jump()
    {
        if(GameManager.instance.catState == Cat_State.Solid)
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
                    SoundFXManager.instance.PlaySoundFXClip(audioJump, transform, 1f);
                    //Debug.Log("����~");
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

    //�̵�
    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 rayStart = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, 1.3f, groundMask);

        //������϶� �̵�
        if (GameManager.instance.catState == Cat_State.Solid)
        {
            Vector2 startPos = new Vector2(transform.position.x, transform.position.y - 0.3f);
            RaycastHit2D fornthit = Physics2D.Raycast(startPos, isRight ? Vector2.right : Vector2.left, 0.7f, groundMask);
            if (fornthit)
            {
                //Debug.Log("�տ� �����ִ�");
                angle = Vector2.Angle(fornthit.normal, Vector2.up);
                //Debug.Log(angle);

                if (angle >= 90 || angle <= 0)
                {
                    //Debug.Log("�����");
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

        } //������� �̵�
        else if (GameManager.instance.catState == Cat_State.Liquid)
        {
            Vector2 startPos = new Vector2(transform.position.x, transform.position.y - 0.3f);
            RaycastHit2D fornthit = Physics2D.Raycast(startPos, isRight ? Vector2.right : Vector2.left, 1f, groundMask);
            SlopeChk(fornthit);
            Debug.DrawRay(startPos, isRight ? Vector2.right : Vector2.left * 1.3f, Color.red);

            if (fornthit)
            {
                if (angle >= 90 || angle <= 0)
                {
                    //Debug.Log("�����");
                }
            }
            else
            {
                if (!isSlope && !isMelting)
                {
                    transform.Translate(new Vector3(horizontalInput, 0, 0) * moveSpeed / 2 * Time.deltaTime);
                }
            }
        }

        if (!isJumping && !isMelting)
        {
            if (horizontalInput != 0)
            {
                
                anim.SetBool("Move", true);
                //SoundFXManager.instance.PlaySoundFXClip(audioWalk, transform, 1f);
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
                this.transform.position = new Vector3(transform.position.x, hit.point.y + 1.33f / 2f + 0.03f, transform.position.z);
                rb.gravityScale = 0f;

                if (GameManager.instance.catState == Cat_State.Liquid && !isSlope)
                    rb.velocity = Vector2.zero;
            }

            if(!isJumping)
                transform.up = hit.normal;
        }

        if (!isGround)
        {
            if (isLongJump) //Long Jump �ڵ�
            {
                rb.gravityScale = 3.0f;
            }
            else
            {
                rb.gravityScale = 6.0f;
            }
        }
    }

    //�¿�Ȯ��
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

    //���� üũ
    void SlopeChk(RaycastHit2D hit)
    {
        perp = Vector2.Perpendicular(hit.normal).normalized;
        angle = Vector2.Angle(hit.normal, Vector2.up);

        //Debug.Log(perp.x);

        if (angle != 0 && angle < 90) isSlope = true;
        else isSlope = false;
    }

    //������ üũ
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

    //��庯��
    void ChangeState()
    {
        if(Input.GetKeyDown(KeyCode.Z) && !isJumping && isGround && GameManager.instance.catState != Cat_State.Fly && !isMelting)
        {
            isMelting = true;
            
            if (GameManager.instance.catState == Cat_State.Solid)
            {
                anim.SetTrigger("Melt");
                SoundFXManager.instance.PlaySoundFXClip(audioMelt, transform, 1f);
                collider.isTrigger = false;

                GameManager.instance.catState = Cat_State.Liquid;

                collider.size = new Vector2(collider.size.x, collider.size.y / 2);
                collider.offset = new Vector2(0, -0.34f);
            }
            else if(GameManager.instance.catState == Cat_State.Liquid)
            {
                anim.SetTrigger("Harden");
                SoundFXManager.instance.PlaySoundFXClip(audioHard, transform, 1f);
                collider.isTrigger = true;

                GameManager.instance.catState = Cat_State.Solid;

                collider.size = new Vector2(1.33f, 1.5f);
                collider.offset = new Vector2(0, 0);
            }
        }
    }

    public void PipeMove()
    {
        //Debug.Log("������ �۵���");

        Vector3 pos = this.transform.position;

        float dis = 0.8f;

        RaycastHit2D upHit = Physics2D.Raycast(new Vector3(pos.x, pos.y + dis, pos.z), Vector2.up, 0.1f, 1 << LayerMask.NameToLayer("Pipe"));
        RaycastHit2D downHit = Physics2D.Raycast(new Vector3(pos.x, pos.y - dis, pos.z), Vector2.down, 0.1f, 1 << LayerMask.NameToLayer("Pipe"));
        RaycastHit2D rightHit = Physics2D.Raycast(new Vector3(pos.x + dis, pos.y, pos.z), Vector2.right, 0.1f, 1 << LayerMask.NameToLayer("Pipe"));
        RaycastHit2D leftHit = Physics2D.Raycast(new Vector3(pos.x - dis, pos.y, pos.z), Vector2.left, 0.1f, 1 << LayerMask.NameToLayer("Pipe"));

        int wayCount = 0;

        if (direction != Direction.Center)
        {
            if (upHit)
            {
                wayCount++;
                Debug.Log("��");
            }
            if (downHit)
            {
                wayCount++;
                Debug.Log("�Ʒ�");
            }
            if (rightHit)
            {
                wayCount++;
                Debug.Log("������");
            }
            if (leftHit)
            {
                wayCount++;
                Debug.Log("����");
            }
        }
            

        Debug.Log(wayCount);

        if (direction == Direction.Down)
        {
            if (downHit)
            {
                transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
            }
            else
            {
                anim.SetTrigger("Crush");
                direction = Direction.Center;
                isCrushing = true;
            }
        }
        else if (direction == Direction.Up)
        {
            if (upHit)
            {
                transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
            }
            else
            {
                anim.SetTrigger("Crush");
                GameManager.instance.catState = Cat_State.Solid;
                isRight = true;
            }
        }
        else if (direction == Direction.Right)
        {
            if (rightHit)
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            }
            else
            {
                anim.SetTrigger("Crush");
                direction = Direction.Center;
                isCrushing = true;
            }
        }
        else if (direction == Direction.Left)
        {
            if (leftHit)
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            }
            else
            {
                anim.SetTrigger("Crush");
                direction = Direction.Center;
                isCrushing = true;
            }
        }
        else if (direction == Direction.Center && isCrushing == false)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && (rightHit))
            {
                direction = Direction.Right;
                anim.SetTrigger("RightFly");
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && (leftHit))
            {
                direction = Direction.Left;
                anim.SetTrigger("LeftFly");
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && (upHit))
            {
                direction = Direction.Up;
                anim.SetTrigger("UpFly");
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && (downHit))
            {
                direction = Direction.Down;
                anim.SetTrigger("DownFly");
            }

        }

        //if (wayCount >= 4)
        //{
        //    direction = Direction.Center;
        //    anim.SetTrigger("Idle");

        //    switch (direction)
        //    {
        //        case Direction.Up:
        //            this.transform.Translate(new Vector2(0, dis));
        //            break;
        //        case Direction.Down:
        //            this.transform.Translate(new Vector2(0, -dis));
        //            break;
        //        case Direction.Right:
        //            this.transform.Translate(new Vector2(dis, 0));
        //            break;
        //        case Direction.Left:
        //            this.transform.Translate(new Vector2(-dis, 0));
        //            break;
        //    }

               
        //}

        Debug.DrawRay(new Vector3(pos.x, pos.y + dis, pos.z), Vector2.up * 0.1f, Color.red);
        Debug.DrawRay(new Vector3(pos.x, pos.y - dis, pos.z), Vector2.down * 0.1f, Color.red);
        Debug.DrawRay(new Vector3(pos.x + dis, pos.y, pos.z), Vector2.right * 0.1f, Color.red);
        Debug.DrawRay(new Vector3(pos.x - dis, pos.y, pos.z), Vector2.left * 0.1f, Color.red);
    }

    public void Melting()
    {
        isMelting = false;
        //Debug.Log("����~!");
    }

    public void Center()
    {
        direction = Direction.Center;
        anim.SetTrigger("Idle");
        isCrushing = false;
        //Debug.Log("�����߾�");
    }

    public void PipeChk()
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.down, 1f, 1 << LayerMask.NameToLayer("Pipe"));

        if(hit)
        {
            //Debug.Log("������ ����");
            GameManager.instance.catState = Cat_State.Fly;
            collider.isTrigger = true;

            //������ �߰����� ����
            //this.transform.position = new Vector2(hit.collider.transform.position.x, pos.y);
            this.transform.Translate(0, -0.5f, 0);
            anim.SetTrigger("Fly");

            direction = Direction.Down;
        }
    }  
}
