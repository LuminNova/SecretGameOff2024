using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCWander : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float leftBoundary, rightBoundary;
    [SerializeField] private float minPauseTime, maxPauseTime;
    [SerializeField] private float minWalkTime, maxwalkTime;

    [SerializeField] private float facingDirection = -1;

    
    
    private float randomTime, timer;
    private bool isFlipping;
    private bool isWalking = true;

    private void Start() {
        randomTime = Random.Range(minWalkTime, maxwalkTime);

    }
    void Update() {
        timer += Time.deltaTime;
        if(timer >= randomTime)
            StateChange();

        if(!isFlipping && (transform.position.x > rightBoundary || transform.position.x < leftBoundary))
            StartCoroutine(Flip());

        if(isWalking)
            rb.linearVelocity = Vector2.right * facingDirection * speed;
    }

    IEnumerator Flip() {
        isFlipping = true;
        transform.Rotate(0,180,0);
        facingDirection *= -1;
        yield return new WaitForSeconds(0.5f);
        isFlipping = false;
    }

    void StateChange() {
        isWalking = !isWalking;
        randomTime = isWalking ? Random.Range(minWalkTime, maxwalkTime) : Random.Range(minPauseTime, maxPauseTime);
        timer = 0;
    }
}
