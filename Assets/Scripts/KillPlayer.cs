using UnityEngine;
using System.Collections;
using System;

public class KillPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject deathParticle;
    public Transform respawnPoint;
    public Transform checkPoint;
    SpriteRenderer playerSprite;
    Rigidbody2D playerRB;


    void Start()
    {
        playerSprite = player.GetComponent<SpriteRenderer>();
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Deadly")
        {
            StartCoroutine(resetPlayer(0.5f));
        }
    }

    IEnumerator resetPlayer(float duration)
    {
        playerSprite.enabled = false;
        spawnDeathParticle();
        playerRB.linearVelocity = new Vector2(0, 0);
        yield return new WaitForSeconds(duration);
        player.transform.position = respawnPoint.position;
        playerSprite.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            Debug.Log("enter checkpoint");
            updateRespawn();
        }
    }

    void updateRespawn()
    {
        respawnPoint.position = checkPoint.transform.position;
    }

    void spawnDeathParticle()
    {
        GameObject clone = (GameObject)Instantiate(deathParticle, player.transform.position, Quaternion.identity);
        Destroy(clone, 1.0f);
    }
}
