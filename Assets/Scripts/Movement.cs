using UnityEngine;
using System;

public class Movement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed;
    private float initialSpeed;
    public float jump;
    private float move;
    private Rigidbody2D rb;
    public Animator myAnimator;
    private bool isGrounded;
    private bool isFacingRight;
    // public Collider2D 
    void Start()
    {
        initialSpeed = speed;
        move = 0;
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        isGrounded = false;
        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");

        FlipSprite();

        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded) {
            speed *= 2;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && isGrounded) {
            speed = initialSpeed;
        }
        myAnimator.SetFloat("Speed", Mathf.Abs(move));


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(rb.linearVelocity.x, jump));
            isGrounded = false;
            myAnimator.SetBool("isJumping", true);
        }


    }

    private void FlipSprite()
    {
        if (isFacingRight && move < 0f || !isFacingRight && move > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);
        myAnimator.SetFloat("Speed", Math.Abs(rb.linearVelocity.x));
        myAnimator.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        myAnimator.SetBool("isJumping", false);
        isGrounded = true;
    }
}
