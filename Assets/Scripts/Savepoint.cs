using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fungus;

public class Savepoint : MonoBehaviour
{
    public TMP_Text NameTextbox;
    public TMP_Text LvlTextbox;
    public TMP_Text TimeTextbox;
    public TMP_Text LocTextbox;

    public bool MainMenu;

    public GameObject SavePanel;
    public GameObject ButtonPanel;

    public string LastLocation;
    public float PlayerPositionX;
    public float PlayerPositionY;
    [Header("Time")]
    public int Hours;
    public int Minutes;
    public int Seconds;
    public float TimeMiliseconds;
    [Header("Colors")]
    public Color NormalColor;
    public Color SavedColor;

    [SerializeField]
    private Flowchart saveFlow;

    private void Start()
    {
        GameEvents.SaveInitiated += Save;
        Load();
    }

    void Update()
    {
        if (TimeMiliseconds >= 1)
        {
            Seconds++;
            TimeMiliseconds = 0;
        }
        if (Seconds == 60)
        {
            Minutes++;
            Seconds = 0;
        }
        if (Minutes == 60)
        {
            Hours++;
            Minutes = 0;
        }

        if(!MainMenu)
        {
            TimeMiliseconds += Time.deltaTime;
            if (!SavePanel.activeInHierarchy)
            {
                NameTextbox.color = NormalColor;
                LvlTextbox.color = NormalColor;
                TimeTextbox.color = NormalColor;
                LocTextbox.color = NormalColor;
                ButtonPanel.SetActive(true);
                GetComponent<GameMenu>().playerSaved = false;
            }
        }
    }


    public void GetLocation()
    {
        LastLocation = saveFlow.GetStringVariable("Location");
    }

    public void SaveProgress()
    {
        StartCoroutine(SaveData());
    }

    public IEnumerator SaveData()
    {
        GameEvents.OnSaveInitiated();
        NameTextbox.color = SavedColor;
        LvlTextbox.color = SavedColor;
        TimeTextbox.color = SavedColor;
        LocTextbox.color = SavedColor;
        ButtonPanel.SetActive(false);
        yield return new WaitForSeconds(1f);
        GetComponent<GameMenu>().playerSaved = true;
    }

    void Save()
    {
        SaveLoad.Save<int>(Hours, "TimeHours");
        SaveLoad.Save<int>(Minutes, "TimeMinutes");
        SaveLoad.Save<int>(Seconds, "TimeSeconds");
        SaveLoad.Save<string>(LastLocation, "PlayerLocationName");
        SaveLoad.Save<float>(PlayerPositionX, "PlayerPositionX");
        SaveLoad.Save<float>(PlayerPositionY, "PlayerPositionY");

        Load();
    }

    void Load()
    {
        if(SaveLoad.SaveExists("TimeHours") &&
            SaveLoad.SaveExists("TimeMinutes") &&
            SaveLoad.SaveExists("TimeSeconds") &&
            SaveLoad.SaveExists("PlayerLevel") &&
            SaveLoad.SaveExists("PlayerName") &&
            SaveLoad.SaveExists("PlayerLocationName") &&
            SaveLoad.SaveExists("PlayerPositionX") &&
            SaveLoad.SaveExists("PlayerPositionY"))
        {
            Hours = SaveLoad.Load<int>("TimeHours");
            Minutes = SaveLoad.Load<int>("TimeMinutes");
            Seconds = SaveLoad.Load<int>("TimeSeconds");

            TimeTextbox.text = Hours.ToString("0") + ":" + Minutes.ToString("00") + ":" + Seconds.ToString("00");
            NameTextbox.text = SaveLoad.Load<string>("PlayerName");
            LvlTextbox.text = "Lv " + SaveLoad.Load<float>("PlayerLevel").ToString();
            LocTextbox.text = SaveLoad.Load<string>("PlayerLocationName");
        }
    }
}
