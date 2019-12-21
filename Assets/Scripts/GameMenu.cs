using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject GamePanel;
    [Header("Items")]
    public GameObject ItemPanel;
    public GameObject ItemInspect;

    private Interaction interaction;

    private void Start()
    {
        interaction = GameObject.Find("Player").GetComponent<Interaction>();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Menu"))
        {
            if(GamePanel.activeInHierarchy)
            {
                CloseAll();
                interaction.InteractionSwitch();
                EventSystem.current.SetSelectedGameObject(GamePanel.transform.Find("MainList").transform.Find("Status").gameObject);
            }
            else
            {
                GamePanel.SetActive(true);
                EventSystem.current.SetSelectedGameObject(GamePanel.GetComponentInChildren<Button>().gameObject);
                interaction.InteractionSwitch();
            }
        }
        if (Input.GetButtonDown("Cancel"))
        {
            if (ItemInspect.activeInHierarchy)
            {
                ItemInspect.SetActive(false);
                EventSystem.current.SetSelectedGameObject(ItemPanel.GetComponentInChildren<Button>().gameObject);
            }
            else if (ItemPanel.activeInHierarchy)
            {
                ItemPanel.SetActive(false);
                EventSystem.current.SetSelectedGameObject(GamePanel.GetComponentInChildren<Button>().gameObject);
            }
            else if (GamePanel.activeInHierarchy)
            {
                GamePanel.SetActive(false);
                EventSystem.current.SetSelectedGameObject(GamePanel.transform.Find("MainList").transform.Find("Status").gameObject);
                interaction.InteractionSwitch();
            }
        }
    }

    public void CloseAll()
    {
        ItemInspect.SetActive(false);
        ItemPanel.SetActive(false);
        GamePanel.SetActive(false);
    }

    public void ItemPanelOpen()
    {
        ItemPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(ItemPanel.GetComponentInChildren<Button>().gameObject);
    }

    public void ItemInspectOpen()
    {
        ItemInspect.SetActive(true);
        EventSystem.current.SetSelectedGameObject(ItemInspect.GetComponentInChildren<Button>().gameObject);
    }

    public void GameSave()
    {
        GameEvents.OnSaveInitiated();
    }
}
