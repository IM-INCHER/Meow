using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float movespeed;
    public float slopeForce; // 경사로를 따라 이동할 때의 힘
    //private Transform groundCheck;
    //private LayerMask groundLayer;


    private Rigidbody2D rb;
    private bool isOnSlope = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(horizontalInput, 0, 0) * movespeed * Time.deltaTime);

        //// 경사로를 따라 이동 처리
        //CheckGround();

        //if (isOnSlope)
        //{
        //    rb.AddForce(Vector2.down * slopeForce);
        //}
    }

    private void CheckGround()
    {
        //isOnSlope = false;
        //Collider2D groundCollider = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        //if (groundCollider != null)
        //{
        //    float slopeAngle = Vector2.Angle(Vector2.up, groundCollider.transform.up);

        //    if (slopeAngle > 0 && slopeAngle < 45)
        //    {
        //        isOnSlope = true;
        //    }
        //}
    }
}
