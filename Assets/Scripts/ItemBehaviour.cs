using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public SecretManager chickenSecret;
    public Item item;
    public Transform player;
    public float range;

    public GameObject movingItem;

    [SerializeField]
    private float dropDistance;

    private bool fallen;

    private int originalDurability;
    void Start()
    {
        originalDurability = item.durability;
        chickenSecret = SecretManager.instance;
        fallen = false;
    }

    void Update()
    {
    }

    void OnMouseDown()
    {
        Debug.Log("Mouse Clicked");

        // Pickup item with tag PickupItem
        if ((player.position-gameObject.transform.position).magnitude < range && gameObject.CompareTag("PickupItem"))
        {
            if (InventoryManager.instance.AddItem(item))
            {
                // Destroy the item after pickup
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Inventory full");
            }
        }

        // Item falling out of tree
        if ((player.position-gameObject.transform.position).magnitude < range && gameObject.CompareTag("Shake"))
        {
            if(transform.childCount == 2 && fallen == false){
                StartCoroutine(ItemDropping(movingItem.transform.position, movingItem.transform.position + new Vector3(0,dropDistance,0), .5f));
                movingItem.transform.SetParent(null);
                StartCoroutine(ExecuteSequentialTasks());
                fallen = true;
            } else {
                itemInteraction();
            }
            
        }

        // Damage item and destroy when broken with (temporarily using) Respawn tag
        if ((player.position-gameObject.transform.position).magnitude < range && gameObject.CompareTag("BreakableItem")) 
        {
            itemInteraction();
        }
    }

    public void itemInteraction(){
        // Item being used
        Debug.Log("Damage item");
        Item useItem = InventoryManager.instance.GetSelectedItem(true); // use item
        if(useItem == null){
            StartCoroutine(ExecuteSequentialTasks());
            return;
        }

        Debug.Log("Item being used: " + useItem);
        if(useItem.isUsableItem){
            Debug.Log("game object is being damaged");

            // shake
            StartCoroutine(ExecuteSequentialTasks());
            
            item.durability = item.durability - useItem.damageNumber;
            Debug.Log("New Item durability = " + item.durability);
            
            if(item.durability <= 0){
                // Reset item durability
                item.durability = originalDurability;
                Destroy(gameObject);
                Debug.Log("bomb" + item.durability);
            }

            if (item.durability == 1) {
                InventoryManager.instance.AddItem(item);
                chickenSecret.SecretCount(1);
                item.durability = originalDurability;
                Destroy(gameObject);
                Debug.Log("rose" + item.durability);
            }

        } else {
            Debug.Log("Item is not useable");
        }
    }

    IEnumerator ItemDropping(Vector3 start, Vector3 end, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            movingItem.transform.position = Vector3.Lerp(start, end, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        movingItem.transform.position = end;
    }

    IEnumerator ExecuteSequentialTasks()
    {
        // Start Task A and wait for it to finish
        yield return StartCoroutine(LerpPosition(transform.position, transform.position + new Vector3(-.1f, 0, 0), .05f));

        // Start Task B and wait for it to finish
        yield return StartCoroutine(LerpPosition(transform.position, transform.position + new Vector3(.2f, 0, 0), .05f));

        // Start Task C and wait for it to finish
        yield return StartCoroutine(LerpPosition(transform.position, transform.position + new Vector3(-.1f, 0, 0), .05f));
    }

    IEnumerator LerpPosition(Vector3 start, Vector3 end, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            transform.position = Vector3.Lerp(start, end, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = end;
    }

    void OnApplicationQuit()
    {
        item.durability = originalDurability;
    }

    // When player (Player tag) touches an object
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player is colliding with the item
        if (other.CompareTag("Player") && gameObject.CompareTag("Secret")) // Ensure your player GameObject has the "Player" tag
        {
            // Perform any actions you want, like adding points, updating inventory, etc.
            Debug.Log("Player picked up the item!");

            if (InventoryManager.instance.AddItem(item))
            {
                chickenSecret.SecretCount(1);
                // Destroy the item after pickup
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Inventory full");
            }
        }

        if (other.CompareTag("Player") && gameObject.CompareTag("Flag")) // Ensure your player GameObject has the "Player" tag
        {
            // Perform any actions you want, like adding points, updating inventory, etc.
            Debug.Log("Player picked up the item!");

        
            chickenSecret.SecretCount(1);
            Destroy(gameObject);
            
        }
    }
}