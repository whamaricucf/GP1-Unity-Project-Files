using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private SceneSO sceneSO;
    public float moveSpeed = 5f;
    public Vector3 moveInput;
    public Transform movePoint;
    public Transform Knightro;
    public GameObject MenuCanvas;
    public GameObject MenuButton;
    private bool menuOpen;
    private string curScene;
    private int encounterCD;
    private bool firstRunOW;
    private bool firstRunD;

    public LayerMask whatStopsMovement;

    static Random rnd = new Random();

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        sceneSO.encounterCD = 10;
        sceneSO.firstRunOW = false;
        sceneSO.firstRunD = false;
        sceneSO.firstRunD = true;
        sceneSO.firstRunDEncounter = false;
    }

    void OnEnable()
    {
        movePoint.parent = null;
        SceneManager.sceneLoaded += OnSceneLoaded;
        encounterCD = sceneSO.encounterCD;
        if (sceneSO.firstRunOW)
        {
            StartCoroutine(SetPos());
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneSO.sceneName = scene.name;
        curScene = scene.name;
        if (curScene == "Overworld" && !sceneSO.firstRunOW)
        {
            Knightro.position = new Vector3(-3.5f, -3.5f, 0);
            movePoint.position = new Vector3(-3.5f, -3.5f, 0);
        }
        if (curScene == "Overworld" && sceneSO.firstRunOW)
        {
            StartCoroutine(SetPos());
        }
        if (curScene == "Dungeon" && sceneSO.firstRunD)
        {
            sceneSO.firstRunD = false;
            Knightro.position = new Vector3(0.5f, -10.5f, 0);
            movePoint.position = new Vector3(0.5f, -10.5f, 0);
            anim.SetFloat("VelocityY", 1f);
            anim.SetFloat("VelocityX", 0f);
            sceneSO.worldPos = movePoint.position;
        }
        if (curScene == "Dungeon" && sceneSO.firstRunDEncounter)
        {
            StartCoroutine(SetPos());
        }
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        MovementAndMenu();
        SceneSwap();
    }

    void MovementAndMenu()
    {
        if (!menuOpen)
        {
            MenuButton.SetActive(true);
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                    {
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                        anim.SetFloat("VelocityY", 0f);
                        anim.SetFloat("VelocityX", (movePoint.position.x - Knightro.position.x));
                        
                        if (encounterCD > 0)
                        {
                            encounterCD--;//lowering the cooldown int to 0 for each step taken
                        }
                        if (!Input.GetKey(KeyCode.Space))
                        {
                            EncounterLoader();
                        }
                    }
                }else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                    {
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                        anim.SetFloat("VelocityX", 0f);
                        anim.SetFloat("VelocityY", (movePoint.position.y - Knightro.position.y));
                        if (encounterCD > 0)
                        {
                            encounterCD--; //lowering the cooldown int to 0 for each step taken
                        }
                        if (!Input.GetKey(KeyCode.Space))
                        {
                            EncounterLoader();
                        }
                    }
                }
                anim.SetBool("moving", false);
            }
            else
            {
                anim.SetBool("moving", true);
            }   

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                MenuCanvas.SetActive(true);
                menuOpen = true;
            }
        }
        else
        {
            MenuButton.SetActive(false);
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                MenuCanvas.SetActive(false);
                menuOpen = false;
            }
        }
    }

    void EncounterLoader()
    {
        int encounterChance = rnd.Next(0, 100) + 1;
        //Debug.Log("Encounter cd: " + encounterCD);
        sceneSO.worldPos = movePoint.position;
        if (curScene == "Overworld")
        {
            if (encounterChance < 26 && encounterCD == 0) //25% chance of getting a combat encounter, and it needs the cooldown int to be 0.
            {
                sceneSO.firstRunOW = true;
                encounterCD = 15; //how many steps are taken until another encounter can be loaded
                sceneSO.encounterCD = encounterCD;
                SceneManager.LoadScene("OverworldBattleScene", LoadSceneMode.Single);
            }
        }
        //same thing as above but for the dungeon
        if (curScene == "Dungeon")
        {
            if (encounterChance < 26 && encounterCD == 0)
            {
                sceneSO.firstRunDEncounter = true;
                sceneSO.firstRunD = false;
                sceneSO.worldPos = movePoint.position;
                encounterCD = 10;
                sceneSO.encounterCD = encounterCD;
                SceneManager.LoadScene("DungeonBattleScene", LoadSceneMode.Single);
            }
        }
    }

    IEnumerator SetPos()
    {
        Debug.Log("SetPos run");
        Debug.Log(sceneSO.worldPos);
        yield return new WaitForSeconds(0.01f);
        Knightro.position = sceneSO.worldPos;
        movePoint.position = sceneSO.worldPos;
    }

    void SceneSwap()
    {
        if (curScene == "Overworld")
        {
            if (Knightro.position == new Vector3(50.5f, 13.5f, 0) || Knightro.position == new Vector3(51.5f, 13.5f, 0))
            {
                sceneSO.firstRunD = true;
                SceneManager.LoadScene("Dungeon", LoadSceneMode.Single);
            }
        }
        if (curScene == "Dungeon")
        {
            if (Knightro.position == new Vector3(0.5f, 11.5f, 0) || Knightro.position == new Vector3(-0.5f, 11.5f, 0))
            {
                SceneManager.LoadScene("Victory", LoadSceneMode.Single);
            }
        }
    }
}
