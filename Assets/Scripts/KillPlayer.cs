using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour
{
    public GameObject player;
    public Transform respawnPoint;
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
        playerRB.linearVelocity = new Vector2(0, 0);
        yield return new WaitForSeconds(duration);
        player.transform.position = respawnPoint.position;
        playerSprite.enabled = true;
    }
}
