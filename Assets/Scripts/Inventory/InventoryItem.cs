using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;

    public int count = 1;
    public TMP_Text countText;

    [HideInInspector]
    public Transform parentAfterDrag;
    [HideInInspector]
    public Item item;

    public void InitializeItem(Item newItem){
        item = newItem;
        image.sprite = newItem.image;
        RefreshCount();
    }

    public void RefreshCount(){
        countText.text = count.ToString();
        countText.gameObject.SetActive(count > 1);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
}
