using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using TMPro;

public class TypeInput : MonoBehaviour
{
    public TMP_Text playerNameTextbox;
    public Flowchart flow;
    public string playerName;
    public bool canEdit;
    public int curLetters;
    public int maxLetters;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerNameTextbox.text = playerName;
        if(canEdit)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                DeleteLetter();
            }
        }
    }

    public void AddLetter(string letter)
    {
        if(curLetters == maxLetters)
        {
            Debug.Log("Max Letter Capacity Reached.");
        }
        else
        {
            playerName += letter;
            curLetters++;
        }
    }

    public void EditSwitch()
    {
        if(canEdit)
        {
            canEdit = false;
        }
        else
        {
            canEdit = true;
        }
    }

    public void DeleteLetter()
    {
        if (curLetters == 0)
        {
            Debug.Log("There is nothing to delete.");
        }
        else
        {
            playerName = playerName.Substring(0, playerName.Length - 1);
            curLetters--;
        }
    }

    public void StoreName()
    {
        GlobalVariables.instance.playerName = playerName;
    }

    public void SendToFlow()
    {
        flow.SetStringVariable("Name", playerName);
    }
}
