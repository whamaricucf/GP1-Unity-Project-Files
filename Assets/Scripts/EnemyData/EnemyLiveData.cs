using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyLiveData : ScriptableObject
{
    public Sprite overworld;
    public bool stunned;
    public bool backRow;
    public bool defending;
    public int lvl;
}
