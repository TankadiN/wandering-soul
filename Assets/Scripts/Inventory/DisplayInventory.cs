using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    public Inventory inventory;
    public List<Button> ItemSlots;
    [Header("Item Inspection")]
    public Sprite loadedSpr;
    public Image itemImage;
    public TMP_Text itemName;
    public TMP_Text itemDesc;
    public int saveID;

    void Start()
    {
        for(int i = 0; i < inventory.Items.Count; i++)
        {
            ItemSlots[i].GetComponentInChildren<TMP_Text>().text = inventory.Items[i].itemName;
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
        for (int i = 0; i < inventory.Items.Count; i++)
        {
            ItemSlots[i].GetComponentInChildren<TMP_Text>().text = inventory.Items[i].itemName;
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
#if UNITY_EDITOR
        loadedSpr = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Resources/Images/" + inventory.Items[ID].itemImageName + ".png");
#else
        loadedSpr = Resources.Load<Sprite>("Images/" + inventory.Items[ID].itemImageName);
#endif
        itemImage.sprite = loadedSpr;
        itemName.text = inventory.Items[ID].itemName;
        itemDesc.text = inventory.Items[ID].itemDescription;
        GetComponent<ItemInspect>().ID = saveID;
        GetComponent<ItemInspect>().SendInfoToFlow();
        GetComponent<GameMenu>().ItemInspectOpen();
    }
}
