using UnityEngine;

public class ItemPopup : MonoBehaviour
{
    public GameObject popupUI; // Assign the UI GameObject in the Inspector

    private void Start()
    {
        // Ensure the popup is hidden at the start
        if (popupUI != null){
            popupUI.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("enter");
            // Show the popup UI
            if (popupUI != null)
                popupUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Player"))
        {
            Debug.Log("left");
            // Show the popup UI
            if (popupUI != null)
                popupUI.SetActive(false);
        }
    }
}
