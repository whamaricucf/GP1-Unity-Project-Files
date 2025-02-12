using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;
using TMPro;
using UnityEngine.UI;

public class Encounters : MonoBehaviour
{
    static Random rnd = new Random();
    private BattleStateMachine BSM;

    public EnemyID Slime;
    public EnemyID Toolin;
    public EnemyID Muskbud;
    public EnemyID EvilSlime;
    public EnemyID StinkyRat;
    public EnemyID SkeletonJuggler;
    public EnemyID SpectralHorseman;

    public GameObject Slime1Prefab;
    public GameObject Slime2Prefab;
    public GameObject Slime3Prefab;
    public GameObject Slime4Prefab;
    public GameObject ToolinPrefab;
    public GameObject MuskbudPrefab;
    public GameObject EvilSlimePrefab;
    public GameObject StinkyRatPrefab;
    public GameObject SkeletonJugglerPrefab;
    public GameObject SpectralHorsemanPrefab;

    public GameObject[] encounterEnemies;
    public int enemyNum;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject Enemy4;

    public GameObject[] enemyNameBars;
    public GameObject enemyNameBar;
    public GameObject enemyNameBar2;
    public GameObject enemyNameBar3;
    public GameObject enemyNameBar4;

    public TextMeshProUGUI name1;
    public TextMeshProUGUI name2;
    public TextMeshProUGUI name3;
    public TextMeshProUGUI name4;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "OverworldBattleScene")
        {
            Debug.Log("Overworld Battle Scene Loaded");
            OverworldEncounters();
            BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        }
        if (scene.name == "DungeonBattleScene")
        {
            Debug.Log("Dungeon Battle Scene Loaded");
            OverworldEncounters();
            BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OverworldEncounters()
    {
        int encounterRandomizer = rnd.Next(0, 200);
        if (encounterRandomizer < 49)
        {
            Debug.Log("Encounter 1");
            //4 Slimes
            enemyNum = 4;
            Enemy1 = Slime1Prefab;
            Enemy2 = Slime2Prefab;
            Enemy3 = Slime3Prefab;
            Enemy4 = Slime4Prefab;
            Enemy1.transform.position = new Vector3(-3.24f, 0.55f, 0);
            Enemy2.transform.position = new Vector3(-2.49f, -1.11f, 0);
            Enemy3.transform.position = new Vector3(-6.02f, 0.03f, 0);
            Enemy4.transform.position = new Vector3(-5.19f, -1.43f, 0);
            GameObject[] encounterEnemies = { Enemy1, Enemy2, Enemy3, Enemy4};
            GameObject[] enemyNameBars = { enemyNameBar, enemyNameBar2, enemyNameBar3, enemyNameBar4 };
            name1.text = "Slime";
            name2.text = "Slime2";
            name3.text = "Slime3";
            name4.text = "Slime4";
            for (int i = 0; i < enemyNum; i++)
            {
                Instantiate(encounterEnemies[i]);
            }
            Enemy1.transform.position = new Vector3(-3.24f, 0.55f, 0);
            Enemy1.transform.position = new Vector3(-3.24f, 0.55f, 0);
            Enemy1.transform.position = new Vector3(-3.24f, 0.55f, 0);
            Enemy1.transform.position = new Vector3(-3.24f, 0.55f, 0);
            Enemy1.transform.position = new Vector3(-3.24f, 0.55f, 0);
            Enemy1.transform.position = new Vector3(-3.24f, 0.55f, 0);
            Enemy2.transform.position = new Vector3(-2.49f, -1.11f, 0);
            Enemy3.transform.position = new Vector3(-6.02f, 0.03f, 0);
            Enemy4.transform.position = new Vector3(-5.19f, -1.43f, 0);
            foreach (GameObject enemy in enemyNameBars)
            {
                enemy.gameObject.SetActive(true);
            }
        } else if (encounterRandomizer < 99)
        {
            Debug.Log("Encounter 2");
            //1 Toolin 2 Slimes
            enemyNum = 3;
            Enemy1 = ToolinPrefab;
            Enemy1.transform.position = new Vector3(-3.24f, 0.55f, 0);
            Enemy2 = Slime1Prefab;
            Enemy2.transform.position = new Vector3(-2.49f, -1.11f, 0);
            Enemy3 = Slime2Prefab;
            Enemy3.transform.position = new Vector3(-6.02f, 0.03f, 0);
            GameObject[] encounterEnemies = { Enemy1, Enemy2, Enemy3};
            GameObject[] enemyNameBars = { enemyNameBar, enemyNameBar2, enemyNameBar3 };
            name1.text = "Toolin";
            name2.text = "Slime";
            name3.text = "Slime2";
            for (int i = 0; i < enemyNum; i++)
            {
                Instantiate(encounterEnemies[i]);
            }
            Enemy1.transform.position = new Vector3(-3.24f, 0.55f, 0);
            Enemy2.transform.position = new Vector3(-2.49f, -1.11f, 0);
            Debug.Log(Enemy2);
            Enemy3.transform.position = new Vector3(-6.02f, 0.03f, 0);
            foreach (GameObject enemy in enemyNameBars)
            {
                enemy.gameObject.SetActive(true);
            }
        } else if (encounterRandomizer < 149)
        {
            Debug.Log("Encounter 3");
            //1 Toolin 1 Muskbud
            enemyNum = 2;
            Enemy1 = ToolinPrefab;
            Enemy1.transform.position = new Vector3(-3.24f, 0.55f, 0);
            Enemy2 = MuskbudPrefab;
            Enemy2.transform.position = new Vector3(-2.49f, -1.11f, 0);
            GameObject[] encounterEnemies = { Enemy1, Enemy2};
            GameObject[] enemyNameBars = { enemyNameBar, enemyNameBar2 };
            name1.text = "Toolin";
            name2.text = "Muskbud";
            for (int i = 0; i < enemyNum; i++)
            {
                Instantiate(encounterEnemies[i]);
            }
            Enemy1.transform.position = new Vector3(-3.24f, 0.55f, 0);
            Enemy2.transform.position = new Vector3(-2.49f, -1.11f, 0);
            foreach (GameObject enemy in enemyNameBars)
            {
                enemy.gameObject.SetActive(true);
            }
        } else
        {
            Debug.Log("Encounter 4");
            //1 Muskbud 3 Slimes
            enemyNum = 4;
            Enemy1 = Slime1Prefab;
            Enemy2 = Slime2Prefab;
            Enemy3 = MuskbudPrefab;
            Enemy4 = Slime3Prefab;
            Enemy1.transform.position = new Vector3(-3.24f, 0.55f, 0);
            Enemy2.transform.position = new Vector3(-2.49f, -1.11f, 0);
            Enemy3.transform.position = new Vector3(-6.02f, 0.03f, 0);
            Enemy4.transform.position = new Vector3(-5.19f, -1.43f, 0);
            GameObject[] encounterEnemies = { Enemy1, Enemy2, Enemy3, Enemy4};
            GameObject[] enemyNameBars = { enemyNameBar, enemyNameBar2, enemyNameBar3, enemyNameBar4 };
            name1.text = "Slime";
            name2.text = "Slime2";
            name3.text = "Muskbud";
            name4.text = "Slime3";
            for (int i = 0; i < enemyNum; i++)
            {
                Instantiate(encounterEnemies[i]);
            }
            Enemy1.transform.position = new Vector3(-3.24f, 0.55f, 0);
            Enemy1.transform.position = new Vector3(-3.24f, 0.55f, 0);
            Enemy1.transform.position = new Vector3(-3.24f, 0.55f, 0);
            Enemy1.transform.position = new Vector3(-3.24f, 0.55f, 0);
            Enemy1.transform.position = new Vector3(-3.24f, 0.55f, 0);
            Enemy1.transform.position = new Vector3(-3.24f, 0.55f, 0);
            Enemy1.transform.position = new Vector3(-3.24f, 0.55f, 0);
            Enemy2.transform.position = new Vector3(-2.49f, -1.11f, 0);
            Enemy3.transform.position = new Vector3(-6.02f, 0.03f, 0);
            Enemy4.transform.position = new Vector3(-5.19f, -1.43f, 0);
            foreach (GameObject enemy in enemyNameBars)
            {
                enemy.gameObject.SetActive(true);
            }
        }
    }

    void DungeonEncounters()
    {

    }

    void MiniBossBattle()
    {

    }
    
    void BossBattle()
    {

    }
}
