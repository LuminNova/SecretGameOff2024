using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color selectedColour, notSelectedColour;

    public void Awake(){
        Deselect();
    }

    public void Select(){
        image.color = selectedColour;
    }

    public void Deselect(){
        image.color = notSelectedColour;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0){
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }
    }
}
