﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void TestingScene()
    {
        SceneManager.LoadScene(1);
    }

    public void DeleteSave()
    {
        SaveLoad.SeriouslyDeleteAllSaveFiles();
    }
}
