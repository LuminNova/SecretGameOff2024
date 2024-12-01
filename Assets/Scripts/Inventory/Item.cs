using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite image;
    public bool isStackable;
    public bool isUsableItem;
    public int damageNumber;
    public int durability;
}
