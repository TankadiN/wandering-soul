using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public List<Button> ItemSlots;

    void Start()
    {
        for(int i = 0; i < inventory.Container.Count; i++)
        {
            ItemSlots[i].GetComponentInChildren<TMP_Text>().text = inventory.Container[i].item.name;
        }
        for (int i = 0; i < ItemSlots.Count; i++)
        {
            if (ItemSlots[i].GetComponentInChildren<TMP_Text>().text == "[NO ITEM]")
            {
                ItemSlots[i].interactable = false;
            }
            else
            {
                ItemSlots[i].interactable = true;
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            ItemSlots[i].GetComponentInChildren<TMP_Text>().text = inventory.Container[i].item.name;
        }
        for (int i = 0; i < ItemSlots.Count; i++)
        {
            if (ItemSlots[i].GetComponentInChildren<TMP_Text>().text == "[NO ITEM]")
            {
                ItemSlots[i].interactable = false;
            }
            else
            {
                ItemSlots[i].interactable = true;
            }
        }
    }
}
