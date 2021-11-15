using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    public static readonly string SAVE_FOLDER = Application.persistentDataPath + "/Saves/";

    public static void Init()
    {
        if(!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void Save(string saveString)
    {
        File.WriteAllText(SAVE_FOLDER + "Save.txt", saveString);
    }

    public static string Load()
    {
        if(File.Exists(SAVE_FOLDER + "Save.txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "Save.txt");
            return saveString;
        }
        else
        {
            return null;
        }
    }
}

