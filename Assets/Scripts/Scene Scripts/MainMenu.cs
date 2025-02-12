using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public PlayerID Knightro;
    public PlayerID Pegasus;
    public PlayerID RubberDuck;
    public PlayerID Citronaut;
    [SerializeField] private SceneSO sceneSO;
    private int turnChoice;
    public GameObject AP;

    void OnEnable()
    {
        turnChoice = 1;
        sceneSO.encounterCD = 15;
        sceneSO.firstRunOW = false;
        sceneSO.firstRunD = false;
        sceneSO.firstRunD = true;
        sceneSO.firstRunDEncounter = false;

        Knightro.data.partySlot = 1;
        Knightro.data.inParty = true;
        Knightro.data.backRow = false;
        Knightro.data.canDoMagic = false;
        Knightro.data.dead = false;
        Knightro.data.lvl = 5;
        Knightro.data.defending = false;
        Knightro.data.stunned = false;
        Knightro.data.knowsFire1 = false;
        Knightro.data.knowsFire2 = false;
        Knightro.data.knowsIce1 = false;
        Knightro.data.knowsIce2 = false;
        Knightro.data.knowsZap1 = false;
        Knightro.data.knowsZap2 = false;
        Knightro.data.knowsCure = false;
        Knightro.data.knowsAssess = false;

        Pegasus.data.partySlot = 2;
        Pegasus.data.inParty = true;
        Pegasus.data.backRow = true;
        Pegasus.data.canDoMagic = true;
        Pegasus.data.dead = false;
        Pegasus.data.lvl = 5;
        Pegasus.data.defending = false;
        Pegasus.data.stunned = false;
        Pegasus.data.knowsFire1 = false;
        Pegasus.data.knowsFire2 = false;
        Pegasus.data.knowsIce1 = false;
        Pegasus.data.knowsIce2 = false;
        Pegasus.data.knowsZap1 = false;
        Pegasus.data.knowsZap2 = false;
        Pegasus.data.knowsCure = true;
        Pegasus.data.knowsAssess = true;

        RubberDuck.data.partySlot = 3;
        RubberDuck.data.inParty = true;
        RubberDuck.data.backRow = true;
        RubberDuck.data.canDoMagic = true;
        RubberDuck.data.dead = false;
        RubberDuck.data.lvl = 5;
        RubberDuck.data.defending = false;
        RubberDuck.data.stunned = false;
        RubberDuck.data.knowsFire1 = true;
        RubberDuck.data.knowsFire2 = false;
        RubberDuck.data.knowsIce1 = true;
        RubberDuck.data.knowsIce2 = false;
        RubberDuck.data.knowsZap1 = true;
        RubberDuck.data.knowsZap2 = false;
        RubberDuck.data.knowsCure = false;
        RubberDuck.data.knowsAssess = false;

        Citronaut.data.partySlot = 4;
        Citronaut.data.inParty = true;
        Citronaut.data.backRow = false;
        Citronaut.data.canDoMagic = false;
        Citronaut.data.dead = false;
        Citronaut.data.lvl = 5;
        Citronaut.data.defending = false;
        Citronaut.data.stunned = false;
        Citronaut.data.knowsFire1 = false;
        Citronaut.data.knowsFire2 = false;
        Citronaut.data.knowsIce1 = false;
        Citronaut.data.knowsIce2 = false;
        Citronaut.data.knowsZap1 = false;
        Citronaut.data.knowsZap2 = false;
        Citronaut.data.knowsCure = false;
        Citronaut.data.knowsAssess = false;

        Knightro.stats.HP = Knightro.stats.maxHP;
        Knightro.stats.MP = Knightro.stats.maxMP;

        Pegasus.stats.HP = Pegasus.stats.maxHP;
        Pegasus.stats.MP = Pegasus.stats.maxMP;

        RubberDuck.stats.HP = RubberDuck.stats.maxHP;
        RubberDuck.stats.MP = RubberDuck.stats.maxMP;

        Citronaut.stats.HP = Citronaut.stats.maxHP;
        Citronaut.stats.MP = Citronaut.stats.maxMP;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            turnChoice++;
            if (turnChoice > 2)
            {
                turnChoice = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            turnChoice--;
            if (turnChoice < 1)
            {
                turnChoice = 2;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.G))
        {
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }

        switch (turnChoice)
        {
            case 1:
                AP.transform.position = new Vector3(-2.33f, -1.24f, 0);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("Overworld", LoadSceneMode.Single);
                }
                break;
            case 2:
                AP.transform.position = new Vector3(-1.83f, -2.81f, 0);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("Credits", LoadSceneMode.Single);
                }
                break;
        }
    }
}
