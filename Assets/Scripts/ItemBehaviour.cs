using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public Item item;
    public Transform player;
    public float range;

    public GameObject movingItem;

    [SerializeField]
    private float dropDistance;

    private int originalDurability;

    void Start(){
        originalDurability = item.durability; 
    }

    void Update(){
    }

    void OnMouseDown(){
        Debug.Log("Mouse Clicked");

        // Pickup item with tag PickupItem
        if ((player.position-gameObject.transform.position).magnitude < range && gameObject.CompareTag("PickupItem"))
        {
            if(InventoryManager.instance.AddItem(item)){
                // Destroy the item after pickup
                Destroy(gameObject);
            } else {
                Debug.Log("Inventory full");
            }
        }

        if ((player.position-gameObject.transform.position).magnitude < range && gameObject.CompareTag("Shake"))
        {
            if(transform.childCount == 2){
                StartCoroutine(ItemDropping(movingItem.transform.position, movingItem.transform.position + new Vector3(0,dropDistance,0), .5f));
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
            // Calculate the interpolation factor
            float t = elapsedTime / duration;

            // Interpolate position
            movingItem.transform.position = Vector3.Lerp(start, end, t);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Wait until the next frame
            yield return null;
        }

        // Ensure the final position is exactly the target position
        movingItem.transform.position = end;
    }

    IEnumerator ExecuteSequentialTasks(){
        // Start Task A and wait for it to finish
        yield return StartCoroutine(LerpPosition(transform.position, transform.position + new Vector3(-.1f,0,0), .05f));

        // Start Task B and wait for it to finish
        yield return StartCoroutine(LerpPosition(transform.position, transform.position + new Vector3(.2f,0,0), .05f));

        // Start Task C and wait for it to finish
        yield return StartCoroutine(LerpPosition(transform.position, transform.position + new Vector3(-.1f,0,0), .05f));
    }

    IEnumerator LerpPosition(Vector3 start, Vector3 end, float duration){
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the interpolation factor
            float t = elapsedTime / duration;

            // Interpolate position
            transform.position = Vector3.Lerp(start, end, t);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Wait until the next frame
            yield return null;
        }

        // Ensure the final position is exactly the target position
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
        if (other.CompareTag("Player") && gameObject.CompareTag("EditorOnly")) // Ensure your player GameObject has the "Player" tag
        {
            // Perform any actions you want, like adding points, updating inventory, etc.
            // Debug.Log("Player picked up the item!");

            // if(InventoryManager.instance.AddItem(item)){
            //     // Destroy the item after pickup
            //     Destroy(gameObject);
            // } else {
            //     Debug.Log("Inventory full");
            // }

            Destroy(gameObject);
        }
    }
}