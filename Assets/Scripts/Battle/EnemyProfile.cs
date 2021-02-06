using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyProfile : ScriptableObject
{
    public float hp;

    public Sprite enemyVisual;

    public GameObject[] enemyAttacks;
}
