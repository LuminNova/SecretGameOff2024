using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public Item item;
    public Transform player;
    public float range;

    void Start(){

    }

    void Update(){
    }

    void OnMouseDown(){
        Debug.Log("Mouse Clicked");
        if ((player.position-gameObject.transform.position).magnitude < range) // Vector3.Distance() is also possible
        {
            if(InventoryManager.instance.AddItem(item)){
                // Destroy the item after pickup
                Destroy(gameObject);
            } else {
                Debug.Log("Inventory full");
            }
        }
    }


    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     // Check if the player is colliding with the item
    //     if (other.CompareTag("Player")) // Ensure your player GameObject has the "Player" tag
    //     {
    //         // Perform any actions you want, like adding points, updating inventory, etc.
    //         Debug.Log("Player picked up the item!");

    //         if(InventoryManager.instance.AddItem(item)){
    //             // Destroy the item after pickup
    //             Destroy(gameObject);
    //         } else {
    //             Debug.Log("Inventory full");
    //         }
    //     }
    // }
}