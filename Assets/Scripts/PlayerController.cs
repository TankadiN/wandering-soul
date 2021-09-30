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
    [Header("Health Variables")]
    public float maxHealth;
    public float currentHealth;
    public float invTime;
    [Header("Level Variables")]
    public float level;
    public float maxExperience;
    public float currentExperience;
    [Header("Color Control")]
    public SpriteRenderer outlinePlayer;
    public SpriteRenderer soulPlayer;
    [Header("Color Control Soul")]
    public List<SpriteRenderer> soulHeart;

    void Start ()
    {
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
        GameEvents.SaveInitiated += Save;
        Load();
    }
	
	void Update ()
    {
        if(invTime > 0)
        {
            invTime -= Time.deltaTime;
            foreach (SpriteRenderer spr in soulHeart)
            {
                spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 0.5f);
            }
        }

        foreach(SpriteRenderer spr in soulHeart)
        {
            if (invTime < 0)
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
        //bar.GetComponent<RectTransform>().sizeDelta += new Vector2(25f, 0f);
        level++;
        maxHealth += 5;
        currentHealth = maxHealth;
        float leftoverExp = currentExperience - maxExperience;
        maxExperience += 50;
        currentExperience = 0 + leftoverExp;
        
    }

    public void Damage(float amount)
    {
        if(invTime <= 0)
        {
            AudioManager.instance.Play("soul_hurt");
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                Death();
            }
            invTime = 1;
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

    void Save()
    {
        SaveLoad.Save<string>(playerName, "PlayerName");
        SaveLoad.Save<float>(maxHealth, "PlayerMaxHP");
        SaveLoad.Save<float>(currentHealth, "PlayerCurHP");
        SaveLoad.Save<float>(level, "PlayerLevel");
        SaveLoad.Save<float>(maxExperience, "PlayerMaxXP");
        SaveLoad.Save<float>(currentExperience, "PlayerCurXP");
        SaveLoad.Save<int>(menuArtNumber, "MenuVariable");
    }
    
    void Load()
    {
        if (SaveChecker.CheckSaveFile())
        {
            playerName = SaveLoad.Load<string>("PlayerName");
            maxHealth = SaveLoad.Load<float>("PlayerMaxHP");
            currentHealth = SaveLoad.Load<float>("PlayerCurHP");
            level = SaveLoad.Load<float>("PlayerLevel");
            maxExperience = SaveLoad.Load<float>("PlayerMaxXP");
            currentExperience = SaveLoad.Load<float>("PlayerCurXP");
            Vector2 tempVector;
            tempVector.x = SaveLoad.Load<float>("PlayerPositionX");
            tempVector.y = SaveLoad.Load<float>("PlayerPositiony");
            playerObjects[0].transform.position = tempVector;
        }
    }
}
