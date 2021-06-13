using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;

    public EnemyProfile enemyProf;

    public void TakeDamage(float num)
    {
        health -= num;
    }
}
