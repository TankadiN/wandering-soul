using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Fungus;

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
    //public GameObject actButtonPrefab;
    public ActButtons[] actButtons;
    public List<GameObject> enemyPositions;
    public List<Button> enemyButtons;

    public Flowchart enemyActFlowchart;

    public List<EnemyProfile> enemiesInBattle;
    public List<GameObject> enemiesAlive;
    private bool enemyActed;
    private GameObject[] enemyAtks;


    public PlayerMovement playerHeart;
    public PlayerController playerStats;
    [Header("Player UI")]
    public GameObject playerUi;
    public GameObject battleInventory;
    public GameObject playerActPanel;
    public List<GameObject> enemyActContainers;

    private int en = 0;

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
            en = 0;
            foreach(EnemyProfile e in enemiesInBattle)
            {
                GameObject go = Instantiate(enemyPrefab) as GameObject;
                //GameObject flow = Instantiate(e.flowchart) as GameObject;
                //GameObject act = Instantiate(actButtonPrefab) as GameObject;
                foreach (GameObject o in enemyPositions)
                {
                    if(o.transform.childCount <= 0)
                    {
                        go.transform.SetParent(o.transform, true);
                        go.transform.position = o.transform.position;
                        break;
                    }
                }
                go.name = e.name;
                //flow.name = e.name;
                //flow.transform.SetParent(go.transform, true);
                go.GetComponent<Enemy>().enemyProf = e;
                go.GetComponent<SpriteRenderer>().sprite = e.enemyVisual;
                go.GetComponent<Enemy>().health = e.hp;
                enemiesAlive.Add(go);

                for(int i = 0; i < e.actions.Length; i++)
                {
                    if(i >= e.actions.Length)
                    {
                        actButtons[en].buttons[i].gameObject.SetActive(false);
                    }
                    string action = e.name + "/" + e.actions[i];
                    actButtons[en].buttons[i].GetComponentInChildren<TMP_Text>().text = e.actions[i];
                    actButtons[en].buttons[i].onClick.AddListener(delegate { actButton(action); });
                }

                foreach (Button b in enemyButtons)
                {
                    if (b.GetComponentInChildren<TMP_Text>().text == "Enemy")
                    {
                        b.GetComponentInChildren<TMP_Text>().text = e.name;
                        break;
                    }
                }
                en++;
            }
            enemiesInBattle.Clear();
            state = BattleState.Start;
            en = 0;
            foreach (Button b in enemyButtons)
            {
                if (b.GetComponentInChildren<TMP_Text>().text == "Enemy")
                {
                    b.gameObject.SetActive(false);
                }
            }

            /*foreach (ActButtons actB in actButtons)
            {
                foreach(Button b in actB.buttons)
                {
                    if (b.GetComponentInChildren<TMP_Text>().text == "Act")
                    {
                        b.gameObject.SetActive(false);
                    }
                }
            }*/
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
            if(enemiesAlive.Count <= 0)
            {
                //enemyFinishedTurn();
                state = BattleState.Won;
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
            Debug.Log("Battle Won");
        }
        else if (state == BattleState.Lost)
        {
            //lost
            Debug.Log("Battle Lost");
        }
        //======
        //Inputs
        //======
        if(Input.GetButtonDown("Cancel"))
        {
            if(battleInventory.activeInHierarchy)
            {
                battleInventory.SetActive(false);
                playerUi.transform.Find("Item").GetComponent<Button>().Select();
            }
            else if(playerActPanel.activeInHierarchy)
            {
                playerActPanel.SetActive(false);
                playerUi.transform.Find("Act").GetComponent<Button>().Select();
            }
            else if(enemyActContainers[0].activeInHierarchy || enemyActContainers[1].activeInHierarchy || enemyActContainers[2].activeInHierarchy)
            {
                foreach(GameObject cont in enemyActContainers)
                {
                    cont.SetActive(false);
                }

                if(enemyActContainers[0].activeInHierarchy)
                {
                    playerUi.transform.Find("Enemy0").GetComponent<Button>().Select();
                }
                else if (enemyActContainers[1].activeInHierarchy)
                {
                    playerUi.transform.Find("Enemy1").GetComponent<Button>().Select();
                }
                else if (enemyActContainers[2].activeInHierarchy)
                {
                    playerUi.transform.Find("Enemy2").GetComponent<Button>().Select();
                }
            }
        }

    }
    public void CloseAll()
    {
        battleInventory.SetActive(false);
        playerActPanel.SetActive(false);
        foreach (GameObject cont in enemyActContainers)
        {
            cont.SetActive(false);
        }
    }

    public void AddEnemy(EnemyProfile enemy)
    {
        enemiesInBattle.Add(enemy);
    }
    //===========Player Actions===============
    public void PlayerAct()
    {
        playerActPanel.SetActive(true);
        playerActPanel.GetComponentInChildren<RectTransform>().gameObject.GetComponentInChildren<Button>().Select();
        //playerFinishTurn();
    }
    public void SelectEnemy(int id)
    {
        enemyActContainers[id].SetActive(true);
        enemyActContainers[id].GetComponentInChildren<RectTransform>().gameObject.GetComponentInChildren<Button>().Select();
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

    public void actButton(string blockName)
    {
        enemyActFlowchart.ExecuteBlock(blockName);
        CloseAll();
    }
}

[System.Serializable]
public class ActButtons
{
    public Button[] buttons;
}
