using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{

    private AudioSource audio;

    public AudioClip MainMenu;
    public AudioClip Overworld;
    public AudioClip Dungeon;
    public AudioClip OverworldBattle;
    public AudioClip DungeonBattle;
    public AudioClip Credits;
    public AudioClip GameOver;
    public AudioClip Victory;

    [SerializeField] private SceneSO sceneSO;

    void Start()
    {
        sceneSO.volume = 0.5f;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        audio = GetComponent<AudioSource>();
        audio.volume = sceneSO.volume;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Overworld")
        {
            audio.clip = Overworld;
            audio.Play();
            audio.loop = true;
        }
        if (scene.name == "Dungeon")
        {
            audio.clip = Dungeon;
            audio.Play();
            audio.loop = true;
        }
        if (scene.name == "OverworldBattleScene")
        {
            audio.clip = OverworldBattle;
            audio.Play();
            audio.loop = true;
        }
        if (scene.name == "DungeonBattleScene")
        {
            audio.clip = DungeonBattle;
            audio.Play();
            audio.loop = true;
        }
        if (scene.name == "MainMenu")
        {
            audio.clip = MainMenu;
            audio.Play();
            audio.loop = true;
        }
        if (scene.name == "GameOver")
        {
            audio.clip = GameOver;
            audio.Play();
            audio.loop = true;
        }
        if (scene.name == "Credits")
        {
            audio.clip = Credits;
            audio.Play();
            audio.loop = true;
        }
        if (scene.name == "Victory")
        {
            audio.clip = Victory;
            audio.Play();
            audio.loop = true;
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        audio.Stop();
    }
    // Update is called once per frame
    void Update()
    {
        if (audio.volume < 1f && Input.GetKeyDown(KeyCode.O))
        {
            audio.volume += 0.1f;
        }
        if (audio.volume > 0f && Input.GetKeyDown(KeyCode.I))
        {
            audio.volume -= 0.1f;
        }
        sceneSO.volume = audio.volume;
    }
}
