using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour {

    [Header("Game Objects")]
    public Slider[] bar;
    public Image soul;
    public TMP_Text[] nameTextbox;
    public TMP_Text[] hpTextbox;
    public TMP_Text[] lvlTextbox;
    public TMP_Text[] expTextbox;
    [Header("Player Variables")]
    public string playerName;
    public int menuArtNumber;
    [Header("Health Variables")]
    public float maxHealth;
    public float currentHealth;
    [Header("Level Variables")]
    public float level;
    public float maxExperience;
    public float currentExperience;
    [Header("Color Control")]
    public SpriteRenderer outlinePlayer;
    public SpriteRenderer soulPlayer;

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
        if (currentExperience >= maxExperience)
        {
            LevelUp();
        }
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        if(currentHealth <= 0)
        {
            
        }
        //Display Logic
        string sMaxHp = maxHealth.ToString();
        string sCurHp = currentHealth.ToString();
        string sLvl = maxHealth.ToString();
        string sMaxExp = maxExperience.ToString();
        string sCurExp = currentExperience.ToString();

        for(int i = 0; i < bar.Length; i++)
        {
            nameTextbox[i].text = playerName;
            hpTextbox[i].text = "hp " + sCurHp + "/" + sMaxHp;
            lvlTextbox[i].text = "lv " + level;
            expTextbox[i].text = "exp " + sCurExp + "/" + sMaxExp;

            bar[i].maxValue = maxHealth;
            bar[i].value = currentHealth;
        }

        soul.fillAmount = currentExperience / maxExperience;
    }

    public void LevelUp()
    {
        AudioManager.instance.Play("levelup");
        //bar.GetComponent<RectTransform>().sizeDelta += new Vector2(25f, 0f);
        level++;
        maxHealth += 4;
        currentHealth = maxHealth;
        float leftoverExp = currentExperience - maxExperience;
        maxExperience += 50;
        currentExperience = 0 + leftoverExp;
        
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Debug.Log("Player died");
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
            gameObject.transform.position = tempVector;
        }
    }
}
