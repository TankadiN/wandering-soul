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

    [Header("Buttons")]
    public GameObject newGameButton;
    public GameObject continueButton;
    public GameObject deleteSaveButton;

    [Header("Panels")]
    public GameObject deleteSavePanel;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(buttonList.GetComponentInChildren<Selectable>().gameObject);

        if (SaveChecker.CheckSaveFile())
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

    public void DeletePopup()
    {
        if (deleteSavePanel.activeInHierarchy)
        {
            deleteSavePanel.SetActive(false);
            EventSystem.current.SetSelectedGameObject(buttonList.GetComponentInChildren<Button>().gameObject);
        }
        else
        {
            deleteSavePanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(deleteSavePanel.GetComponentInChildren<Button>().gameObject);
        }
    }

    public void DeleteSave()
    {
        SaveLoad.SeriouslyDeleteAllSaveFiles();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}
