using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> Items;
    [SerializeField]
    private ItemDatabase database;

    private void Start()
    {
        GameEvents.SaveInitiated += Save;
        Load();
    }

    public void AddItem(string itemName)
    {
        Item itemToAdd = database.GetItem(itemName);
        Items.Add(itemToAdd);
        GameEvents.OnItemAddedToInventory(itemToAdd);
        Debug.Log("Item addded.");
    }

    public void RemoveItem(string itemName)
    {
        Item itemToRemove = database.GetItem(itemName);
        Items.Remove(itemToRemove);
        Debug.Log("Item removed.");
        GameObject.Find("GameManager").GetComponent<DisplayInventory>().ReUpdate();
    }

    public void AddItems(List<Item> items)
    {
        foreach(Item item in items)
        {
            AddItem(item.itemName);
        }
    }

    void Save()
    {
        SaveLoad.Save<List<Item>>(Items, "Inventory");
    }

    void Load()
    {
        if (SaveLoad.SaveExists("Inventory"))
        {
            AddItems(SaveLoad.Load<List<Item>>("Inventory"));
        }
    }
}