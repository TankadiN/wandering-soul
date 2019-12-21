using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class ItemInspect : MonoBehaviour
{
    public Inventory inventory;
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

    public void SendInfoToFlow()
    {
        flow.SetStringVariable("ItemName", inventory.Items[ID].itemName);
        flow.SetStringVariable("ItemType", inventory.Items[ID].itemType);
        flow.SetFloatVariable("HpValue", inventory.Items[ID].itemHealthRecovery);
    }
}
