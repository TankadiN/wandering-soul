using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

    public GameObject enemyPrefab;
    public List<GameObject> enemyPositions;

    public List<EnemyProfile> enemiesInBattle;
    public List<GameObject> enemiesAlive;
    private bool enemyActed;
    private GameObject[] enemyAtks;

    public PlayerMovement playerHeart;
    public PlayerController playerStats;
    [Header("Player UI")]
    public GameObject playerUi;
    public GameObject battleInventory;

    void Start()
    {
        state = BattleState.StandBy;
        enemyActed = false;
        playerUi.GetComponentInChildren<Button>().Select();
    }


    void Update()
    {
        if (state == BattleState.StandBy)
        {
            //no battle at this moment
        }
        else if (state == BattleState.Initialize)
        {
            foreach(EnemyProfile e in enemiesInBattle)
            {
                GameObject go = Instantiate(enemyPrefab) as GameObject;
                foreach(GameObject o in enemyPositions)
                {
                    if(o.transform.childCount <= 0)
                    {
                        go.transform.SetParent(o.transform, true);
                        go.transform.position = o.transform.position;
                        break;
                    }
                }
                go.name = e.name;
                go.GetComponent<Enemy>().enemyProf = e;
                go.GetComponent<SpriteRenderer>().sprite = e.enemyVisual;
                go.GetComponent<Enemy>().health = e.hp;
                enemiesAlive.Add(go);
            }
            state = BattleState.Start;
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

                    foreach (GameObject emy in enemiesAlive)
                    {
                        int atkNumb = Random.Range(0, emy.GetComponent<Enemy>().enemyProf.enemyAttacks.Length);

                        GameObject go = Instantiate(emy.GetComponent<Enemy>().enemyProf.enemyAttacks[atkNumb], box.transform.position, Quaternion.identity) as GameObject;
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

            if(playerStats.currentHealth <= 0)
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

        if(Input.GetButtonDown("Cancel"))
        {
            if(battleInventory.activeInHierarchy)
            {
                battleInventory.SetActive(false);
                playerUi.transform.Find("Item").GetComponent<Button>().Select();
            }
        }

    }

    public void AddEnemy(EnemyProfile enemy)
    {
        enemiesInBattle.Add(enemy);
    }
    //===========Player Actions===============
    public void PlayerAct()
    {
        playerFinishTurn();
    }

    public void PlayerItem()
    {
        battleInventory.SetActive(true);
        battleInventory.GetComponentInChildren<RectTransform>().gameObject.GetComponentInChildren<Button>().Select();
    }
    public void PlayerUseItem()
    {
        if(GameObject.Find("GameManager").GetComponent<ItemInspect>().checkUse())
        {
            
        }
        else
        {
            battleInventory.SetActive(false);
            GameObject.Find("GameManager").GetComponent<ItemInspect>().UseBattleItem();
        }
    }
    //===========Player Actions===============
    public void TestInitialize()
    {
        state = BattleState.Initialize;
    }

    public void playerFinishTurn()
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
