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
        string saveString = SaveSystem.Load();
        if (saveString != null)
        {
            Savepoint.SaveData saveData = JsonUtility.FromJson<Savepoint.SaveData>(saveString);

            number = saveData.menuArtNumber;
        }
    }

}
