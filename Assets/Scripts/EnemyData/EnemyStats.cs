using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStats : ScriptableObject
{
    public string enemyName;
    public int maxHP;
    public int maxMP;
    public int str;
    public int mag;
    public int def;
    public int agi;
    public int exp;
}
