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
    Lost,
    WaitForInput,
    CleanUp
}

public enum Action
{
    None,
    Fight,
    Act,
    Spare
}

public enum SpoilsType
{
    Spared,
    Dead,
}

public class TurnHandle : MonoBehaviour
{
    public BattleState state;

    public RectTransform CanvasRect;

    public GameObject box;

    public GameObject enemyPrefab;
    public ActButtons[] actButtons;
    public List<GameObject> enemyPositions;
    public List<Button> enemyActButtons;
    public List<EnemyProfile> enemiesInBattle;
    public List<GameObject> enemiesAlive;

    private bool enemyActed;
    public Flowchart battleFlowchart;

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
    [Header("Player")]
    public Action playerAction;
    public GameObject playerBattlePanel;
    public GameObject playerMainPanel;
    public GameObject playerFightPanel;
    public Slider fightSlider;
    public GameObject playerItemPanel;
    public GameObject playerActPanel;
    public GameObject playerEnemyPanel;
    public GameObject victoryPanel;
    public List<GameObject> enemyActContainers;

    private int en = 0;

    private int enemyID;

    public bool fightSliderActive;
    public bool fightSliderDecrease;

    void Start()
    {
        state = BattleState.StandBy;
        enemyActed = false;
        playerMainPanel.GetComponentInChildren<Button>().Select();
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

                foreach (Button b in enemyActButtons)
                {
                    if (b.transform.Find("Name").GetComponent<TMP_Text>().text == "Enemy")
                    {
                        b.transform.Find("Name").GetComponent<TMP_Text>().text = e.name;
                        go.GetComponent<Enemy>().nameText = b.transform.Find("Name").GetComponent<TMP_Text>();
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
            foreach (Button b in enemyActButtons)
            {
                if (b.transform.Find("Name").GetComponent<TMP_Text>().text == "Enemy")
                {
                    b.gameObject.SetActive(false);
                }
            }
        }
        else if (state == BattleState.Start)
        {
            playerMainPanel.SetActive(true);
            playerMainPanel.GetComponentInChildren<Button>().Select();
            state = BattleState.PlayerTurn;
        }
        else if (state == BattleState.PlayerTurn)
        {
            //wait for player to attack
        }
        else if (state == BattleState.EnemyTurn)
        {
            if(CheckAliveEnemies())
            {
                //enemyFinishedTurn();
                state = BattleState.Won;
            }
            else
            {
                if(!enemyActed)
                {
                    box.SetActive(true);
                    playerHeart.gameObject.SetActive(true);
                    playerHeart.SetHeart();

                    foreach (GameObject emy in enemiesAlive)
                    {
                        if (emy == null)
                        {
                            continue;
                        }
                        else
                        {
                            int atkNumb = Random.Range(0, emy.GetComponent<Enemy>().enemyProf.enemyAttacks.Length);

                            GameObject go = Instantiate(emy.GetComponent<Enemy>().enemyProf.enemyAttacks[atkNumb], box.transform.position, Quaternion.identity) as GameObject;
                            go.transform.SetParent(box.transform, true);
                            go.transform.localScale = new Vector3(1, 1, 1);
                        }
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
            box.SetActive(false);

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
            battleFlowchart.ExecuteBlock("Win");
            state = BattleState.WaitForInput;
            Debug.Log("Battle Won");
        }
        else if (state == BattleState.Lost)
        {
            //lost
            Debug.Log("Battle Lost");
        }
        else if (state == BattleState.WaitForInput)
        {
            //Do Nothing
        }
        else if (state == BattleState.CleanUp)
        {
            CloseAll();

            for (int i = 0; i < actButtons.Length; i++)
            {
                for (int u = 0; u < actButtons[en].buttons.Length ; u++)
                {
                    actButtons[i].buttons[u].GetComponentInChildren<TMP_Text>().text = "Act";
                    actButtons[i].buttons[u].onClick.RemoveAllListeners();
                    actButtons[i].buttons[u].gameObject.SetActive(true);
                }
            }

            foreach (Button b in enemyActButtons)
            {
                b.transform.Find("Name").GetComponent<TMP_Text>().text = "Enemy";
                b.transform.Find("Name").GetComponent<TMP_Text>().color = new Color32(255, 255, 255, 255);
                b.gameObject.SetActive(true);
            }

            enemiesAlive.Clear();

            battleCam.Priority = 0;
            lastActiveCam.Priority = 1;
            playerBattlePanel.SetActive(false);

            state = BattleState.StandBy;
        }
        //======
        //Inputs
        //======
        if (!playerFightPanel.activeInHierarchy)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                if (playerItemPanel.activeInHierarchy)
                {
                    playerItemPanel.SetActive(false);
                    playerMainPanel.transform.Find("ButtonHolder").transform.Find("Item").GetComponent<Button>().Select();
                }
                else if (enemyActContainers[0].activeInHierarchy || enemyActContainers[1].activeInHierarchy || enemyActContainers[2].activeInHierarchy)
                {
                    if (enemyActContainers[0].activeInHierarchy)
                    {
                        playerEnemyPanel.transform.Find("EnemyPicker").GetChild(0).GetChild(0).GetComponent<Button>().Select();
                    }
                    else if (enemyActContainers[1].activeInHierarchy)
                    {
                        playerEnemyPanel.transform.Find("EnemyPicker").GetChild(0).GetChild(1).GetComponent<Button>().Select();
                    }
                    else if (enemyActContainers[2].activeInHierarchy)
                    {
                        playerEnemyPanel.transform.Find("EnemyPicker").GetChild(0).GetChild(2).GetComponent<Button>().Select();
                    }

                    foreach (GameObject cont in enemyActContainers)
                    {
                        cont.SetActive(false);
                    }
                    selectedEnemy = null;
                    target.SetActive(false);
                    playerActPanel.SetActive(false);
                }
                else if (playerEnemyPanel.activeInHierarchy)
                {
                    playerEnemyPanel.SetActive(false);
                    if (playerAction == Action.Fight)
                    {
                        playerMainPanel.transform.Find("ButtonHolder").transform.Find("Fight").GetComponent<Button>().Select();
                    }
                    else if (playerAction == Action.Act)
                    {
                        playerMainPanel.transform.Find("ButtonHolder").transform.Find("Act").GetComponent<Button>().Select();
                    }
                    else if (playerAction == Action.Spare)
                    {
                        playerMainPanel.transform.Find("ButtonHolder").transform.Find("Spare").GetComponent<Button>().Select();
                    }
                    playerAction = Action.None;
                }
            }
        }
        //==============
        //Other Funcions
        //==============
        if (fightSliderActive)
        {
            if (fightSliderDecrease)
            {
                fightSlider.value -= Time.deltaTime * 10;
            }
            else
            {
                fightSlider.value += Time.deltaTime * 10;
            }
        }

        if (fightSlider.value == fightSlider.maxValue)
        {
            fightSliderDecrease = true;
        }
        if(fightSliderDecrease && fightSlider.value == fightSlider.minValue)
        {
            showValue(0, ValuePopup.Type.Miss);
            CloseAll();
            playerFinishTurn();
        }
    }
    public void CloseAll()
    {
        playerFightPanel.SetActive(false);
        playerActPanel.SetActive(false);
        playerItemPanel.SetActive(false);
        playerEnemyPanel.SetActive(false);
        victoryPanel.SetActive(false);
        foreach (GameObject cont in enemyActContainers)
        {
            cont.SetActive(false);
        }
        GameObject.Find("ResetSelection").GetComponent<Selectable>().Select();
    }

    public void AddEnemy(EnemyProfile enemy)
    {
        enemiesInBattle.Add(enemy);
    }
    //==============
    //Player Actions
    //==============
    public void PlayerFight()
    {
        playerAction = Action.Fight;
        playerEnemyPanel.SetActive(true);
        playerEnemyPanel.GetComponentInChildren<RectTransform>().gameObject.GetComponentInChildren<Button>().Select();
    }

    public void PlayerAct()
    {
        playerAction = Action.Act;
        playerEnemyPanel.SetActive(true);
        playerEnemyPanel.GetComponentInChildren<RectTransform>().gameObject.GetComponentInChildren<Button>().Select();
    }

    public void SelectEnemy(int id)
    {
        selectedEnemy = enemiesAlive[id].GetComponent<Enemy>();
        enemyID = id;
        Target();
        if(playerAction == Action.Fight)
        {
            playerFightPanel.SetActive(true);
            playerFightPanel.GetComponentInChildren<RectTransform>().gameObject.GetComponentInChildren<Button>().Select();
            fightSliderActive = true;
        }
        else if (playerAction == Action.Act)
        {
            playerActPanel.SetActive(true);
            enemyActContainers[id].SetActive(true);
            enemyActContainers[id].GetComponentInChildren<RectTransform>().gameObject.GetComponentInChildren<Button>().Select();
        }
        else if (playerAction == Action.Spare)
        {
            battleFlowchart.SetStringVariable("Name", selectedEnemy.GetComponent<Enemy>().enemyProf.name);
            battleFlowchart.SetBooleanVariable("spareable", selectedEnemy.GetComponent<Enemy>().spareable);
            battleFlowchart.ExecuteBlock("Spare");
            CloseAll();
        }
    }

    public void PlayerItem()
    {
        playerItemPanel.SetActive(true);
        playerItemPanel.GetComponentInChildren<RectTransform>().gameObject.GetComponentInChildren<Button>().Select();
    }

    public void PlayerUseItem()
    {
        if(GameObject.Find("GameManager").GetComponent<ItemInspect>().checkUse())
        {
            
        }
        else
        {
            playerItemPanel.SetActive(false);
            GameObject.Find("GameManager").GetComponent<ItemInspect>().UseBattleItem();
        }
    }

    public void PlayerSpare()
    {
        playerAction = Action.Spare;
        playerEnemyPanel.SetActive(true);
        playerEnemyPanel.GetComponentInChildren<RectTransform>().gameObject.GetComponentInChildren<Button>().Select();
    }

    public void EndBattle()
    {
        StartCoroutine(ReturnOverworld());
        GameObject.Find("ResetSelection").GetComponent<Selectable>().Select();
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
        selectedEnemy.updateColor();
    }

    public void dealDamage(float amount)
    {
        selectedEnemy.curHealth -= amount;
        showValue(amount, ValuePopup.Type.Damage);

        if(selectedEnemy.curHealth <= 0)
        {
            selectedEnemy.curHealth = 0;
            giveSpoils(SpoilsType.Dead);
            selectedEnemy.removeEnemy("DEAD");
        }
    }

    public void stopGaugePower()
    {
        fightSliderActive = false;
        if(fightSlider.value >= 9.7f)
        {
            float atkPower = playerStats.attackValue * 2;
            dealDamage(atkPower);
        }
        else if(fightSlider.value <= 9.7f && fightSlider.value >= 5f)
        {
            float atkPower = playerStats.attackValue;
            dealDamage(atkPower);
        }
        else if (fightSlider.value <= 5f)
        {
            float atkPower = playerStats.attackValue * 0.5f;
            dealDamage(atkPower);
        }
        StartCoroutine(FightPause(1));
    }

    IEnumerator FightPause(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        CloseAll();
        playerFinishTurn();
    }

    public void spareEnemy()
    {
        selectedEnemy.removeEnemy("SPARED");
        giveSpoils(SpoilsType.Spared);
        enemyActButtons[enemyID].interactable = false;
    }

    public void playerFinishTurn()
    {
        playerMainPanel.SetActive(false);
        target.SetActive(false);
        GameObject.Find("ResetSelection").GetComponent<Selectable>().Select();
        state = BattleState.EnemyTurn;
        playerAction = Action.None;
        fightSlider.value = fightSlider.minValue;
        fightSliderActive = false;
        fightSliderDecrease = false;
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
        battleFlowchart.ExecuteBlock(blockName);
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

    public void giveSpoils(SpoilsType type)
    {
        if(type == SpoilsType.Dead)
        {
            battleFlowchart.SetFloatVariable("temp_money", selectedEnemy.enemyProf.money);
            battleFlowchart.SetFloatVariable("temp_exp", selectedEnemy.enemyProf.exp);
        }
        else if (type == SpoilsType.Spared)
        {
            battleFlowchart.SetFloatVariable("temp_money", selectedEnemy.enemyProf.money);
            battleFlowchart.SetFloatVariable("temp_exp", 0);
        }
        battleFlowchart.ExecuteBlock("AddVariables");
    }

    IEnumerator ReturnOverworld()
    {
        GameObject.Find("Canvas").GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.5f);
        state = BattleState.CleanUp;
        yield return new WaitForSeconds(0.5f);
        GameObject.Find("Canvas").GetComponent<Animator>().SetTrigger("FadeIn");
        GameObject.Find("GameManager").GetComponent<PlayerController>().playerObject.GetComponent<Interaction>().InteractionSwitch();
    }

    public bool CheckAliveEnemies()
    {
        foreach (GameObject en in enemiesAlive)
        {
            if(en == null)
            {
                continue;
            }
            else
            {
                return false;
            }
        }
        return true;
    }
}

[System.Serializable]
public class ActButtons
{
    public Button[] buttons;
}
