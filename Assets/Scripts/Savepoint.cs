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
    private Flowchart saveFlow = null;

    private PlayerController playerCtrl;
    private Inventory playerInv;
    private CameraSave camSave;

    private void Awake()
    {
        SaveSystem.Init();
    }

    private void Start()
    {
        Load();
        playerCtrl = GameObject.Find("GameManager").GetComponent<PlayerController>();
        playerInv = GameObject.Find("GameManager").GetComponent<Inventory>();
        camSave = GameObject.Find("GameManager").GetComponent<CameraSave>();
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
        StartCoroutine(InitSaveData());
    }

    public IEnumerator InitSaveData()
    {
        camSave.GatherPriorities();
        SaveData saveData = new SaveData
        {
            playerName = playerCtrl.playerName,
            maxHealth = playerCtrl.maxHealth,
            currentHealth = playerCtrl.currentHealth,
            level = playerCtrl.level,
            maxExperience = playerCtrl.maxExperience,
            currentExperience = playerCtrl.currentExperience,
            Hours = Hours,
            Minutes = Minutes,
            Seconds = Seconds,
            LastLocation = LastLocation,
            PlayerPosition = new Vector2(PlayerPositionX, PlayerPositionY),
            camPriority = camSave.priority,
            menuArtNumber = playerCtrl.menuArtNumber,
            Items = playerInv.Items
        };
        string json = JsonUtility.ToJson(saveData);
        SaveSystem.Save(json);

        NameTextbox.color = SavedColor;
        LvlTextbox.color = SavedColor;
        TimeTextbox.color = SavedColor;
        LocTextbox.color = SavedColor;

        ButtonPanel.SetActive(false);
        AudioManager.instance.Play("savedgame");
        Load();
        yield return new WaitForSeconds(1f);
        GetComponent<GameMenu>().playerSaved = true;
    }

    void Load()
    {
        string saveString = SaveSystem.Load();
        if(saveString != null)
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(saveString);

            TimeTextbox.text = saveData.Hours.ToString("0") + ":" + saveData.Minutes.ToString("00") + ":" + saveData.Seconds.ToString("00");
            NameTextbox.text = saveData.playerName;
            LvlTextbox.text = "Lv " + saveData.level.ToString();
            LocTextbox.text = saveData.LastLocation;
        }
    }

    public class SaveData
    {
        public string playerName;
        public float maxHealth;
        public float currentHealth;
        public float level;
        public float maxExperience;
        public float currentExperience;
        public int Hours;
        public int Minutes;
        public int Seconds;
        public string LastLocation;
        public Vector2 PlayerPosition;
        public List<int> camPriority;
        public int menuArtNumber;
        public List<Item> Items;
    }
}
