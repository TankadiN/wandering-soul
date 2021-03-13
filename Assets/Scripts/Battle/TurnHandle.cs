using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState
{
    StandBy,
    Initialize,
    Start,
    PlayerTurn,
    EnemyTurn,
    FinishedTurn,
    Won,
    Lost
}

public class TurnHandle : MonoBehaviour
{
    public BattleState state;

    public GameObject box;

    public List<EnemyProfile> enemiesInBattle;
    public List<GameObject> enemiesAlive;
    private bool enemyActed;
    private GameObject[] enemyAtks;

    public GameObject playerUi;
    public PlayerMovement playerHeart;
    public PlayerController playerStats;
    
    void Start()
    {
        state = BattleState.Start;
        enemyActed = false;
    }


    void Update()
    {
        if (state == BattleState.StandBy)
        {
            
        }
        else if (state == BattleState.Initialize)
        {

        }
        else if (state == BattleState.Start)
        {
            playerUi.SetActive(true);
            playerUi.GetComponentInChildren<Button>().Select();
            state = BattleState.PlayerTurn;
        }
        else if(state == BattleState.PlayerTurn)
        {
            //wait for player to attack
        }
        else if (state == BattleState.EnemyTurn)
        {
            if(enemiesInBattle.Count <= 0)
            {
                enemyFinishedTurn();
            }
            else
            {
                if(!enemyActed)
                {
                    playerHeart.gameObject.SetActive(true);
                    playerHeart.SetHeart();

                    foreach (EnemyProfile emy in enemiesInBattle)
                    {
                        int atkNumb = Random.Range(0, emy.enemyAttacks.Length);

                        GameObject go = Instantiate(emy.enemyAttacks[atkNumb], box.transform.position, Quaternion.identity) as GameObject;
                        go.transform.SetParent(box.transform, true);
                        go.transform.localScale = new Vector3(1, 1, 1);
                    }

                    enemyAtks = GameObject.FindGameObjectsWithTag("Enemy");

                    enemyActed = true;
                }
                else
                {
                    bool enemyFin = true;

                    foreach (GameObject emy in enemyAtks)
                    {
                        if (!emy.GetComponent<EnemyTurnHandle>().finishedTurn)
                        {
                            enemyFin = false;
                        }
                    }

                    if(enemyFin)
                    {
                        enemyFinishedTurn();
                    }
                }
            }
        }
        else if (state == BattleState.FinishedTurn)
        {
            playerHeart.gameObject.SetActive(false);

            if(playerStats.currentHealth < 0)
            {
                state = BattleState.Lost;
            }
            else
            {
                state = BattleState.Start;
            }
        }
        else if (state == BattleState.Won)
        {
            //won
        }
        else if (state == BattleState.Lost)
        {
            //lost
        }
    }

    public void AddEnemy(EnemyProfile enemy)
    {
        enemiesInBattle.Add(enemy);
    }



    public void PlayerAct()
    {
        playerFinishTurn();
    }

    void playerFinishTurn()
    {
        playerUi.SetActive(false);
        GameObject.Find("ResetSelection").GetComponent<Selectable>().Select();
        state = BattleState.EnemyTurn;
    }

    void enemyFinishedTurn()
    {
        foreach (GameObject obj in enemyAtks)
        {
            Destroy(obj);
        }

        enemyActed = false;
        state = BattleState.FinishedTurn;
    }
}
