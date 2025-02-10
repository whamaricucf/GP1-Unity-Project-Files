using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Spells : ScriptableObject
{
    public string spellName;
    [field: TextArea]
    public string description;
    public int MPCost;
    public int spellDmg;
    public GameObject sprite;
    public bool freezeEffect;
    public bool burnEffect;
}
