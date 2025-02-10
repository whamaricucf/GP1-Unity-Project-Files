using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeroStateMachine : MonoBehaviour
{
    private BattleStateMachine BSM;

    public enum TurnState
    {
        //ADDTOLIST,
        WAITING,
        CHOOSEACTION,
        ACTION,
        DEAD
    }

    public TurnState currentState;

    void Start()
    {
        
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "OverworldBattleScene" || scene.name == "DungeonBattleScene" || scene.name == "BossBattleScene")
        {
            BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
            currentState = TurnState.WAITING;
            switch (currentState)
            {
                //case (TurnState.ADDTOLIST):

                    //currentState = TurnState.WAITING;
                    //break;
                case (TurnState.WAITING):
                    //idle
                    break;
                case (TurnState.CHOOSEACTION):

                    break;
                case (TurnState.ACTION):

                    break;
                case (TurnState.DEAD):

                    break;

            }
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
