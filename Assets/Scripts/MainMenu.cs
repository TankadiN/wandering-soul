using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject buttonList;

    public GameObject saveFile;
    public GameObject newGameButton;
    public GameObject continueButton;
    public GameObject deleteSaveButton;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(buttonList.GetComponentInChildren<Selectable>().gameObject);

        if (SaveLoad.SaveExists("TimeHours") &&
            SaveLoad.SaveExists("TimeMinutes") &&
            SaveLoad.SaveExists("TimeSeconds") &&
            SaveLoad.SaveExists("PlayerLevel") &&
            SaveLoad.SaveExists("PlayerName") &&
            SaveLoad.SaveExists("PlayerMaxHP") &&
            SaveLoad.SaveExists("PlayerCurHP") &&
            SaveLoad.SaveExists("PlayerMaxXP") &&
            SaveLoad.SaveExists("PlayerCurXP") &&
            SaveLoad.SaveExists("PlayerLocationName") &&
            SaveLoad.SaveExists("PlayerPositionX") &&
            SaveLoad.SaveExists("PlayerPositionY") &&
            SaveLoad.SaveExists("CameraPriorities") &&
            SaveLoad.SaveExists("Inventory"))
        {
            newGameButton.SetActive(false);
        }
        else
        {
            saveFile.SetActive(false);
            continueButton.SetActive(false);
            deleteSaveButton.SetActive(false);
        }
    }

    public void NewGameOrContinue()
    {
        SceneManager.LoadScene(2);
    }

    public void TestingScene()
    {
        SceneManager.LoadScene("TestingPlace");
    }

    public void DeleteSave()
    {
        SaveLoad.SeriouslyDeleteAllSaveFiles();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
