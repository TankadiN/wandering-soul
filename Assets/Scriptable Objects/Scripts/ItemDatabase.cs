using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Items", menuName = "Inventory System/New Item Database", order = 5)]
public class ItemDatabase : ScriptableObject
{
    public List<Item> Items;

    public Item GetItem(string itemName)
    {
        return Items.Find(x=>x.itemName == itemName);
    }

    public Item GetItem(int itemIndex)
    {
        return Items[itemIndex];
    }
}
[System.Serializable]
public class Item
{
    public string itemName;
    public string itemType;
    public string itemImageName;
    [TextArea]
    public string itemDescription;
    public float itemHealthRecovery;

    public Item(string name, string type, string image, string description, float hpValue)
    {
        itemName = name;
        itemType = type;
        itemImageName = image;
        itemDescription = description;
        itemHealthRecovery = hpValue;
    }
}
