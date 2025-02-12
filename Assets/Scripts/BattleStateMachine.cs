using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class BattleStateMachine : MonoBehaviour
{
    [SerializeField] private SceneSO sceneSO;
    private string prevScene;

    private bool canGo;
    private bool assessUse;
    private int turnChoice;
    private int spellChoice;
    private PlayerID[] partyIDArray;
    public GameObject[] heroUIBars;
    public TextMeshProUGUI[] heroNames;
    public TextMeshProUGUI[] heroDisplayHP;
    public TextMeshProUGUI[] heroDisplayMP;
    public TextMeshProUGUI[] heroDisplayMaxHP;
    public TextMeshProUGUI[] heroDisplayAction;

    private bool missed;
    private bool turnTaken;

    public GameObject fireball;
    public GameObject iceball;
    public GameObject lightningball;

    public GameObject[] enemyNameBars;
    public TextMeshProUGUI[] enemyNames;
    public GameObject enemyNameBar;
    public TextMeshProUGUI enemyName1;
    public GameObject enemyNameBar2;
    public TextMeshProUGUI enemyName2;
    public GameObject enemyNameBar3;
    public TextMeshProUGUI enemyName3;
    public GameObject enemyNameBar4;
    public TextMeshProUGUI enemyName4;

    public GameObject heroUIBar1;
    public TextMeshProUGUI heroName1;
    public TextMeshProUGUI heroHP1;
    public TextMeshProUGUI heroMaxHP1;
    public TextMeshProUGUI heroMP1;
    public TextMeshProUGUI heroAction1;

    private GameObject spellThatExists;
    private Vector3 goalPosition;

    public GameObject heroUIBar2;
    public TextMeshProUGUI heroName2;
    public TextMeshProUGUI heroHP2;
    public TextMeshProUGUI heroMaxHP2;
    public TextMeshProUGUI heroMP2;
    public TextMeshProUGUI heroAction2;

    public GameObject heroUIBar3;
    public TextMeshProUGUI heroName3;
    public TextMeshProUGUI heroHP3;
    public TextMeshProUGUI heroMaxHP3;
    public TextMeshProUGUI heroMP3;
    public TextMeshProUGUI heroAction3;

    public GameObject heroUIBar4;
    public TextMeshProUGUI heroName4;
    public TextMeshProUGUI heroHP4;
    public TextMeshProUGUI heroMaxHP4;
    public TextMeshProUGUI heroMP4;
    public TextMeshProUGUI heroAction4;

    private string actionTaken;


    private int encounterExp;

    private GameObject[] enemies;

    private Player curPlayer;
    private Enemy curEnemy;
    private GameObject curTarget;
    private Enemy curTargetData;
    private Player playerTargetData;
    private int tempIteration;
    public DamagePopUp DamagePopUp;

    public enum PerformAction
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION
    }
    public PerformAction battleStates;

    public List<HandleTurns> PerformList = new List<HandleTurns>();
    public List<GameObject> HeroBattleList = new List<GameObject>();
    public List<GameObject> EnemyBattleList = new List<GameObject>();
    public GameObject[] InitialEnemyBattleList;

    public Spells fireData;
    public Spells firaData;
    public Spells blizzardData;
    public Spells blizzaraData;
    public Spells thunderData;
    public Spells thundaraData;
    public Spells cureData;
    public Spells assessData;

    private Spells curSpell;


    public enum HeroGUI
    {
        ACTIVATE,
        WAITING,
        SELECTACTION,
        MAGIC,
        SPELLS,
        ITEMS,
        TARGET,
        MAGICTARGET,
        DESTROYTARGET,
        MOVESPELL,
        FLEE,
        DONE,
        GAMEEND
    }

    public enum Action
    {
        ATTACK,
        MAGIC,
        DEFEND,
        ITEMS,
        ROW,
        FLEE
    }

    public HeroGUI HeroInput;
    public Action HeroAction;

    public List<GameObject> HeroesToManage = new List<GameObject>();
    public List<GameObject> Spells = new List<GameObject>();
    public List<GameObject> EnemiesToManage = new List<GameObject>();
    private HandleTurns HeroChoice;

    public AudioClip dmgSFX;
    public AudioClip healSFX;
    public AudioClip missSFX;

    public GameObject ActionPanel;
    public TextMeshProUGUI AttackText;
    public TextMeshProUGUI MagicText;
    public TextMeshProUGUI DefendText;
    public TextMeshProUGUI ItemsText;
    public GameObject ActionPanel2;
    public TextMeshProUGUI RowText;
    public TextMeshProUGUI FleeText;
    public GameObject HeroPanel;
    public GameObject EnemyPanel;
    public GameObject MagicPanel;
    public GameObject ItemPanel;
    public GameObject DescPanel;
    public GameObject UserActionPanel;
    public TextMeshProUGUI userActionName;
    public TextMeshProUGUI userActionMP;
    public GameObject FlavorPanel;
    public TextMeshProUGUI UserActionName;
    public TextMeshProUGUI UserActionTaken;
    public TextMeshProUGUI UserActionMP;
    public GameObject ActionPointer;
    public GameObject ActionPointer2;
    public GameObject DamagePanel;

    public TextMeshProUGUI Fire1Text;
    public TextMeshProUGUI Fire2Text;
    public TextMeshProUGUI Ice1Text;
    public TextMeshProUGUI Ice2Text;
    public TextMeshProUGUI Zap1Text;
    public TextMeshProUGUI Zap2Text;
    public TextMeshProUGUI CureText;
    public TextMeshProUGUI AssessText;
    public TextMeshProUGUI DescText;
    private TextMeshProUGUI SpellDesc;
    public TextMeshProUGUI FlavorText;
    private string tempFlavor;

    private AudioSource audio;

    private Vector3 firstPos;
    private Vector3 secondPos;

    private Vector3 targetFirstPos;
    private Vector3 targetSecondPos;

    private Vector3 enemyFirstPos;
    private Vector3 enemySecondPos;

    private int atkDmg;
    private int damageRating;

    private bool firstRun;
    private bool enemyMoveRun;
    private bool deadCounterRan;

    static Random rnd = new Random();

    void Start()
    {
        //ARRAY TIME!!!
        heroNames = new TextMeshProUGUI[4];
        heroNames[0] = heroName1;
        heroNames[1] = heroName2;
        heroNames[2] = heroName3;
        heroNames[3] = heroName4;
        heroDisplayHP = new TextMeshProUGUI[4];
        heroDisplayHP[0] = heroHP1;
        heroDisplayHP[1] = heroHP2;
        heroDisplayHP[2] = heroHP3;
        heroDisplayHP[3] = heroHP4;
        heroDisplayMaxHP = new TextMeshProUGUI[4];
        heroDisplayMaxHP[0] = heroMaxHP1;
        heroDisplayMaxHP[1] = heroMaxHP2;
        heroDisplayMaxHP[2] = heroMaxHP3;
        heroDisplayMaxHP[3] = heroMaxHP4;
        heroDisplayMP = new TextMeshProUGUI[4];
        heroDisplayMP[0] = heroMP1;
        heroDisplayMP[1] = heroMP2;
        heroDisplayMP[2] = heroMP3;
        heroDisplayMP[3] = heroMP4;
        heroDisplayAction = new TextMeshProUGUI[4];
        heroDisplayAction[0] = heroAction1;
        heroDisplayAction[1] = heroAction2;
        heroDisplayAction[2] = heroAction3;
        heroDisplayAction[3] = heroAction4;
        enemyNameBars = new GameObject[4];
        enemyNameBars[0] = enemyNameBar;
        enemyNameBars[1] = enemyNameBar2;
        enemyNameBars[2] = enemyNameBar3;
        enemyNameBars[3] = enemyNameBar4;
        enemyNames = new TextMeshProUGUI[4];
        enemyNames[0] = enemyName1;
        enemyNames[1] = enemyName2;
        enemyNames[2] = enemyName3;
        enemyNames[3] = enemyName4;
    }

    private int heroIterator;
    private int enemyIterator;
    private int heroNum;

    void OnEnable()
    {
        actionTaken = "";
        SceneManager.sceneLoaded += OnSceneLoaded;
        prevScene = sceneSO.sceneName;
        audio = GetComponent<AudioSource>();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "OverworldBattleScene" || scene.name == "DungeonBattleScene" || scene.name == "BossBattleScene")
        {
            heroIterator = 0;
            enemyIterator = 0;
            battleStates = PerformAction.WAIT;
            StartCoroutine(HeroEnemyLists());
            ActionPanel.SetActive(false);
            ActionPanel2.SetActive(false);
            EnemyPanel.SetActive(false);
            HeroPanel.SetActive(false);
            MagicPanel.SetActive(false);
            ItemPanel.SetActive(false);
            DescPanel.SetActive(false);
            UserActionPanel.SetActive(false);
            FlavorPanel.SetActive(false);
            ActionPointer.SetActive(false);
            ActionPointer2.SetActive(false);
            DamagePanel.SetActive(false);
            firstRun = false;
            deadCounterRan = false;
            missed = false;
            assessUse = false;
            turnTaken = false;
            HeroInput = HeroGUI.ACTIVATE;
            HeroAction = Action.ATTACK;
        }
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        audio.volume = sceneSO.volume;

        switch (battleStates)
        {
            case (PerformAction.WAIT):
                if (PerformList.Count > 0)
                {
                    battleStates = PerformAction.TAKEACTION;
                }
                break;
            case (PerformAction.TAKEACTION):
                GameObject performer = GameObject.Find(PerformList[0].Attacker);
                if (PerformList[0].Type == "Enemy")
                {
                    EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>();
                    ESM.HeroToAttack = PerformList[0].Target;
                    ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                }
                if (PerformList[0].Type == "Hero")
                {
                    Debug.Log("Hero in perform list");
                }
                battleStates = PerformAction.PERFORMACTION;
                break;
            case (PerformAction.PERFORMACTION):

                break;
        }

        switch (HeroInput)
        {
            case (HeroGUI.ACTIVATE):
                if (firstRun)
                {
                    if (EnemyBattleList.Count != 0)
                    {
                        ActionPointer2.SetActive(true);
                        if (HeroesToManage.Count > 0)
                        {
                            if (heroIterator > HeroBattleList.Count - 1)
                            {
                                heroIterator = 0;
                            }
                            enemyIterator = 0;
                            curPlayer = HeroesToManage[0].transform.root.GetComponent<Player>();
                            ActionPointer2.transform.position = curPlayer.transform.position + new Vector3(0, 1, 0);
                            //Debug.Log(curPlayer);
                            EnemyPanel.SetActive(true);
                            HeroPanel.SetActive(true);
                            turnTaken = false;
                            canGo = false;
                            HeroChoice = new HandleTurns();
                            turnChoice = 1;
                            AttackText.color = Color.white;
                            MagicText.color = Color.white;
                            DefendText.color = Color.white;
                            ItemsText.color = Color.white;
                            RowText.color = Color.white;
                            FleeText.color = Color.white;
                            firstPos = new(curPlayer.gameObject.transform.position.x, curPlayer.gameObject.transform.position.y, curPlayer.gameObject.transform.position.z);
                            secondPos = new(curPlayer.gameObject.transform.position.x - 0.5f, curPlayer.gameObject.transform.position.y, curPlayer.gameObject.transform.position.z);
                            Vector3 pointerRotationDefault = new Vector3(0, 0, 0);
                            ActionPointer.transform.eulerAngles = pointerRotationDefault;
                            if (!curPlayer.ID.data.dead)
                            {
                                heroNames[heroIterator].color = Color.yellow;
                            }
                            else if (curPlayer.ID.data.dead == true)
                            {
                                Debug.Log(curPlayer + " = dead");
                                HeroInput = HeroGUI.DONE;
                                break;
                            }
                            HeroInput = HeroGUI.WAITING;
                        }
                        else
                        {
                            PopulateEnemyManageList();
                            Debug.Log("ENEMIES TURN!");
                            heroIterator = 0;
                            ActionPanel.SetActive(false);
                            ActionPanel2.SetActive(false);
                            ActionPointer.SetActive(false);
                            HeroPanel.SetActive(true);
                            EnemyPanel.SetActive(true);
                            //Enemies go now!
                            
                            tempFlavor = ("Enemies are attacking!");
                            StartCoroutine(FlavorTextFill());
                            enemyMoveRun = false;
                            heroNum = UnityEngine.Random.Range(0, HeroBattleList.Count);
                            curTarget = HeroBattleList[heroNum];
                            playerTargetData = curTarget.transform.root.GetComponent<Player>();
                            HeroInput = HeroGUI.WAITING;
                        }
                    }
                    else
                    {
                        Debug.Log("Victory");
                        //VICTORY!
                        StartCoroutine(Victory());
                        break;
                    }
                }
                break;
            case (HeroGUI.WAITING):
                if (HeroesToManage.Count > 0)
                {
                    if (!curPlayer.dead)
                    {
                        turnTaken = false;
                        StartCoroutine(TurnDelay());
                    }
                    else
                    {
                        //Debug.Log(curPlayer.ID.data.heroName + " is dead!");
                        HeroInput = HeroGUI.DONE;
                    }
                }
                else if (EnemiesToManage.Count > 0)
                {
                    if (!enemyMoveRun)
                    {
                        if (playerTargetData.ID.data.dead == true)
                        {
                            //Debug.Log(curTarget + " is dead");
                            heroNum = UnityEngine.Random.Range(0, HeroBattleList.Count);
                            curTarget = HeroBattleList[heroNum];
                            playerTargetData = curTarget.transform.root.GetComponent<Player>();
                            //CheckIfHeroIsDead();
                        }
                        else
                        {
                            //Debug.Log(curTarget + " is alive");
                            enemyMoveRun = true;
                            int i = System.Array.IndexOf(enemies, EnemiesToManage[0]);
                            enemyFirstPos = EnemiesToManage[0].transform.position;
                            enemySecondPos = new Vector3(EnemiesToManage[0].transform.position.x + 0.5f, EnemiesToManage[0].transform.position.y, EnemiesToManage[0].transform.position.z);
                            enemyNames[i].color = Color.yellow;
                            StartCoroutine(EnemyMove());
                        }
                    }
                }
                else
                {
                    PopulateHeroManageList();
                    HeroInput = HeroGUI.ACTIVATE;
                }
                break;
            case (HeroGUI.SELECTACTION):
                if (EnemyBattleList.Count != 0)
                {
                    if (HeroesToManage.Count != 0)
                    {
                        Debug.Log(turnTaken);
                        turnTaken = false;
                        if (turnTaken == false)
                        {
                            ActionPanel.SetActive(true);
                            ActionPointer.SetActive(true);
                            HeroPanel.SetActive(true);
                            EnemyPanel.SetActive(true);
                            MagicPanel.SetActive(false);
                            UserActionPanel.SetActive(false);
                            SelectAction();
                        }
                    }
                    else
                    {
                        HeroInput = HeroGUI.ACTIVATE;
                    }
                }
                else
                {
                    HeroInput = HeroGUI.ACTIVATE;
                }
                break;
            case (HeroGUI.MAGIC):
                ActionPointer2.SetActive(false);
                SpellChecker();
                ActionPanel.SetActive(false);
                EnemyPanel.SetActive(false);
                HeroPanel.SetActive(false);
                MagicPanel.SetActive(true);
                UserActionPanel.SetActive(true);
                ActionPointer.SetActive(true);
                HeroInput = HeroGUI.SPELLS;
                break;
            case (HeroGUI.SPELLS):
                SpellSelector();
                break;
            case (HeroGUI.ITEMS):
                //this is never called who cares
                HeroInput = HeroGUI.DONE;
                break;
            case (HeroGUI.TARGET):
                ActionPointer2.SetActive(false);
                if (EnemyBattleList.Count != 0)
                {
                    ActionPanel.SetActive(false);
                    ActionPanel2.SetActive(false);
                    Target();
                }
                else
                {
                    HeroInput = HeroGUI.ACTIVATE;
                }
                break;
            case (HeroGUI.MAGICTARGET):
                MagicTarget();
                break;
            case (HeroGUI.MOVESPELL):
                StartCoroutine(ThrowThatSpellBabyGirl());
                break;
            case (HeroGUI.DESTROYTARGET):
                DestroyTarget();
                HeroInput = HeroGUI.DONE;
                break;
            case (HeroGUI.DONE):
                ActionPointer.SetActive(false);
                missed = false;
                if (HeroesToManage.Count > 0)
                {
                    heroDisplayAction[heroIterator].text = actionTaken;
                    turnTaken = false;
                    HeroInputDone();
                }
                else if (EnemiesToManage.Count > 0)
                {
                    EnemyTurnDone();
                }
                else
                {
                    HeroInput = HeroGUI.ACTIVATE;
                }
                break;
            case (HeroGUI.GAMEEND):
                SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
                break;
        }
        if (firstRun)
        {
            for (int i = 0; i < partyIDArray.Length; i++)
            {
                heroDisplayHP[i].text = partyIDArray[i].stats.HP.ToString();
                heroDisplayMaxHP[i].text = partyIDArray[i].stats.maxHP.ToString();
                heroDisplayMP[i].text = partyIDArray[i].stats.MP.ToString();

            }
        }
        Debug.Log(HeroInput);
    }

    IEnumerator HeroEnemyLists()
    {
        yield return new WaitForSeconds(0.01f);
        HeroBattleList.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
        EnemyBattleList.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        enemies = new GameObject[EnemyBattleList.Count];
        partyIDArray = new PlayerID[HeroBattleList.Count];
        heroUIBars = new GameObject[HeroBattleList.Count];
        for (int i = 0; i < HeroBattleList.Count(); i++)
        {
            HeroesToManage.Add(HeroBattleList[i]);
            curPlayer = HeroesToManage[i].transform.root.GetComponent<Player>();
            if (curPlayer.backRow)
            {
                curPlayer.transform.position = new Vector3((5f + (curPlayer.slot / 2.5f)), (2.5f - curPlayer.slot), (0));
            }
            else
            {
                curPlayer.transform.position = new Vector3((4f + (curPlayer.slot / 2.5f)), (2.5f - curPlayer.slot), (0));
            }
            PartySlots(curPlayer, i);
        }
        for (int i = 0; i < EnemyBattleList.Count(); i++)
        {
            if (i == 0)
            {
                EnemyBattleList[i].transform.position = new Vector3(-3.24f, 0.55f, 0);
            }
            if (i == 1)
            {
                EnemyBattleList[i].transform.position = new Vector3(-2.49f, -1.11f, 0);
            }
            if (i == 2)
            {
                EnemyBattleList[i].transform.position = new Vector3(-6.02f, 0.03f, 0);
            }
            if (i == 3)
            {
                EnemyBattleList[i].transform.position = new Vector3(-5.19f, -1.43f, 0);
            }
            enemies[i] = EnemyBattleList[i];
        }
        firstRun = true;
    }

    public void CollectActions(HandleTurns input)
    {
        PerformList.Add(input);
    }

    Player PartySlots(Player hero, int i)
    {
        if (hero.ID.data.inParty)
        {
            if (hero.ID.data.partySlot == 1)
            {
                partyIDArray[0] = hero.ID;

                heroNames[0].text = hero.ID.data.heroName;
                heroDisplayHP[0].text = hero.ID.stats.HP.ToString();
                heroDisplayMaxHP[0].text = hero.ID.stats.maxHP.ToString();
                heroDisplayMP[0].text = hero.ID.stats.MP.ToString();
                heroUIBar1.gameObject.SetActive(true);
            }
            if (hero.ID.data.partySlot == 2)
            {
                partyIDArray[1] = hero.ID;
                heroNames[1].text = hero.ID.data.heroName;
                heroDisplayHP[1].text = hero.ID.stats.HP.ToString();
                heroDisplayMaxHP[1].text = hero.ID.stats.maxHP.ToString();
                heroDisplayMP[1].text = hero.ID.stats.MP.ToString();
                heroUIBar2.gameObject.SetActive(true);
            }
            if (hero.ID.data.partySlot == 3)
            {
                partyIDArray[2] = hero.ID;
                heroNames[2].text = hero.ID.data.heroName;
                heroDisplayHP[2].text = hero.ID.stats.HP.ToString();
                heroDisplayMaxHP[2].text = hero.ID.stats.maxHP.ToString();
                heroDisplayMP[2].text = hero.ID.stats.MP.ToString();
                heroUIBar3.gameObject.SetActive(true);
            }
            if (hero.ID.data.partySlot == 4)
            {
                partyIDArray[3] = hero.ID;
                heroNames[3].text = hero.ID.data.heroName;
                heroDisplayHP[3].text = hero.ID.stats.HP.ToString();
                heroDisplayMaxHP[3].text = hero.ID.stats.maxHP.ToString();
                heroDisplayMP[3].text = hero.ID.stats.MP.ToString();
                heroUIBar4.gameObject.SetActive(true);
            }
        }
        return hero;
    }

    public void SelectAction()
    {
        HeroChoice.Attacker = partyIDArray[heroIterator].data.heroName;
        HeroChoice.AttackerGameObject = HeroesToManage[0];
        curPlayer.defending = false;
        //Player ID is now stored in curPlayer.ID;
        HeroChoice.Type = "Hero";
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            turnChoice++;
            if (turnChoice > 6)
            {
                turnChoice = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            turnChoice--;
            if (turnChoice < 1)
            {
                turnChoice = 6;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (turnChoice < 5)
            {
                turnChoice = 5;
            }
            else if (turnChoice > 4)
            {
                turnChoice = 1;
            }
        }
        if (turnChoice < 5)
        {
            ActionPanel.SetActive(true);
            ActionPanel2.SetActive(false);
        }
        else
        {
            ActionPanel.SetActive(false);
            ActionPanel2.SetActive(true);
        }
        switch (turnChoice)
        {
            case (1):
                HeroAction = Action.ATTACK;
                //Debug.Log("Attack");
                ActionPointer.transform.position = new Vector3(-7.85f, -2.5f, 0);
                MagicText.color = Color.white;
                DefendText.color = Color.white;
                ItemsText.color = Color.white;
                RowText.color = Color.white;
                FleeText.color = Color.white;
                AttackText.color = Color.yellow;
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    turnTaken = true;
                    actionTaken = "Attack";
                    turnChoice = 1;
                    HeroInput = HeroGUI.TARGET;
                }
                break;
            case (2):
                HeroAction = Action.MAGIC;
                //Debug.Log("Magic");
                ActionPointer.transform.position = new Vector3(-7.85f, -3.14f, 0);
                AttackText.color = Color.white;
                DefendText.color = Color.white;
                ItemsText.color = Color.white;
                RowText.color = Color.white;
                FleeText.color = Color.white;
                if (curPlayer.ID.data.canDoMagic)
                {
                    userActionMP.text = "MP: " + curPlayer.ID.stats.MP.ToString();
                    userActionName.text = curPlayer.ID.data.heroName.ToString();
                    MagicText.color = Color.yellow;
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        if (curPlayer.ID.stats.MP >= 5)
                        {
                            turnTaken = true;
                            actionTaken = "Magic";
                            turnChoice = 1;
                            HeroInput = HeroGUI.MAGIC;
                            break;
                        }
                        else
                        {
                            MagicText.color = Color.red;
                            if (Input.GetKeyDown(KeyCode.Return))
                            {
                                tempFlavor = (curPlayer.ID.data.heroName + " doesn't have enough MP!");
                                StartCoroutine(FlavorTextFill());
                            }
                        }
                    }
                }
                else
                {
                    MagicText.color = Color.red;
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        //Display box that says this person can't do magic!
                        Debug.Log(curPlayer.ID.data.heroName + " can't do magic!");
                    }
                }
                break;
            case (3):
                HeroAction = Action.DEFEND;
                //Debug.Log("Defend");
                ActionPointer.transform.position = new Vector3(-7.85f, -3.74f, 0);
                AttackText.color = Color.white;
                MagicText.color = Color.white;
                ItemsText.color = Color.white;
                RowText.color = Color.white;
                FleeText.color = Color.white;
                DefendText.color = Color.yellow;
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    turnTaken = true;
                    actionTaken = "Defend";
                    curPlayer.defending = true;
                    HeroInput = HeroGUI.DONE;
                }
                break;
            case (4):
                HeroAction = Action.ITEMS;
                ActionPointer.transform.position = new Vector3(-7.85f, -4.35f, 0);
                AttackText.color = Color.white;
                MagicText.color = Color.white;
                DefendText.color = Color.white;
                RowText.color = Color.white;
                FleeText.color = Color.white;
                ItemsText.color = Color.yellow;
                //Debug.Log("Items");
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    tempFlavor = ("We outta items.");
                    StartCoroutine(FlavorTextFill());
                }
                break;
            case (5):
                HeroAction = Action.ROW;
                //Debug.Log("Row");
                ActionPointer.transform.position = new Vector3(-7.85f, -2.5f, 0);
                AttackText.color = Color.white;
                MagicText.color = Color.white;
                DefendText.color = Color.white;
                ItemsText.color = Color.white;
                FleeText.color = Color.white;
                RowText.color = Color.yellow;
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    turnTaken = true;
                    actionTaken = "Row";
                    curPlayer.backRow = !curPlayer.backRow;
                    if (curPlayer.backRow)
                    {
                        curPlayer.transform.position = new Vector3((5f + (curPlayer.slot / 2.5f)), (2.5f - curPlayer.slot), (0));
                    }
                    else
                    {
                        curPlayer.transform.position = new Vector3((4f + (curPlayer.slot / 2.5f)), (2.5f - curPlayer.slot), (0));
                    }
                    HeroInput = HeroGUI.DONE;
                }
                break;
            case (6):
                HeroAction = Action.FLEE;
                //Debug.Log("Flee");
                ActionPointer.transform.position = new Vector3(-7.85f, -3.14f, 0);
                AttackText.color = Color.white;
                MagicText.color = Color.white;
                DefendText.color = Color.white;
                ItemsText.color = Color.white;
                RowText.color = Color.white;
                FleeText.color = Color.yellow;
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    turnTaken = true;
                    actionTaken = "Flee";
                    //have a display box that pops up that says "The party is fleeing!" then wait a few seconds then load the respective scene
                    ActionPointer.SetActive(false);
                    ActionPointer2.SetActive(false);
                    StartCoroutine(Fleeing());
                    HeroInput = HeroGUI.FLEE;
                }
                break;
        }
    }

    IEnumerator Fleeing()
    {
        ActionPanel2.SetActive(false);
        FlavorPanel.SetActive(true);
        FlavorText.text = "The party is fleeing!";
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(prevScene, LoadSceneMode.Single);
    }
    IEnumerator Victory()
    {
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < HeroBattleList.Count(); i++)
        {
            curPlayer = HeroBattleList[i].transform.root.GetComponent<Player>();
            if (curPlayer.ID.data.dead)
            {
                curPlayer.ID.data.dead = false;
                curPlayer.ID.stats.HP = 1;
                Vector3 aliveRotation = new Vector3(0, 0, 0);
                curPlayer.transform.eulerAngles = aliveRotation;
            }
        }
        ActionPanel2.SetActive(false);
        FlavorPanel.SetActive(true);
        FlavorText.text = "Victory!";
        yield return new WaitForSeconds(1.8f);
        SceneManager.LoadScene(prevScene, LoadSceneMode.Single);
    }

    public void Target()
    {
        Vector3 pointerRotation = new Vector3(0, 0, -90);
        ActionPointer.transform.eulerAngles = pointerRotation;
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            turnChoice++;
            if (turnChoice > EnemyBattleList.Count)
            {
                turnChoice = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            turnChoice--;
            if (turnChoice < 1)
            {
                turnChoice = EnemyBattleList.Count;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (EnemyBattleList.Count > 2)
            {
                turnChoice -= 2;
                if (turnChoice < 1)
                {
                    if (turnChoice == 0)
                    {
                        turnChoice = 3;
                    }
                    else
                    {
                        turnChoice = EnemyBattleList.Count;
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (EnemyBattleList.Count > 2 && turnChoice <= EnemyBattleList.Count)
            {
                turnChoice += 2;
            }
            if (turnChoice > EnemyBattleList.Count)
            {
                if (turnChoice == 5)
                {
                    turnChoice = 2;
                }
                else
                {
                    turnChoice = 1;
                }
            }
        }
        if (turnChoice > EnemyBattleList.Count)
        {
            turnChoice = EnemyBattleList.Count;
        }

        Debug.Log(turnChoice);
        curTarget = EnemyBattleList[turnChoice - 1];
        targetFirstPos = new(curTarget.transform.position.x, curTarget.transform.position.y, curTarget.transform.position.z);
        targetSecondPos = new(curTarget.transform.position.x + 0.5f, curTarget.transform.position.y, curTarget.transform.position.z);
        Debug.Log(curTarget);
        ActionPointer.transform.position = curTarget.transform.position + new Vector3(0, 1, 0);
        //change color of enemy text here
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            turnTaken = false;
            DescPanel.SetActive(false);
            Vector3 pointerRotationDefault = new Vector3(0, 0, 0);
            ActionPointer.transform.eulerAngles = pointerRotationDefault;
            turnChoice = 1;
            HeroInput = HeroGUI.SELECTACTION;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ActionPointer.SetActive(false);
            Vector3 pointerRotationDefault = new Vector3(0, 0, 0);
            ActionPointer.transform.eulerAngles = pointerRotationDefault;
            HeroChoice.Target = curTarget;
            curTargetData = curTarget.transform.root.GetComponent<Enemy>();
            PhysDamageCalculator(curPlayer.ID.stats.str, curPlayer.ID.stats.agi, curPlayer.ID.data.lvl, curTargetData.ID.data.backRow, curTarget, curTargetData.ID.stats.def, curTargetData.ID.stats.agi, curTargetData.ID.data.stunned, curTargetData.ID.data.defending);
            curTargetData.curHP -= atkDmg;
            if (!missed)
            {
                DamagePanel.SetActive(true);
                DamagePopUp.PopUp(atkDmg.ToString());
                StartCoroutine(DamagePanelTimer());
            }
            StartCoroutine(attackerJiggle());
            if (atkDmg > 0)
            {
                //play dmg sfx here
                audio.clip = dmgSFX;
                audio.Play();
                if (curTarget.transform.root.GetComponent<Enemy>().curHP <= 0)
                {
                    Destroy(curTarget);
                    int i = System.Array.IndexOf(enemies, curTarget);
                    enemyNameBars[i].SetActive(false);
                    EnemyBattleList.Remove(curTarget);
                    StartCoroutine(AttackDelay());
                }
                else
                {
                    StartCoroutine(targetJiggle());
                }
            }
            else
            {
                //miss sfx
                audio.clip = missSFX;
                audio.Play();
                StartCoroutine(AttackDelay());
            }
        }
    }

    IEnumerator DamagePanelTimer()
    {
        yield return new WaitForSeconds(1f);
        DamagePanel.SetActive(false);
    }

    void HeroInputDone()
    {
        curPlayer.ID.data.backRow = curPlayer.backRow;
        heroNames[heroIterator].color = Color.white;
        heroIterator++;
        //PerformList.Add(HeroChoice);
        HeroesToManage.RemoveAt(0);
        actionTaken = "";
        HeroInput = HeroGUI.ACTIVATE;
    }

    void EnemyTurnDone()
    {
        int i = System.Array.IndexOf(enemies, EnemiesToManage[0]);
        enemyNames[i].color = Color.white;
        EnemiesToManage.RemoveAt(0);
        enemyIterator++;
        enemyMoveRun = false;
        if (!deadCounterRan)
        {
            PartyDeadCheck();
        }
    }

    int PhysDamageCalculator(int strValue, int agiValue, int lvlValue, bool playerTargetBackRowValue, GameObject target, int targetDefValue, int targetAgiValue, bool targetStunned, bool targetDefending)
    {
        //hitCritChance is a random number generated 1 to 200 that determines whether an attack will crit, hit, or miss. It is based on a scale where a lower number is better.
        int hitCritChance = rnd.Next(0, 200) + 1;
        //This resets atkDmg to 0 each time the damage calculator is run as a failsafe
        atkDmg = 0;
        //This if statement is checking if it is a physical attack, spellType 0 is considered a physical attack. Anything higher than spellType 0 is an actual spell
        PhysDamageRating();
        PhysAttackChance();

        void PhysDamageRating()
        {
            //The damage rating is derived from the stats of the attacker and will be used in the damage roll if the attack lands
            damageRating = ((strValue / 2) * ((lvlValue + 1) / 2));
            //If the attacker is in the back row, it will cut the damage rating in half
            if (playerTargetBackRowValue == true)
            {
                //Debug.Log("Damage reduced by half due to: Back Row");
                damageRating = damageRating / 2;
            }
            if (damageRating < 1)
            {
                damageRating = 1;
            }
            //Debug.Log("Attacker Damage Rating: " + damageRating);
        }

        void PhysAttackChance()
        {
            int critChance = 10 + ((agiValue / 2) - (targetAgiValue / 4));
            //Checks if the hitCritChance random number generated is low enough to crit
            if (hitCritChance != 200 && hitCritChance <= critChance && hitCritChance <= 100 || hitCritChance == 1)
            {
                Debug.Log(target + " was crit!");
                int damage = rnd.Next(damageRating, damageRating * 2);
                //Debug.Log("Pre-Crit Damage: " + damage);
                atkDmg = damage * 2;
            }
            //If the hitCritChance random number generated isn't low enough to be considered a crit it will check here if it is enough to hit
            else if (hitCritChance != 200 && hitCritChance <= 150 + (agiValue / 2) - (targetAgiValue / 2) || targetStunned == true)
            {
                Debug.Log(target + " was hit!");
                //The below if statement is purely for balancing purposes, this information will not be shown to the player
                if (critChance < 1)
                {
                    //Debug.Log("# Needed to Crit: 1");
                }
                else if (critChance < 100)
                {
                    //Debug.Log("# Needed to Crit: " + critChance);
                }
                else
                {
                    //Debug.Log("# Needed to Crit: 100");
                }
                //Damage roll
                int damage = rnd.Next(damageRating, damageRating * 2);
                atkDmg = damage - targetDefValue;
                //Debug.Log("Raw Damage: " + damage + "   Reduced by: " + targetDefValue + " Target Defense!");
                if (atkDmg < 1)
                {
                    if (target.CompareTag("Hero") == true)
                    {
                        //If that target IS a Player Character, minimum damage is 0 even if you hit
                        //Debug.Log("Target is Hero");
                        atkDmg = 0;
                    }
                    else if (target.CompareTag("Enemy") == true)
                    {
                        //If the Target is NOT a Player Character, minimum damage IF you hit is 1
                        //Debug.Log("Target is Enemy");
                        atkDmg = 1;
                    }
                }
            }
            else
            {
                //If the hitCritChance random number generated isn't low enough to hit, the attack misses and the attacker's turn is over
                Debug.Log("Missed!");
                missed = true;
                DamagePanel.SetActive(true);
                DamagePopUp.PopUp("Missed!");
                StartCoroutine(DamagePanelTimer());
                //Debug.Log("# Needed to Hit: " + (150 + (agiValue / 2) - (targetAgiValue / 2)));
                atkDmg = 0;
            }
            //Debug.Log("Hit Chance: " + hitCritChance);
        }

        //This is unlikely to be used in our prototype but it is to cap damage at 9999
        if (atkDmg > 9999)
        {
            atkDmg = 9999;
        }

        //If the Target is a Player Character AND the Target is in the Back Row, Then damage dealt/taken is halved
        if (playerTargetBackRowValue == true)
        {
            if (atkDmg > 1)
            {
                atkDmg = atkDmg / 2;
            }
        }

        Debug.Log("Final Damage: " + atkDmg);
        return atkDmg;
    }

    public IEnumerator targetJiggle()
    {
        yield return new WaitForSeconds(0.03f);
        Vector3 startPosition = new(curTarget.gameObject.transform.position.x, curTarget.gameObject.transform.position.y, curTarget.gameObject.transform.position.z);
        if (curTarget.tag == "Hero")
        {
            //Debug.Log("Hero was moved");
            Vector3 newPosition = new(curTarget.gameObject.transform.position.x + 0.5f, curTarget.gameObject.transform.position.y, curTarget.gameObject.transform.position.z);
            curTarget.transform.position = newPosition;
        }
        else
        {
            Vector3 newPosition = new(curTarget.gameObject.transform.position.x - 0.5f, curTarget.gameObject.transform.position.y, curTarget.gameObject.transform.position.z);
            curTarget.transform.position = newPosition;
        }
        yield return new WaitForSeconds(0.1f);
        curTarget.transform.position = startPosition;
        HeroInput = HeroGUI.DONE;
    }

    public IEnumerator attackerJiggle()
    {
        curPlayer.transform.position = secondPos;
        yield return new WaitForSeconds(0.05f);
        curPlayer.transform.position = firstPos;
    }
    public IEnumerator enemyJiggle()
    {
        curEnemy.transform.position = enemySecondPos;
        yield return new WaitForSeconds(0.01f);
        curEnemy.transform.position = enemyFirstPos;
    }

    void PopulateHeroManageList()
    {
        for (int i = 0; i < HeroBattleList.Count(); i++)
        {
            HeroesToManage.Add(HeroBattleList[i]);
            //curPlayer = HeroesToManage[i].transform.root.GetComponent<Player>();
            if (curPlayer.backRow)
            {
                curPlayer.transform.position = new Vector3((5f + (curPlayer.slot / 2.5f)), (2.5f - curPlayer.slot), (0));
            }
            else
            {
                curPlayer.transform.position = new Vector3((4f + (curPlayer.slot / 2.5f)), (2.5f - curPlayer.slot), (0));
            }
        }
    }

    void PopulateEnemyManageList()
    {
        for (int i = 0; i < EnemyBattleList.Count(); i++)
        {
            EnemiesToManage.Add(EnemyBattleList[i]);
        }
    }

    public IEnumerator EnemyMove()
    {
        yield return new WaitForSeconds(1f);
        playerTargetData = curTarget.transform.root.GetComponent<Player>();
        curEnemy = EnemiesToManage[0].transform.root.GetComponent<Enemy>();
        ActionPointer2.SetActive(true);
        ActionPointer2.transform.position = curEnemy.transform.position + new Vector3(0, 1, 0);
        //curEnemy.ID.data.lvl is currently equal to 1 for each enemy in the game. change this for balancing maybe idfk
        PhysDamageCalculator(curEnemy.ID.stats.str, curEnemy.ID.stats.agi, curEnemy.ID.data.lvl, playerTargetData.ID.data.backRow, curTarget, playerTargetData.ID.stats.def, playerTargetData.ID.stats.agi, playerTargetData.ID.data.stunned, playerTargetData.defending);
        playerTargetData.ID.stats.HP -= atkDmg;
        //Debug.Log(curTarget + playerTargetData.ToString() + " took " + atkDmg + " damage");
        DamagePanel.SetActive(true);
        if (!missed)
        {
            DamagePopUp.PopUp(atkDmg.ToString());
            StartCoroutine(DamagePanelTimer());
            StartCoroutine(enemyJiggle());
        }
        if (atkDmg > 0)
        {
            //dmg sfx
            audio.clip = dmgSFX;
            audio.Play();
            if (playerTargetData.ID.stats.HP <= 0)
            {
                playerTargetData.ID.stats.HP = 0;
                playerTargetData.ID.data.dead = true;
                Vector3 deadRotation = new Vector3(0, 0, 90);
                curTarget.transform.eulerAngles = deadRotation;
                yield return new WaitForSeconds(1f);
                HeroInput = HeroGUI.DONE;
            }
            else
            {
                StartCoroutine(targetJiggle());
            }
        }
        else
        {
            //miss sfx
            audio.clip = missSFX;
            audio.Play();
            yield return new WaitForSeconds(1f);
            HeroInput = HeroGUI.DONE;
        }
    }
    public IEnumerator TurnDelay()
    {
        StartCoroutine(CanGo());
        yield return new WaitForSeconds(0.01f);
        if (canGo == true)
        {
            HeroInput = HeroGUI.SELECTACTION;
        }
    }

    public IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(0.01f);
        HeroInput = HeroGUI.DONE;
    }

    void PartyDeadCheck()
    {
        deadCounterRan = true;
        int deadCounter = 0;
        for (int i = 0; i < HeroBattleList.Count; i++)
        {
            if (partyIDArray[i].data.dead == true)
            {
                deadCounter++;
            }
            if (deadCounter == HeroBattleList.Count)
            {
                //Game Over
                Debug.Log("Game Over");
                HeroInput = HeroGUI.GAMEEND;
            }
            else
            {
                deadCounterRan = false;
                HeroInput = HeroGUI.WAITING;
            }
        }
    }

    void SpellChecker()
    {
        if (curPlayer.ID.data.knowsFire1)
        {
            Fire1Text.color = Color.white;
        }
        else
        {
            Fire1Text.color = Color.grey;
        }
        if (curPlayer.ID.data.knowsFire2)
        {
            Fire2Text.color = Color.white;
        }
        else
        {
            Fire2Text.color = Color.grey;
        }
        if (curPlayer.ID.data.knowsIce1)
        {
            Ice1Text.color = Color.white;
        }
        else
        {
            Ice1Text.color = Color.grey;
        }
        if (curPlayer.ID.data.knowsIce2)
        {
            Ice2Text.color = Color.white;
        }
        else
        {
            Ice2Text.color = Color.grey;
        }
        if (curPlayer.ID.data.knowsZap1)
        {
            Zap1Text.color = Color.white;
        }
        else
        {
            Zap1Text.color = Color.grey;
        }
        if (curPlayer.ID.data.knowsZap2)
        {
            Zap2Text.color = Color.white;
        }
        else
        {
            Zap2Text.color = Color.grey;
        }
        if (curPlayer.ID.data.knowsCure)
        {
            CureText.color = Color.white;
        }
        else
        {
            CureText.color = Color.grey;
        }
        if (curPlayer.ID.data.knowsAssess)
        {
            AssessText.color = Color.white;
        }
        else
        {
            AssessText.color = Color.grey;
        }
    }

    void SpellSelector()
    {
        DescPanel.SetActive(true);
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            turnChoice++;
            if (turnChoice > 8)
            {
                turnChoice = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            turnChoice--;
            if (turnChoice < 1)
            {
                turnChoice = 8;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (turnChoice == 7)
            {
                turnChoice = 2;
            } else if (turnChoice == 8)
            {
                turnChoice = 3;
            }
            else
            {
                turnChoice += 3;
                if (turnChoice > 8)
                {
                    turnChoice = 1;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (turnChoice == 3)
            {
                turnChoice = 8;
            }
            else if (turnChoice == 2)
            {
                turnChoice = 7;
            }
            else
            {
                turnChoice -= 3;
                if (turnChoice < 1)
                {
                    turnChoice = 6;
                }
            }
        }
        switch (turnChoice)
        {
            case (1):
                ActionPointer.transform.position = new Vector3(-7.32f, -2.75f, 0);
                DescText.text = "First level black magic.";
                if (curPlayer.ID.data.knowsFire1)
                {
                    curSpell = fireData;
                    SpellChecker();
                    Fire1Text.color = Color.yellow;
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        if (curPlayer.ID.stats.MP >= 5)
                        {
                            Instantiate(fireball, new Vector3(curPlayer.transform.position.x - 1f, curPlayer.transform.position.y, curPlayer.transform.position.z), Quaternion.identity);
                            spellChoice = turnChoice;
                            turnChoice = 1;
                            MagicPanel.SetActive(false);
                            DescPanel.SetActive(false);
                            HeroInput = HeroGUI.MAGICTARGET;
                        }
                        else
                        {
                            tempFlavor = (curPlayer.ID.data.heroName + " doesn't have enough MP!");
                            StartCoroutine(FlavorTextFill());
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    StartCoroutine(SpellDestroyer());
                    DescPanel.SetActive(false);
                    Vector3 pointerRotationDefault = new Vector3(0, 0, 0);
                    ActionPointer.transform.eulerAngles = pointerRotationDefault;
                    turnChoice = 1;
                    HeroInput = HeroGUI.SELECTACTION;
                }
                break;
            case (4):
                ActionPointer.transform.position = new Vector3(-3.67f, -2.75f, 0);
                DescText.text = "Second level black magic.";
                if (curPlayer.ID.data.knowsFire2)
                {
                    curSpell = firaData;
                    SpellChecker();
                    Fire2Text.color = Color.yellow;
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        if (curPlayer.ID.stats.MP >= 10)
                        {
                            Instantiate(fireball, new Vector3(curPlayer.transform.position.x - 1f, curPlayer.transform.position.y, curPlayer.transform.position.z), Quaternion.identity);
                            spellChoice = turnChoice;
                            turnChoice = 1;
                            MagicPanel.SetActive(false);
                            DescPanel.SetActive(false);
                            HeroInput = HeroGUI.MAGICTARGET;
                        }
                        else
                        {
                            tempFlavor = (curPlayer.ID.data.heroName + " doesn't have enough MP!");
                            StartCoroutine(FlavorTextFill());
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    StartCoroutine(SpellDestroyer());
                    DescPanel.SetActive(false);
                    Vector3 pointerRotationDefault = new Vector3(0, 0, 0);
                    ActionPointer.transform.eulerAngles = pointerRotationDefault;
                    turnChoice = 1;
                    HeroInput = HeroGUI.SELECTACTION;
                }
                break;
            case (2):
                ActionPointer.transform.position = new Vector3(-7.32f, -3.46f, 0);
                DescText.text = "First level black magic.";
                if (curPlayer.ID.data.knowsIce1)
                {
                    curSpell = blizzardData;
                    
                    SpellChecker();
                    Ice1Text.color = Color.yellow;
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        if (curPlayer.ID.stats.MP >= 5)
                        {
                            Instantiate(iceball, new Vector3(curPlayer.transform.position.x - 1f, curPlayer.transform.position.y, curPlayer.transform.position.z), Quaternion.identity);
                            spellChoice = turnChoice;
                            turnChoice = 1;
                            MagicPanel.SetActive(false);
                            DescPanel.SetActive(false);
                            HeroInput = HeroGUI.MAGICTARGET;
                        }
                        else
                        {
                            tempFlavor = (curPlayer.ID.data.heroName + " doesn't have enough MP!");
                            StartCoroutine(FlavorTextFill());
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    StartCoroutine(SpellDestroyer());
                    DescPanel.SetActive(false);
                    Vector3 pointerRotationDefault = new Vector3(0, 0, 0);
                    ActionPointer.transform.eulerAngles = pointerRotationDefault;
                    turnChoice = 1;
                    HeroInput = HeroGUI.SELECTACTION;
                }
                break;
            case (5):
                ActionPointer.transform.position = new Vector3(-3.67f, -3.46f, 0);
                DescText.text = "Second level black magic.";
                if (curPlayer.ID.data.knowsIce2)
                {
                    curSpell = blizzaraData;
                    SpellChecker();
                    Ice2Text.color = Color.yellow;
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        if (curPlayer.ID.stats.MP >= 10)
                        {
                            Instantiate(iceball, new Vector3(curPlayer.transform.position.x - 1f, curPlayer.transform.position.y, curPlayer.transform.position.z), Quaternion.identity);
                            spellChoice = turnChoice;
                            turnChoice = 1;
                            MagicPanel.SetActive(false);
                            DescPanel.SetActive(false);
                            HeroInput = HeroGUI.MAGICTARGET;
                        }
                        else
                        {
                            tempFlavor = (curPlayer.ID.data.heroName + " doesn't have enough MP!");
                            StartCoroutine(FlavorTextFill());
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    StartCoroutine(SpellDestroyer());
                    DescPanel.SetActive(false);
                    Vector3 pointerRotationDefault = new Vector3(0, 0, 0);
                    ActionPointer.transform.eulerAngles = pointerRotationDefault;
                    turnChoice = 1;
                    HeroInput = HeroGUI.SELECTACTION;
                }
                break;
            case (3):
                ActionPointer.transform.position = new Vector3(-7.32f, -4.15f, 0);
                DescText.text = "First level black magic.";
                if (curPlayer.ID.data.knowsZap1)
                {
                    curSpell = thunderData;
                    SpellChecker();
                    Zap1Text.color = Color.yellow;
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        if (curPlayer.ID.stats.MP >= 5)
                        {
                            Instantiate(lightningball, new Vector3(curPlayer.transform.position.x - 1f, curPlayer.transform.position.y, curPlayer.transform.position.z), Quaternion.identity);
                            spellChoice = turnChoice;
                            turnChoice = 1;
                            MagicPanel.SetActive(false);
                            DescPanel.SetActive(false);
                            HeroInput = HeroGUI.MAGICTARGET;
                        }
                        else
                        {
                            tempFlavor = (curPlayer.ID.data.heroName + " doesn't have enough MP!");
                            StartCoroutine(FlavorTextFill());
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    StartCoroutine(SpellDestroyer());
                    DescPanel.SetActive(false);
                    Vector3 pointerRotationDefault = new Vector3(0, 0, 0);
                    ActionPointer.transform.eulerAngles = pointerRotationDefault;
                    turnChoice = 1;
                    HeroInput = HeroGUI.SELECTACTION;
                }
                break;
            case (6):
                ActionPointer.transform.position = new Vector3(-3.67f, -4.15f, 0);
                DescText.text = "Second level black magic.";
                if (curPlayer.ID.data.knowsZap2)
                {
                    curSpell = thundaraData;
                    SpellChecker();
                    Zap2Text.color = Color.yellow;
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        if (curPlayer.ID.stats.MP >= 10)
                        {
                            Instantiate(lightningball, new Vector3(curPlayer.transform.position.x - 1f, curPlayer.transform.position.y, curPlayer.transform.position.z), Quaternion.identity);
                            spellChoice = turnChoice;
                            turnChoice = 1;
                            MagicPanel.SetActive(false);
                            DescPanel.SetActive(false);
                            HeroInput = HeroGUI.MAGICTARGET;
                        }
                        else
                        {
                            tempFlavor = (curPlayer.ID.data.heroName + " doesn't have enough MP!");
                            StartCoroutine(FlavorTextFill());
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    StartCoroutine(SpellDestroyer());
                    DescPanel.SetActive(false);
                    Vector3 pointerRotationDefault = new Vector3(0, 0, 0);
                    ActionPointer.transform.eulerAngles = pointerRotationDefault;
                    turnChoice = 1;
                    HeroInput = HeroGUI.SELECTACTION;
                }
                break;
            case (7):
                ActionPointer.transform.position = new Vector3(0.15f, -2.75f, 0);
                DescText.text = "Heal the target by 1/3 of their Max HP!";
                if (curPlayer.ID.data.knowsCure)
                {
                    curSpell = cureData;
                    SpellChecker();
                    CureText.color = Color.yellow;
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        if (curPlayer.ID.stats.MP >= 5)
                        {
                            spellChoice = turnChoice;
                            turnChoice = 1;
                            MagicPanel.SetActive(false);
                            DescPanel.SetActive(false);
                            UserActionPanel.SetActive(false);
                            HeroInput = HeroGUI.MAGICTARGET;
                        }
                        else
                        {
                            tempFlavor = (curPlayer.ID.data.heroName + " doesn't have enough MP!");
                            StartCoroutine(FlavorTextFill());
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    StartCoroutine(SpellDestroyer());
                    DescPanel.SetActive(false);
                    Vector3 pointerRotationDefault = new Vector3(0, 0, 0);
                    ActionPointer.transform.eulerAngles = pointerRotationDefault;
                    turnChoice = 1;
                    HeroInput = HeroGUI.SELECTACTION;
                }
                break;
            case (8):
                ActionPointer.transform.position = new Vector3(0.15f, -3.46f, 0);
                DescText.text = "Gives detailed information on the target";
                if (curPlayer.ID.data.knowsAssess)
                {
                    curSpell = assessData;
                    SpellChecker();
                    AssessText.color = Color.yellow;
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        if (curPlayer.ID.stats.MP >= 5)
                        {
                            spellChoice = turnChoice;
                            turnChoice = 1;
                            MagicPanel.SetActive(false);
                            DescPanel.SetActive(false);
                            UserActionPanel.SetActive(false);
                            HeroPanel.SetActive(true);
                            HeroInput = HeroGUI.MAGICTARGET;
                        }
                        else
                        {
                            tempFlavor = (curPlayer.ID.data.heroName + " doesn't have enough MP!");
                            StartCoroutine(FlavorTextFill());
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    StartCoroutine(SpellDestroyer());
                    DescPanel.SetActive(false);
                    //UnityEngine.Object obj = UnityEngine.GameObject.FindGameObjectsWithTag("Spell"));
                    Vector3 pointerRotationDefault = new Vector3(0, 0, 0);
                    ActionPointer.transform.eulerAngles = pointerRotationDefault;
                    turnChoice = 1;
                    HeroInput = HeroGUI.SELECTACTION;
                }
                break;
        }
    }

    public void MagicTarget()
    {
        Vector3 pointerRotation = new Vector3(0, 0, -90);
        ActionPointer.transform.eulerAngles = pointerRotation;
        UserActionPanel.SetActive(false);
        HeroPanel.SetActive(true);
        EnemyPanel.SetActive(true);
        if (curSpell != cureData)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                turnChoice++;
                if (turnChoice > EnemyBattleList.Count)
                {
                    turnChoice = 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                turnChoice--;
                if (turnChoice < 1)
                {
                    turnChoice = EnemyBattleList.Count;
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (EnemyBattleList.Count > 2)
                {
                    turnChoice -= 2;
                    if (turnChoice < 1)
                    {
                        if (turnChoice == 0)
                        {
                            turnChoice = 3;
                        }
                        else
                        {
                            turnChoice = EnemyBattleList.Count;
                        }
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (EnemyBattleList.Count > 2 && turnChoice <= EnemyBattleList.Count)
                {
                    turnChoice += 2;
                }
                if (turnChoice > EnemyBattleList.Count)
                {
                    if (turnChoice == 5)
                    {
                        turnChoice = 2;
                    }
                    else
                    {
                        turnChoice = 1;
                    }
                }
            }
            curTarget = EnemyBattleList[turnChoice - 1];
            targetFirstPos = new(curTarget.transform.position.x, curTarget.transform.position.y, curTarget.transform.position.z);
            targetSecondPos = new(curTarget.transform.position.x + 0.5f, curTarget.transform.position.y, curTarget.transform.position.z);
            curTargetData = curTarget.GetComponent<Enemy>();
            goalPosition = curTarget.transform.position;
            ActionPointer.transform.position = curTarget.transform.position + new Vector3(0, 1, 0);
            //change color of enemy text here
            if (curSpell != assessData)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    UserActionPanel.SetActive(false);
                    curPlayer.ID.stats.MP -= curSpell.MPCost;
                    for (int i = 0; i < partyIDArray.Length; i++)
                    {
                        heroDisplayMP[i].text = partyIDArray[i].stats.MP.ToString();
                    }
                    MagicDmgCalc(curPlayer.ID.stats.mag, curPlayer.ID.data.lvl, curTargetData.ID.stats.mag, curSpell.spellDmg);
                    curTargetData.curHP -= atkDmg;
                    DamagePanel.SetActive(true);
                    DamagePopUp.PopUp(atkDmg.ToString());
                    StartCoroutine(DamagePanelTimer());
                    StartCoroutine(attackerJiggle());
                    if (atkDmg > 0)
                    {
                        //play dmg sfx here
                        audio.clip = dmgSFX;
                        audio.Play();
                        Spells.AddRange(UnityEngine.GameObject.FindGameObjectsWithTag("Spell"));
                        HeroInput = HeroGUI.MOVESPELL;
                    }
                    else
                    {
                        //miss sfx
                        audio.clip = missSFX;
                        audio.Play();
                        StartCoroutine(SpellDestroyer());
                        HeroInput = HeroGUI.DONE;
                    }
                }
            }
            else
            {
                //assess
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    //healing sfx
                    assessUse = true;
                    audio.clip = healSFX;
                    audio.Play();
                    tempFlavor = (curTargetData.ID.stats.enemyName + " Max HP: " + curTargetData.ID.stats.maxHP + " STR: " + curTargetData.ID.stats.str + " MAG: " + curTargetData.ID.stats.mag + " DEF: " + curTargetData.ID.stats.def + " AGI: " + curTargetData.ID.stats.agi);
                    StartCoroutine(FlavorTextFill());
                    UserActionPanel.SetActive(false);
                    HeroInput = HeroGUI.DONE;
                }
            }
        }
        else
        {
            HeroPanel.SetActive(true);
            EnemyPanel.SetActive(true);
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                turnChoice++;
                if (turnChoice > HeroBattleList.Count)
                {
                    turnChoice = 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                turnChoice--;
                if (turnChoice < 1)
                {
                    turnChoice = HeroBattleList.Count;
                }
            }
            curTarget = HeroBattleList[turnChoice - 1];
            playerTargetData = curTarget.transform.root.GetComponent<Player>();
            ActionPointer.transform.position = curTarget.transform.position + new Vector3(0, 1, 0);
            if (Input.GetKeyDown(KeyCode.Return) && !playerTargetData.ID.data.dead)
            {
                UserActionPanel.SetActive(false);
                curPlayer.ID.stats.MP -= curSpell.MPCost;
                for (int i = 0; i < partyIDArray.Length; i++)
                {
                    heroDisplayMP[i].text = partyIDArray[i].stats.MP.ToString();
                }
                //healing sfx
                audio.clip = healSFX;
                audio.Play();
                Cure();
            }
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            turnTaken = false;
            StartCoroutine(SpellDestroyer());
            DescPanel.SetActive(false);
            Vector3 pointerRotationDefault = new Vector3(0, 0, 0);
            ActionPointer.transform.eulerAngles = pointerRotationDefault;
            turnChoice = 1;
            HeroInput = HeroGUI.MAGIC;
        }
    }

    void Cure()
    {
        playerTargetData.ID.stats.HP += playerTargetData.ID.stats.maxHP / 3;
        if (playerTargetData.ID.stats.HP > playerTargetData.ID.stats.maxHP)
        {
            playerTargetData.ID.stats.HP = playerTargetData.ID.stats.maxHP;
        }
        UserActionPanel.SetActive(false);
        HeroInput = HeroGUI.DONE;
    }

    int MagicDmgCalc(int mag, int lvl, int targetMag, int spellDmg)
    {
        atkDmg = 0;
        atkDmg = (spellDmg * mag * lvl) / targetMag;
        return atkDmg;
    }

    IEnumerator FlavorTextFill()
    {
        FlavorText.text = tempFlavor;
        FlavorPanel.SetActive(true);
        if (!assessUse)
        {
            yield return new WaitForSeconds(2f);
        }
        else
        {
            yield return new WaitForSeconds(4f);
        }
        assessUse = false;
        FlavorPanel.SetActive(false);
        FlavorText.text = "";
    }

    IEnumerator SpellDestroyer()
    {
        yield return new WaitForSeconds(0.1f);
        if (Spells.Count > 0)
        {
            spellThatExists = Spells[0];
            Destroy(spellThatExists);
            if (Spells[0] != null)
            {
                Spells.RemoveAt(0);
            }
        }
    }

    IEnumerator ThrowThatSpellBabyGirl()
    {
        if (Spells.Count > 0)
        {
            spellThatExists = Spells[0];
            spellThatExists.transform.position = Vector3.Lerp(spellThatExists.transform.position, goalPosition, 7f * Time.deltaTime);
            //Debug.Log("spell moving");
        }
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(SpellDestroyer());
        HeroInput = HeroGUI.DESTROYTARGET;
    }

    void DestroyTarget()
    {
        if (curTarget != null)
        {
            if (curTarget.transform.root.GetComponent<Enemy>().curHP <= 0)
            {
                Destroy(curTarget);
                int i = System.Array.IndexOf(enemies, curTarget);
                enemyNameBars[i].SetActive(false);
                EnemyBattleList.Remove(curTarget);
            }
            else
            {
                StartCoroutine(targetJiggle());
            }
        }
    }

    IEnumerator CanGo()
    {
        yield return new WaitForSeconds(0.05f);
        canGo = true;
    }
}

