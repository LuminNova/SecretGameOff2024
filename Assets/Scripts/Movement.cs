using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed;
    public float jump;
    public bool isFacingLeft;
    private float move;
    private Rigidbody2D rb;
    Animator myAnimator;
    void Start()
    {
       rb = GetComponent<Rigidbody2D>(); 
       myAnimator = GetComponent<Animator>();
       isFacingLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        if (rb.linearVelocity.x != 0 && isFacingLeft) {
            
        } else if (rb.linearVelocity.x != 0 && !isFacingLeft) {

        } else {

        }

        if (Input.GetButtonDown("Jump")) {
            rb.AddForce(new Vector2(rb.linearVelocity.x, jump));
        }
    }
}
