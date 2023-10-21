using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public LayerMask groundMask;

    public float movespeed;

    private Rigidbody2D rb;
    private bool isSlope;
    private bool isGround;
    private bool isRight;

    public float shortJumpForce = 2.0f;  // ÂªÀº Á¡ÇÁ Èû
    public float longJumpForce = 3.0f;  // ·Õ Á¡ÇÁ Èû
    public float jumpPressTime = 0.2f;  // Á¡ÇÁ Å°¸¦ ±æ°Ô ´©¸£´Â ½Ã°£
    public float jumpPressStartTime;
     

    private Vector2 perp;
    private float angle;

    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Flip();
        CheckGround();
        Jump();
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(horizontalInput, 0, 0) * movespeed * Time.deltaTime);

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
    }

    public void Jump()
    {
        if (isGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                jumpPressStartTime = Time.time;
            }
            if (Input.GetButton("Jump"))
            {
                float jumpTime = Time.time - jumpPressStartTime;
                float jumpForce = jumpTime < jumpPressTime ? longJumpForce : shortJumpForce;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    //Áö¸éÃ¼Å©
    private void CheckGround()
    {
        isGround = Physics2D.OverlapCircle(transform.position, 1, groundMask);
    }

    //°æ»ç·ÎÃ¼Å©
    void SlopeChk(RaycastHit2D hit)
    {
        perp = Vector2.Perpendicular(hit.normal).normalized;
        angle = Vector2.Angle(hit.normal, Vector2.up);

        if (angle != 0) isSlope = true;
        else isSlope = false;
    }

    //ÁÂ¿ì ¹ÝÀü
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
}
