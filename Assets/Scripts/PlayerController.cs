using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
using TMPro;

public class PlayerController : MonoBehaviour {

    [Header("Game Objects")]
    public List<Image> bar;
    public List<TMP_Text> nameTextbox;
    public List<TMP_Text> hpTextbox;
    public List<TMP_Text> lvlTextbox;
    [Header("GameOver Objects")]
    public Flowchart mainFlow;
    public List<GameObject> playerObjects;
    [Header("Player Variables")]
    public string playerName;
    public int menuArtNumber;
    public float money;
    [Header("Health Variables")]
    public float maxHealth;
    public float currentHealth;
    public float invTime;
    private float curInvTime;
    [Header("Level Variables")]
    public float level;
    public float maxExperience;
    public float currentExperience;
    [Header("Attack")]
    public float attackValue;
    [Header("Color Control")]
    public SpriteRenderer outlinePlayer;
    public SpriteRenderer spritePlayer;
    public SpriteRenderer soulPlayer;
    [Header("Color Control Soul")]
    public List<SpriteRenderer> soulHeart;

    public GameObject playerObject;
    public GameObject playerBattleObject;

    private RectTransform CanvasRect;

    void Start ()
    {
        CanvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
        currentHealth = maxHealth;
        soulPlayer.color = new Color(outlinePlayer.color.r, outlinePlayer.color.g, outlinePlayer.color.b, 255);
        if(GlobalVariables.instance)
        {
            if (string.IsNullOrEmpty(GlobalVariables.instance.playerName))
            {

            }
            else
            {
                playerName = GlobalVariables.instance.playerName;
            }
        }
        SendPlayerNameToFlow();
        //GameEvents.SaveInitiated += Save;
        Load();
    }
	
	void Update ()
    {
        if(curInvTime > 0)
        {
            curInvTime -= Time.deltaTime;
            foreach (SpriteRenderer spr in soulHeart)
            {
                spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 0.5f);
            }
        }

        foreach(SpriteRenderer spr in soulHeart)
        {
            if (curInvTime < 0)
            {
                spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 1);
            }
        }

        if (currentExperience >= maxExperience)
        {
            LevelUp();
        }
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        //Display Logic
        string sMaxHp = maxHealth.ToString();
        string sCurHp = currentHealth.ToString();
        string sLvl = maxHealth.ToString();
        string sMaxExp = maxExperience.ToString();
        string sCurExp = currentExperience.ToString();

        foreach(Image i in bar)
        {
            i.fillAmount = currentHealth / maxHealth;
        }
        foreach (TMP_Text t in nameTextbox)
        {
            t.text = playerName;
        }
        foreach (TMP_Text t in hpTextbox)
        {
            t.text = sCurHp + "/" + sMaxHp;
        }
        foreach (TMP_Text t in lvlTextbox)
        {
            t.text = "lv " + level;
        }
        //expTextbox[i].text = "exp " + sCurExp + "/" + sMaxExp;
    }

    public void LevelUp()
    {
        AudioManager.instance.Play("levelup");
        level++;
        maxHealth += 5;
        currentHealth = maxHealth;
        float leftoverExp = currentExperience - maxExperience;
        maxExperience += 50;
        currentExperience = 0 + leftoverExp;
        
    }

    public void Damage(float amount)
    {
        if (curInvTime <= 0)
        {
            AudioManager.instance.Play("soul_hurt");
            currentHealth -= amount;
            ValuePopup.Create(playerObject.transform.position, amount, ValuePopup.Type.Damage);
            ValuePopup.Create(playerBattleObject.transform.position, amount, ValuePopup.Type.Damage);
            if (currentHealth <= 0)
            {
                Death();
            }
            curInvTime = invTime;
        }
    }

    public void Death()
    {
        foreach(GameObject o in playerObjects)
        {
            if(o.activeInHierarchy)
            {
                o.SetActive(false);
            }
        }
        mainFlow.ExecuteBlock("GameOver");
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
    }

    public void FullHeal()
    {
        currentHealth = maxHealth;
    }

    public void GiveExp(float amount)
    {
        currentExperience += amount;
    }

    public void SetMenuArtNumber(int newNumber)
    {
        menuArtNumber = newNumber;
    }

    public void SendPlayerNameToFlow()
    {
        mainFlow.SetStringVariable("playerName", playerName);
    }
    
    void Load()
    {
        string saveString = SaveSystem.Load();
        if (saveString != null)
        {
            Savepoint.SaveData saveData = JsonUtility.FromJson<Savepoint.SaveData>(saveString);

            playerName = saveData.playerName;
            maxHealth = saveData.maxHealth;
            currentHealth = saveData.currentHealth;
            level = saveData.level;
            maxExperience = saveData.maxExperience;
            currentExperience = saveData.currentExperience;
            playerObjects[0].transform.position = saveData.PlayerPosition;
        }
    }
}
