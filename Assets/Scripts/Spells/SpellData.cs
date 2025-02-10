using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpellData
{
    public string spellName;
    [field: TextArea]
    public string description;
    public int MPCost;
    public int spellDmg;
    public bool freezeEffect;
    public bool burnEffect;
}
