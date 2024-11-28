using System;
using UnityEngine;
using UnityEngine.InputSystem.Android.LowLevel;

public class Checkpoint : MonoBehaviour
{
    public KillPlayer killPlayer;
    void Start()
    {
        killPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<KillPlayer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("entered checkpoint");
            killPlayer.updateRespawn(transform.position);

        }
    }
}
