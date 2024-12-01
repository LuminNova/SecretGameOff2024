using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItems;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    public GameObject inventoryItemPrefab;

    public static InventoryManager instance;

    int selectedSlot = -1;

    private void Start(){
        ChangeSelectedSlot(0);
        instance = this;
    }

    private void Update(){
        if(Input.inputString != null){
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if(isNumber && number > 0 && number < 9){
                ChangeSelectedSlot(number - 1);
            }
        }
    }

    // Update selected slot colour
    public void ChangeSelectedSlot(int newValue){
        if(selectedSlot >= 0){
            inventorySlots[selectedSlot].Deselect();
        }
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(Item item){
        // Checks if any slot has the same item with the count lower than max amount
        for(int i = 0; i < inventorySlots.Count; i++){
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot =  slot.GetComponentInChildren<InventoryItem>();
            // Slot is occupied, same item, less than max amount, is stackable
            if(itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems && itemInSlot.item.isStackable){
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        // Finds first empty slot
        for(int i = 0; i < inventorySlots.Count; i++){
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot =  slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot == null){
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    // For Testing
    public void SpawnNewItem(Item item, InventorySlot slot){
        GameObject newItemGameObject = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGameObject.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    
    }

    // Get item in selected slot or null item
    public Item GetSelectedItem(bool use){
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot =  slot.GetComponentInChildren<InventoryItem>();
        if(itemInSlot != null){
            Item item = itemInSlot.item;
            if(use){
                itemInSlot.count--;
                if(itemInSlot.count <= 0){
                    //Destroy(itemInSlot.gameObject);
                } else {
                    itemInSlot.RefreshCount();
                }
            }
            return item;
        }
        return null;
    }
}
