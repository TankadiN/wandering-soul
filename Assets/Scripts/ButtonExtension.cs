using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonExtension : MonoBehaviour
{
    private Button myButton;
    public bool toPressedColor;
    public List<Image> Images;
    public List<TMP_Text> Texts;

    void Start()
    {
        myButton = GetComponent<Button>();
    }

    void Update()
    {
        if(myButton.interactable == false)
        {
            for(int i = 0; i <= Images.Count - 1; i++)
            {
                if(toPressedColor)
                {
                    Images[i].color = myButton.colors.pressedColor;
                }
                else
                {
                    Images[i].color = myButton.colors.highlightedColor;
                }
            }
            for (int i = 0; i <= Texts.Count - 1; i++)
            {
                Texts[i].color = myButton.colors.pressedColor;
            }
        }
        else
        {
            for (int i = 0; i <= Images.Count - 1; i++)
            {
                Images[i].color = myButton.colors.normalColor;
            }
            for (int i = 0; i <= Texts.Count - 1; i++)
            {
                Texts[i].color = myButton.colors.highlightedColor;
            }
        }
    }
}
