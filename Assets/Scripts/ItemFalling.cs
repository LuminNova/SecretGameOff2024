using UnityEngine;

public class ItemFalling : MonoBehaviour
{

    public GameObject fallingItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(fallingItem != null){
            fallingItem.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
