using System.Collections.Generic;
using UnityEngine;

// Testing script
public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public List<Item> itemsToPickup = new List<Item>();

    public void PickupItem(int id){
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if(result){
            Debug.Log("Item added");
        } else {
            Debug.Log("Item NOT added");
        }
    }

    public void GetSelectedItem(){
        Item receivedItem = inventoryManager.GetSelectedItem(false);
        if(receivedItem != null){
            Debug.Log("Received item: " + receivedItem);
        } else {
            Debug.Log("No item received");
        }
    }

    public void UseSelectedItem(){
        Item receivedItem = inventoryManager.GetSelectedItem(true);
        if(receivedItem != null){
            Debug.Log("Used item: " + receivedItem);
        } else {
            Debug.Log("No item used");
        }
    }
}
