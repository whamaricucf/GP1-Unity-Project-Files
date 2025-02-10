using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public EnemyID ID;
    private SpriteRenderer sprites;
    public Sprite idle;
    public Encounters encounters;
    public int curHP;
    public int curMP;

    void Start()
    {
        sprites = GetComponent<SpriteRenderer>();
        idle = ID.data.overworld;
        sprites.sprite = idle;
        curHP = ID.stats.maxHP;
        curMP = ID.stats.maxMP;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "OverworldBattleScene" || scene.name == "DungeonBattleScene" || scene.name == "BossBattleScene")
        {
            
        }
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}