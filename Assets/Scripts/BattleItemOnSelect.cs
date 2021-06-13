using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleItemOnSelect : MonoBehaviour, ISelectHandler
{
    public int ID;

    public void OnSelect (BaseEventData eventData)
    {
        GameObject.Find("GameManager").GetComponent<DisplayInventory>().GetItemInfoBattle(ID);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
