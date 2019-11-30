using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour {

    [Header("Game Objects")]
    public Slider bar;
    public Image soul;
    public TMP_Text hpTextbox;
    public TMP_Text lvlTextbox;
    public TMP_Text expTextbox;
    [Header("Inventory")]
    public InventoryObject inventory;
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
        soulPlayer.color = new Color(outlinePlayer.color.r, outlinePlayer.color.g, outlinePlayer.color.b, 0);
        AudioManager.instance.Play("waterfall");
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
            Destroy(gameObject);
        }
        //Display Logic
        string sMaxHp = maxHealth.ToString();
        string sCurHp = currentHealth.ToString();
        string sLvl = maxHealth.ToString();
        string sMaxExp = maxExperience.ToString();
        string sCurExp = currentExperience.ToString();

        hpTextbox.text = "hp " + sCurHp + "/" + sMaxHp;
        lvlTextbox.text = "lv " + level;
        expTextbox.text = "exp " + sCurExp + "/" + sMaxExp;

        bar.maxValue = maxHealth;
        bar.value = currentHealth;
        soul.fillAmount = currentExperience / maxExperience;
    }

    public void LevelUp()
    {
        AudioManager.instance.Play("levelup");
        bar.GetComponent<RectTransform>().sizeDelta += new Vector2(25f, 0f);
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
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
    }

    public void GiveExp(float amount)
    {
        currentExperience += amount;
    }

    public void PickupItem()
    {
        ItemObject item = GetComponent<Interaction>().item;
        if(item)
        {
            inventory.AddItem(item);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
