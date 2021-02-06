using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnHandle : MonoBehaviour
{
    public bool finishedTurn;

    public int attackAmounts;

    private void Start()
    {
        finishedTurn = false;

        int atkNumb = Random.Range(0, attackAmounts);
        GetComponent<Animator>().SetInteger("AtkDex", atkNumb);
    }

    public void AtkDone()
    {
        finishedTurn = true;
    }
}
