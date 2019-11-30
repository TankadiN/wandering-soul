using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Debugging : MonoBehaviour
{
    public static Debugging instance;

    public GameObject MainMenu;
    public GameObject DebugMenu;

    void Start()
    {
        instance = this;
    }

    public void EnableDebugMenu()
    {
        AudioManager.instance.Play("victory");
        MainMenu.SetActive(false);
        DebugMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(DebugMenu.GetComponentInChildren<Button>().gameObject);
    }
}
