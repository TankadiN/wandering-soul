using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicMenu : MonoBehaviour
{
    public int number;

    public Image targetImage;
    public Sprite[] sprites;

    void Start()
    {
        Load();
        targetImage.sprite = sprites[number];
    }

    void Load()
    {
        if(SaveChecker.CheckSaveFile())
        {
            number = SaveLoad.Load<int>("MenuVariable");
        }
    }

}
