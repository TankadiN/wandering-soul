using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuInput : MonoBehaviour
{
    public static MenuInput instance;
    public Button[] buttons;
    public GameObject FirstSelect;
    public GameObject Selected;

    void Awake()
    {
        EventSystem.current.SetSelectedGameObject(FirstSelect);
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        //Debug.Log(EventSystem.current.currentSelectedGameObject);
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(Selected);
        }
        else
        {
            Selected = EventSystem.current.currentSelectedGameObject;
        }
    }
}
