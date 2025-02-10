using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerLiveData : ScriptableObject
{
    public string className;
    public string heroName;
    public Sprite overworld;
    public Sprite menu;
    public int partySlot;
    public bool inParty;
    public bool backRow;
    public bool canDoMagic;
    public bool dead;
    public int lvl;
    public bool defending;
    public bool stunned;
    public Vector3 currentPosition;
    public bool knowsFire1;
    public bool knowsFire2;
    public bool knowsIce1;
    public bool knowsIce2;
    public bool knowsZap1;
    public bool knowsZap2;
    public bool knowsCure;
    public bool knowsAssess;
}
