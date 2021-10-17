using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public float maxHealth;

    public float curHealth;

    public EnemyProfile enemyProf;

    public float mercy;

    public bool spareable;

    public TMP_Text hpPerc;
    public Image hpBar;
    public TMP_Text mercyPerc;
    public Image mercyBar;

    public void Start()
    {
        curHealth = maxHealth;
    }
    public void Update()
    {
        if(mercy >= 100)
        {
            mercy = 100;
            spareable = true;
        }

        if (hpPerc)
        {
            float calcHP = curHealth / maxHealth * 100f;
            hpPerc.text = calcHP + "%";
        }
        if (mercyPerc)
        {
            mercyPerc.text = mercy + "%";
        }
        if (hpBar)
        {
            hpBar.fillAmount = curHealth / maxHealth;
        }
        if (mercyBar)
        {
            mercyBar.fillAmount = mercy / 100;
        }
    }
}
