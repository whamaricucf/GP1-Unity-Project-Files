using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using UnityEngine.SceneManagement;

public class EnemyStateMachine : MonoBehaviour
{
    private BattleStateMachine BSM;
    public Enemy enemy;
    private Vector3 startPosition;
    private bool actionStarted = false;
    public GameObject HeroToAttack;
    private float animSpeed = 5f;

    public enum TurnState
    {
        WAITING,
        CHOOSEACTION,
        ACTION,
        DEAD
    }

    public TurnState currentState;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "OverworldBattleScene" || scene.name == "DungeonBattleScene" || scene.name == "BossBattleScene")
        {
            currentState = TurnState.WAITING;
            BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
            startPosition = this.gameObject.transform.position;
        }
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case (TurnState.WAITING):

                break;
            case (TurnState.CHOOSEACTION):
                ChooseAction();
                break;
            case (TurnState.ACTION):
                StartCoroutine(TimeForAction());
                break;
            case (TurnState.DEAD):

                break;

        }
    }

    void ChooseAction()
    {
        HandleTurns myAttack = new HandleTurns();
        myAttack.Attacker = enemy.name;
        myAttack.Type = "Enemy";
        myAttack.AttackerGameObject = this.gameObject;
        myAttack.Target = BSM.HeroBattleList [UnityEngine.Random.Range(0, BSM.HeroBattleList.Count)];
        BSM.CollectActions(myAttack);
    }
    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }
        actionStarted = true;
        Vector3 attackPosition = new Vector3(this.gameObject.transform.position.x + 1f, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        while (MoveTowardsEnemy(attackPosition))
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);

        Vector3 firstPosition = startPosition;
        while (MoveTowardsStart(firstPosition))
        {
            yield return null;
        }

        BSM.PerformList.RemoveAt(0);

        BSM.battleStates = BattleStateMachine.PerformAction.WAIT;

        actionStarted = false;
        currentState = TurnState.WAITING;
    }

    private bool MoveTowardsEnemy(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }
    private bool MoveTowardsStart(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }
}
