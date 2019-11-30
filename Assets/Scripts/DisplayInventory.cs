using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public List<Button> ItemSlots;
    [Header("Item Inspection")]
    public Image itemImage;
    public TMP_Text itemName;
    public TMP_Text itemDesc;
    public int saveID;

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

    public void ReUpdate()
    {
        for (int i = 0; i < ItemSlots.Count; i++)
        {
            ItemSlots[i].GetComponentInChildren<TMP_Text>().text = "[NO ITEM]";
        }
    }

    public void GetItemInfo(int ID)
    {
        saveID = ID;
        itemImage.sprite = inventory.Container[ID].item.image;
        itemName.text = inventory.Container[ID].item.name;
        itemDesc.text = inventory.Container[ID].item.description;
        GetComponent<ItemInspect>().ID = saveID;
        GetComponent<ItemInspect>().SendInfoToFlow();
        GetComponent<GameMenu>().ItemInspectOpen();
    }
}
