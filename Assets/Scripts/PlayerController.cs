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
    [Header("Health Variables")]
    public float maxHealth;
    public float currentHealth;
    [Header("Level Variables")]
    public float level;
    public float maxExperience;
    public float currentExperience;

    void Start ()
    {
        currentHealth = maxHealth;
	}
	
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            currentHealth--;
        }
        if (Input.GetKey(KeyCode.E))
        {
            currentExperience++;
        }
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

        hpTextbox.text = sCurHp + "/" + sMaxHp;
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
        maxHealth += 4f;
        currentHealth = maxHealth;
        maxExperience += 50f;
        currentExperience = 0f;
    }
}
