using UnityEngine;
using System;

public class Movement : MonoBehaviour
{

    public float speed;
    private float initialSpeed;
    public float jump;
    private float move;
    private Rigidbody2D rb;
    public Animator myAnimator;
    private bool isGrounded;
    private bool isFacingRight;
    // public Collider2D 
    public GameObject wings;
    public bool canFly;


    void Start()
    {
        initialSpeed = speed;
        move = 0;
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        isGrounded = false;
        isFacingRight = true;
        canFly = false;
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");

        FlipSprite();

        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            speed *= 2;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && isGrounded)
        {
            speed = initialSpeed;
        }
        myAnimator.SetFloat("Speed", Mathf.Abs(move));


        if (Input.GetButtonDown("Jump") && (isGrounded || canFly))
        {
            rb.linearVelocityY = 0;
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
        if (collision.tag != "Checkpoint")
        {

            myAnimator.SetBool("isJumping", false);
            if (canFly == false)
            {
                isGrounded = true;
            }
        }

        if (collision.tag == "Wings")
        {
            canFly = true;
            wings.GetComponentInChildren<Renderer>().enabled = false;
            checkFlight();
        }
    }

    private void checkFlight()
    {
        if (canFly == true)
        {
            isGrounded = false;
        }
    }
}
