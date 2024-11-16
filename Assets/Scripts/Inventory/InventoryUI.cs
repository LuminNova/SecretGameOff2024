using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private static InventoryUI instance;

    public GameObject inventoryMenuGroup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(instance == null){
            instance = this;
        } else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        inventoryMenuGroup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)){
            inventoryMenuGroup.SetActive(!inventoryMenuGroup.activeSelf);
        }
    }
}
