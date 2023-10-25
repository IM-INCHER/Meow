using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float JumpPower;
    public float jumpCount;
    public bool isLongJump = false;
    public bool isJumping = false;
    int health = 3;

    Rigidbody2D rb;
    Animator anim;

    void Start()
    {
        
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpCount = 1;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, 0f);
        transform.Translate(movement * Time.deltaTime);

        if(Input.GetAxisRaw("Horizontal") < 0) //왼쪽을 보고 있을 경우 Idle_L 활성화
        {
            anim.SetBool("Idle_L", true);
            anim.SetBool("Idle_R", false);
            anim.SetBool("Move_L", true);
        }
        else
        {
            anim.SetBool("Move_L", false); 
        }

        if (Input.GetAxisRaw("Horizontal") > 0) //오른쪽을 보고 있을 경우 Idle_R 활성화
        { 
            anim.SetBool("Idle_R", true);
            anim.SetBool("Idle_L", false);
            anim.SetBool("Move_R", true);
        }
        else
        { 
            anim.SetBool("Move_R", false);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isJumping == false)
            {
                isJumping = true;
                //anim.SetBool("Jump_R", true); 
                GetComponent<Rigidbody2D>().AddForce(Vector3.up * 450f);
                isLongJump = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isLongJump = false;
            anim.SetBool("Jump_R", false);
        }

        if(isLongJump == true)
        {
            anim.SetBool("Jump_R", true);
        }
        else if(isLongJump == false)
        {
            anim.SetBool("Jump_R", false );
        }

        if ((Input.GetAxisRaw("Horizontal") < 0) && isJumping == true)
        {
            anim.SetBool("Jump_L", true);
        }
        else if (isJumping == false && isLongJump == false)
        {
            anim.SetBool("Jump_L", false);
        }
    }


    private void FixedUpdate()
    {
        if (isLongJump && rb.velocity.y > 0) //Long Jump일 경우
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
}
