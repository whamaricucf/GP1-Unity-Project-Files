using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PartyChecker : MonoBehaviour
{
    public PlayerID Knightro;
    public PlayerID Pegasus;
    public PlayerID RubberDuck;
    public PlayerID Citronaut;

    public GameObject KnightroPrefab;
    public GameObject PegasusPrefab;
    public GameObject RubberDuckPrefab;
    public GameObject CitronautPrefab;

    private bool KParty;
    private bool PParty;
    private bool RDParty;
    private bool CParty;
    private int KSlot;
    private int PSlot;
    private int RDSlot;
    private int CSlot;

    private int heroNum;

    private bool alreadyRun = false;

    void Start()
    {
        
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!alreadyRun)
        {
            if (scene.name == "OverworldBattleScene" || scene.name == "DungeonBattleScene" || scene.name == "BossBattleScene")
            {
                KParty = Knightro.data.inParty;
                PParty = Pegasus.data.inParty;
                RDParty = RubberDuck.data.inParty;
                CParty = Citronaut.data.inParty;
                KSlot = Knightro.data.partySlot;
                PSlot = Pegasus.data.partySlot;
                RDSlot = RubberDuck.data.partySlot;
                CSlot = Citronaut.data.partySlot;
                InPartyChecker(KParty);
                InPartyChecker(PParty);
                InPartyChecker(RDParty);
                InPartyChecker(CParty);
                for (int i = 0; i < heroNum + 1; i++)
                {
                    if (KParty && KSlot == i)
                    {
                        Instantiate(KnightroPrefab);
                    }
                    if (PParty && PSlot == i)
                    {
                        Instantiate(PegasusPrefab);
                    }
                    if (RDParty && RDSlot == i)
                    {
                        Instantiate(RubberDuckPrefab);
                    }
                    if (CParty && CSlot == i)
                    {
                        Instantiate(CitronautPrefab);
                    }
                }
            }
            alreadyRun = true;
        }
    }


    int InPartyChecker(bool inParty)
    {
        if (inParty)
        {
            heroNum++;
        }
        return heroNum;
    }
}
