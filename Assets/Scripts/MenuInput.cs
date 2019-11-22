using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MenuInput : MonoBehaviour, ISelectHandler
{
    public Button[] buttons;
    public GameObject FirstSelect;
    public GameObject Selected;

    private void Awake()
    {
        Selected = FirstSelect;
    }

    void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(FirstSelect);
        EventSystem.current.SetSelectedGameObject(null);
    }

    void Update()
    {
        //Debug.Log(EventSystem.current.currentSelectedGameObject);
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(Selected);
        }
        if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable == false)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if(buttons[i].interactable)
                {
                    EventSystem.current.SetSelectedGameObject(buttons[i].gameObject);
                    break;
                }
                else
                {
                    Debug.Log(EventSystem.current.currentSelectedGameObject + " is NOT active!");
                }
            }
        }
        Selected = EventSystem.current.currentSelectedGameObject;
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("Changed Button");
    }
}
