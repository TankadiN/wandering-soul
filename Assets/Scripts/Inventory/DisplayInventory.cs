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
    public List<Button> BattleItemSlots;
    [Header("Item Inspection")]
    public Sprite loadedSpr;
    public Image itemImage;
    public TMP_Text itemName;
    public TMP_Text itemDesc;
    public int saveID;
    [Header("Item Inspection Battle")]
    public Image itemImageBattle;
    public TMP_Text itemNameBattle;
    public TMP_Text itemDescBattle;

    void Start()
    {
        for (int i = 0; i < inventory.Items.Count; i++)
        {
            ItemSlots[i].GetComponentInChildren<TMP_Text>().text = inventory.Items[i].itemName;
            BattleItemSlots[i].GetComponentInChildren<TMP_Text>().text = "*";
        }
        for (int i = 0; i < ItemSlots.Count; i++)
        {
            if (ItemSlots[i].GetComponentInChildren<TMP_Text>().text == "[NO ITEM]")
            {
                ItemSlots[i].GetComponentInChildren<TMP_Text>().color = ItemSlots[i].colors.pressedColor;
            }
            else
            {
                ItemSlots[i].GetComponentInChildren<TMP_Text>().color = ItemSlots[i].colors.highlightedColor;
            }
        }
        for (int i = 0; i < BattleItemSlots.Count; i++)
        {
            if (BattleItemSlots[i].GetComponentInChildren<TMP_Text>().text == ".")
            {
                BattleItemSlots[i].GetComponentInChildren<TMP_Text>().color = ItemSlots[i].colors.pressedColor;
            }
            else
            {
                BattleItemSlots[i].GetComponentInChildren<TMP_Text>().color = ItemSlots[i].colors.highlightedColor;
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < inventory.Items.Count; i++)
        {
            ItemSlots[i].GetComponentInChildren<TMP_Text>().text = inventory.Items[i].itemName;
            BattleItemSlots[i].GetComponentInChildren<TMP_Text>().text = "*";
        }
        for (int i = 0; i < ItemSlots.Count; i++)
        {
            if (ItemSlots[i].GetComponentInChildren<TMP_Text>().text == "[NO ITEM]")
            {
                ItemSlots[i].GetComponentInChildren<TMP_Text>().color = ItemSlots[i].colors.pressedColor;
            }
            else
            {
                ItemSlots[i].GetComponentInChildren<TMP_Text>().color = ItemSlots[i].colors.highlightedColor;
            }
        }
        for (int i = 0; i < BattleItemSlots.Count; i++)
        {
            if (BattleItemSlots[i].GetComponentInChildren<TMP_Text>().text == ".")
            {
                BattleItemSlots[i].GetComponentInChildren<TMP_Text>().color = ItemSlots[i].colors.pressedColor;
            }
            else
            {
                BattleItemSlots[i].GetComponentInChildren<TMP_Text>().color = ItemSlots[i].colors.highlightedColor;
            }
        }
    }

    public void ReUpdate()
    {
        for (int i = 0; i < ItemSlots.Count; i++)
        {
            ItemSlots[i].GetComponentInChildren<TMP_Text>().text = "[NO ITEM]";
        }
        for (int i = 0; i < BattleItemSlots.Count; i++)
        {
            BattleItemSlots[i].GetComponentInChildren<TMP_Text>().text = ".";
        }
    }

    public void GetItemInfo(int ID)
    {
        if (ID >= inventory.Items.Count)
        {
            Debug.Log("There is no item on this slot");
        }
        else
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

    public void GetItemInfoBattle(int ID)
    {
        saveID = ID;
        if (saveID >= inventory.Items.Count)
        {
            Debug.Log("There is no item on this slot");
            itemImageBattle.sprite = null;
            itemImageBattle.color = new Color(itemImageBattle.color.r, itemImageBattle.color.g, itemImageBattle.color.b, 0f);
            itemNameBattle.color = new Color(itemNameBattle.color.r, itemNameBattle.color.g, itemNameBattle.color.b, 0.5f);
            itemNameBattle.text = "[NO ITEM]";
            itemDescBattle.text = "";
        }
        else
        {
#if UNITY_EDITOR
            loadedSpr = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Resources/Images/" + inventory.Items[saveID].itemImageName + ".png");
#else
            loadedSpr = Resources.Load<Sprite>("Images/" + inventory.Items[ID].itemImageName);
#endif
            itemImageBattle.color = new Color(itemImageBattle.color.r, itemImageBattle.color.g, itemImageBattle.color.b, 1f);
            itemImageBattle.sprite = loadedSpr;
            itemNameBattle.color = new Color(itemNameBattle.color.r, itemNameBattle.color.g, itemNameBattle.color.b, 1f);
            itemNameBattle.text = inventory.Items[saveID].itemName;
            itemDescBattle.text = inventory.Items[saveID].itemDescription;
        }
        GetComponent<ItemInspect>().ID = saveID;
        GetComponent<ItemInspect>().SendInfoToFlow();
        Debug.Log("Item Slot:" + saveID);
    }
}
