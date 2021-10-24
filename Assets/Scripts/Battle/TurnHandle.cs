using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Fungus;
using Cinemachine;

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

    public RectTransform CanvasRect;

    public GameObject box;

    public GameObject enemyPrefab;
    public ActButtons[] actButtons;
    public List<GameObject> enemyPositions;
    public List<Button> enemyButtons;
    public List<EnemyProfile> enemiesInBattle;
    public List<GameObject> enemiesAlive;

    private bool enemyActed;
    public Flowchart enemyActFlowchart;

    private GameObject[] enemyAtks;
    public PlayerMovement playerHeart;
    public PlayerController playerStats;

    public Enemy selectedEnemy;
    public GameObject target;
    [Header("Camera")]
    public GameObject virtCameraCont;
    public CinemachineVirtualCamera battleCam;
    private CinemachineVirtualCamera lastActiveCam;
    private CinemachineVirtualCamera[] cams;
    [Header("Player UI")]
    public GameObject playerBattlePanel;
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
        cams = virtCameraCont.GetComponentsInChildren<CinemachineVirtualCamera>();
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
                go.GetComponent<Enemy>().enemyProf = e;
                go.GetComponent<SpriteRenderer>().sprite = e.enemyVisual;
                go.GetComponent<Enemy>().maxHealth = e.hp;
                enemiesAlive.Add(go);

                for(int i = 0; i < actButtons[en].buttons.Length; i++)
                {
                    if(i >= e.actions.Length)
                    {
                        actButtons[en].buttons[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        string action = e.name + "/" + e.actions[i];
                        actButtons[en].buttons[i].GetComponentInChildren<TMP_Text>().text = e.actions[i];
                        actButtons[en].buttons[i].onClick.AddListener(delegate { actButton(action); });
                    }
                }

                foreach (Button b in enemyButtons)
                {
                    if (b.transform.Find("Name").GetComponent<TMP_Text>().text == "Enemy")
                    {
                        b.transform.Find("Name").GetComponent<TMP_Text>().text = e.name;
                        go.GetComponent<Enemy>().hpPerc = b.transform.Find("Hp_Perc").GetComponent<TMP_Text>();
                        go.GetComponent<Enemy>().mercyPerc = b.transform.Find("Mercy_Perc").GetComponent<TMP_Text>();
                        go.GetComponent<Enemy>().hpBar = b.transform.Find("HpBar").transform.Find("HpFill").GetComponent<Image>();
                        go.GetComponent<Enemy>().mercyBar = b.transform.Find("MercyBar").transform.Find("MercyFill").GetComponent<Image>();
                        break;
                    }
                }
                en++;
            }
            enemiesInBattle.Clear();
            
            foreach (CinemachineVirtualCamera cam in cams)
            {
                if (cam.Priority == 1)
                {
                    lastActiveCam = cam;
                }
                cam.Priority = 0;
            }

            playerBattlePanel.SetActive(true);
            battleCam.Priority = 1;

            state = BattleState.Start;
            en = 0;
            foreach (Button b in enemyButtons)
            {
                if (b.transform.Find("Name").GetComponent<TMP_Text>().text == "Enemy")
                {
                    b.gameObject.SetActive(false);
                }
            }
        }
        else if (state == BattleState.Start)
        {
            playerUi.SetActive(true);
            playerUi.GetComponentInChildren<Button>().Select();
            state = BattleState.PlayerTurn;
        }
        else if (state == BattleState.PlayerTurn)
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
            else if(enemyActContainers[0].activeInHierarchy || enemyActContainers[1].activeInHierarchy || enemyActContainers[2].activeInHierarchy)
            {
                if(enemyActContainers[0].activeInHierarchy)
                {
                    playerActPanel.transform.Find("EnemyPicker").GetChild(0).GetChild(0).GetComponent<Button>().Select();
                }
                else if (enemyActContainers[1].activeInHierarchy)
                {
                    playerActPanel.transform.Find("EnemyPicker").GetChild(0).GetChild(1).GetComponent<Button>().Select();
                }
                else if (enemyActContainers[2].activeInHierarchy)
                {
                    playerActPanel.transform.Find("EnemyPicker").GetChild(0).GetChild(2).GetComponent<Button>().Select();
                }

                foreach(GameObject cont in enemyActContainers)
                {
                    cont.SetActive(false);
                }
                selectedEnemy = null;
                target.SetActive(false);
            }
            else if(playerActPanel.activeInHierarchy)
            {
                playerActPanel.SetActive(false);
                playerUi.transform.Find("Act").GetComponent<Button>().Select();
            }
        }
        //==============
        //Other Funcions
        //==============
        /*if(selectedEnemy)
        {
            target.SetActive(true);

            Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(selectedEnemy.gameObject.transform.position);
            Vector2 WorldObject_ScreenPosition = new Vector2(
            (ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f),
            (ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f));

            target.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;
        }*/

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
    //==============
    //Player Actions
    //==============
    public void PlayerAct()
    {
        playerActPanel.SetActive(true);
        playerActPanel.GetComponentInChildren<RectTransform>().gameObject.GetComponentInChildren<Button>().Select();
        //playerFinishTurn();
    }

    public void SelectEnemy(int id)
    {
        selectedEnemy = enemiesAlive[id].GetComponent<Enemy>();
        Target();
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
    //===============
    //Other Functions
    //===============
    public void startInitialize()
    {
        state = BattleState.Initialize;
    }

    public void addMercy(float amount)
    {
        selectedEnemy.mercy += amount;
        showValue(amount, ValuePopup.Type.Mercy);
    }

    public void dealDamage(float amount)
    {
        selectedEnemy.curHealth -= amount;
        showValue(amount, ValuePopup.Type.Damage);
    }

    public void playerFinishTurn()
    {
        playerUi.SetActive(false);
        target.SetActive(false);
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

    public void Target()
    {
        target.SetActive(true);

        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(selectedEnemy.gameObject.transform.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        (ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f),
        (ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f));

        target.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;
    }

    public void showValue(float amount, ValuePopup.Type type)
    {
        ValuePopup.Create(selectedEnemy.transform.position, amount, type);
    }
}

[System.Serializable]
public class ActButtons
{
    public Button[] buttons;
}
