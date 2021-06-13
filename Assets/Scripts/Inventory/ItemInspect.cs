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

    public void UseBattleItem()
    {
        if(checkUse())
        {
            Debug.Log("You can't use air !");
        }
        else
        {
            if (flow.HasBlock("UseItemBattle"))
            {
                flow.ExecuteBlock("UseItemBattle");
            }
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
        if (ID >= inventory.Items.Count)
        {
            flow.SetStringVariable("ItemName", null);
            flow.SetStringVariable("ItemType", null);
            flow.SetFloatVariable("HpValue", 0);
        }
        else
        {
            flow.SetStringVariable("ItemName", inventory.Items[ID].itemName);
            flow.SetStringVariable("ItemType", inventory.Items[ID].itemType);
            flow.SetFloatVariable("HpValue", inventory.Items[ID].itemHealthRecovery);
        }
    }

    public bool checkUse()
    {
        if(ID >= inventory.Items.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
