using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuInput : MonoBehaviour
{
    public Button[] buttons;
    public GameObject FirstSelect;
    public GameObject Selected;

    void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(FirstSelect);
    }

    void Update()
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject);
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
            }
            Debug.Log(EventSystem.current.currentSelectedGameObject + " is NOT active!");
        }
        Selected = EventSystem.current.currentSelectedGameObject;
    }
}
