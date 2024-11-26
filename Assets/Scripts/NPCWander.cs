using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCWander : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float leftBoundary, rightBoundary;

    [SerializeField] private float facingDirection = -1;
    
    private bool isFlipping;

    void Update()
    {
        if(!isFlipping && (transform.position.x > rightBoundary || transform.position.x < leftBoundary))
            StartCoroutine(Flip());

        rb.linearVelocity = Vector2.right * facingDirection * speed;
    }

    IEnumerator Flip()
    {
        isFlipping = true;
        transform.Rotate(0,180,0);
        facingDirection *= -1;
        yield return new WaitForSeconds(0.5f);
        isFlipping = false;
    }
}
