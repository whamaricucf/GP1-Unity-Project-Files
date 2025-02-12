using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SceneSO : ScriptableObject
{
    public string sceneName;
    public Vector3 worldPos;
    public int encounterCD;
    public bool firstRunOW;
    public bool firstRunD;
    public bool firstRunDEncounter;
    public float volume;
}
