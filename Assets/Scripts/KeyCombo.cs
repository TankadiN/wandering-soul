using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class KeyCombo : MonoBehaviour
{
    public string inputname;
    private string[] strArr;
    public KeyCode[] combo;
    public int currentIndex = 0;
    public Flowchart flow;
    public string blockName;


    void Start()
    {
        strArr = new string[inputname.Length];
        for(int i = 0; i < inputname.Length; i++)
        {
            strArr[i] = System.Convert.ToString(inputname[i]);
        }

        combo = new KeyCode[strArr.Length];
        for (int i = 0; i < strArr.Length; i++)
        {
            combo[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), strArr[i]);
        }
    }

    void Update()
    {
        if (currentIndex < combo.Length)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(combo[currentIndex]))
                {
                    currentIndex++;
                }
                else
                {
                    Debug.Log("Combo broken.");
                    currentIndex = 0;
                }
            }
        }
        else
        {
            Debug.Log("Combo done.");
            flow.ExecuteBlock(blockName);
            currentIndex = 0;
        }
    }
}
