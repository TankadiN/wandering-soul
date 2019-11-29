using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public GameObject GamePanel;
    public GameObject ItemPanel;

    private void Update()
    {
        if(Input.GetButtonDown("Menu"))
        {
            if(GamePanel.activeInHierarchy)
            {
                GamePanel.SetActive(false);
                ItemPanel.SetActive(false);
            }
            else
            {
                GamePanel.SetActive(true);
            }
        }
    }

    public void ItemPanelOpen()
    {
        ItemPanel.SetActive(true);
    }
}
