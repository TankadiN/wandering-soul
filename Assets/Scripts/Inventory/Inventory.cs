using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Inventory : MonoBehaviour
{
    public int maxItems;
    public bool isFull;
    public List<Item> Items;
    [SerializeField]
    private ItemDatabase database = null;
    [SerializeField]
    private Flowchart flow = null;

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

    public void CheckCapacity()
    {
        if(Items.Count < maxItems)
        {
            isFull = false;
        }
        else
        {
            isFull = true;
        }
        flow.SetBooleanVariable("isFull", isFull);
    }

    void Save()
    {
        SaveLoad.Save<List<Item>>(Items, "Inventory");
    }

    void Load()
    {
        if (SaveChecker.CheckSaveFile())
        {
            AddItems(SaveLoad.Load<List<Item>>("Inventory"));
        }
    }
}