using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class ItemInspect : MonoBehaviour
{
    public InventoryObject inventory;
    public int ID;

    public Flowchart flow;

    public void UseItem()
    {
        if (flow.HasBlock("UseItem"))
        {
            flow.ExecuteBlock("UseItem");
            GetComponent<GameMenu>().CloseAll();
        }
    }

    public void DropItem()
    {
        if (flow.HasBlock("DropItem"))
        {
            flow.ExecuteBlock("DropItem");
            GetComponent<GameMenu>().CloseAll();
        }
    }

    public void RemoveItem(int ID_Pos)
    {
        inventory.RemoveItem(ID_Pos);
        GetComponent<DisplayInventory>().ReUpdate();
    }

    public void SendInfoToFlow()
    {
        flow.SetIntegerVariable("ItemID", ID);
        flow.SetStringVariable("ItemType", inventory.Container[ID].item.type.ToString());
        flow.SetStringVariable("ItemName", inventory.Container[ID].item.name);
        flow.SetFloatVariable("HpValue", inventory.Container[ID].item.restoreHealthValue);
    }
}
