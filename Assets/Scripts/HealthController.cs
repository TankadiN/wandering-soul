using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthController : MonoBehaviour {

    [Header("Game Objects")]
    public Slider bar;
    public TMP_Text hpTextbox;
    public TMP_Text lvlTextbox;
    [Header("Health Variables")]
    public float maxHealth;
    public float currentHealth;
    [Header("Level Variables")]
    public float level;

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

        if (Input.GetKeyDown(KeyCode.E))
        {
            bar.GetComponent<RectTransform>().sizeDelta += new Vector2(25f, 0f);
            level++;
            maxHealth += 4f;
        }
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        // Health Display Logic
        string sMax = maxHealth.ToString();
        string sCur = currentHealth.ToString("0");
        string sLvl = maxHealth.ToString();

        hpTextbox.text = sCur + "/" + sMax;
        lvlTextbox.text = "lv " + level;

        bar.maxValue = maxHealth;
        bar.value = currentHealth;
    }
}
