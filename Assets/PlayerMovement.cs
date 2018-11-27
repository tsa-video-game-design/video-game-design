using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    public float speed = 9f;
    public float jumpForce = 7f;
    private Rigidbody2D rigid;

    public Transform groundCheck;
    public float groundCheckRadius = 0.5f;
    public LayerMask groundLayers;

    public float fallSpeed = 2.5f;
    public float lowFallSpeed = 1.5f;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        if (groundCheck == null)
        {
            groundCheck = transform.Find("GroundCheck");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        transform.Translate(input * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (rigid.velocity.y < 0)
        {
            rigid.velocity += Vector2.up * Physics2D.gravity.y * fallSpeed * Time.deltaTime;

        }
        else if (rigid.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rigid.velocity += Vector2.up * Physics2D.gravity.y * lowFallSpeed * Time.deltaTime;
        }

    }
    void Jump()
    {
        bool onGround = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayers);
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject != gameObject)
            {
                onGround = true;
            }
        }

        if (onGround)
        {
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

    }
}