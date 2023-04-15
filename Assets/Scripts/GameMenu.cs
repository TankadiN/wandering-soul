using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class GameMenu : MonoBehaviour
{
    public GameObject canvas;
    public PostProcessVolume ppv;
    public float timeBlur;
    [Header("Ingame Menu")]
    public GameObject GamePanel;
    public GameObject ItemPanel;
    public GameObject ItemInspect;
    [Header("Buttons")]
    public GameObject ItemsButton;
    [Header("Save Menu")]
    public GameObject SavePanel;
    public bool playerSaved;


    private Interaction interaction;
    private Selectable resetSelect;
    private float timeElapsed;

    private void Start()
    {
        interaction = GameObject.Find("Player").GetComponent<Interaction>();
        resetSelect = GameObject.Find("ResetSelection").GetComponent<Selectable>();
        canvas.GetComponent<Animator>().SetTrigger("FadeIn");
    }

    private void Update()
    {
        if (timeElapsed < timeBlur)
        {
            if(GamePanel.activeInHierarchy)
            {
                ppv.weight = Mathf.Lerp(ppv.weight, 1, timeElapsed / timeBlur);
            }
            else
            {
                ppv.weight = Mathf.Lerp(ppv.weight, 0, timeElapsed / timeBlur);
            }
            timeElapsed += Time.deltaTime;
        }

        if (GameObject.Find("BattleManager").GetComponent<TurnHandle>().state == BattleState.StandBy)
        {
            if (Input.GetButtonDown("Menu"))
            {
                if (SavePanel.activeInHierarchy)
                {
                    if (playerSaved)
                    {
                        SavePanel.SetActive(false);
                        interaction.InteractionSwitch();
                        EventSystem.current.SetSelectedGameObject(SavePanel.transform.Find("ButtonList").transform.Find("Return").gameObject);
                    }
                }
                else
                {
                    if (GamePanel.activeInHierarchy)
                    {
                        CloseAll();
                        interaction.InteractionSwitch();
                        EventSystem.current.SetSelectedGameObject(resetSelect.gameObject);
                    }
                    else
                    {
                        GamePanel.SetActive(true);
                        EventSystem.current.SetSelectedGameObject(GamePanel.GetComponentInChildren<Button>().gameObject);
                        interaction.InteractionSwitch();
                    }
                }
                timeElapsed = 0f;
            }
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if(SavePanel.activeInHierarchy)
            {
                SavePanel.SetActive(false);
                interaction.InteractionSwitch();
                EventSystem.current.SetSelectedGameObject(SavePanel.transform.Find("ButtonList").transform.Find("Return").gameObject);
            }
            else if (ItemInspect.activeInHierarchy)
            {
                ItemInspect.SetActive(false);
                EventSystem.current.SetSelectedGameObject(ItemPanel.GetComponentInChildren<Button>().gameObject);
            }
            else if (ItemPanel.activeInHierarchy)
            {
                ItemPanel.SetActive(false);
                EventSystem.current.SetSelectedGameObject(GamePanel.GetComponentInChildren<Button>().gameObject);
                ItemsButton.transform.Find("Background_Sel").gameObject.SetActive(false);
            }
            else if (GamePanel.activeInHierarchy)
            {
                GamePanel.SetActive(false);
                EventSystem.current.SetSelectedGameObject(null);
                interaction.InteractionSwitch();
            }
            timeElapsed = 0f;
        }
        if(Input.GetButtonDown("Submit"))
        {
            if (SavePanel.activeInHierarchy)
            {
                if (playerSaved)
                {
                    SavePanel.SetActive(false);
                    interaction.InteractionSwitch();
                    EventSystem.current.SetSelectedGameObject(SavePanel.transform.Find("ButtonList").transform.Find("Return").gameObject);
                }
            }
        }
    }

    public void CloseAll()
    {
        ItemInspect.SetActive(false);
        ItemPanel.SetActive(false);
        GamePanel.SetActive(false);
        timeElapsed = 0f;
    }

    public void ItemPanelOpen()
    {
        ItemPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(ItemPanel.GetComponentInChildren<Button>().gameObject);
        ItemsButton.transform.Find("Background_Sel").gameObject.SetActive(true);
    }

    public void ItemInspectOpen()
    {
        ItemInspect.SetActive(true);
        EventSystem.current.SetSelectedGameObject(ItemInspect.GetComponentInChildren<Button>().gameObject);
    }

    public void SavePanelOpen()
    {
        SavePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(SavePanel.GetComponentInChildren<Button>().gameObject);
    }
}
