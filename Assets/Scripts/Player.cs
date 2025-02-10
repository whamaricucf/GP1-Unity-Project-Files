using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerID ID;
    private SpriteRenderer sprites;
    public Sprite idle;
    public int slot;
    public float partySlot;
    public bool backRow;
    public bool defending;
    public Vector3 heroPosition;
    public bool dead;

    void Start()
    {
        sprites = GetComponent<SpriteRenderer>();
        idle = ID.data.overworld;
        slot = ID.data.partySlot;
        dead = ID.data.dead;
        backRow = ID.data.backRow;
        partySlot = (float)slot;
        sprites.sprite = idle;
        sprites.sortingOrder = slot;
        defending = false;
    }

    void Update()
    {
        ID.data.currentPosition = heroPosition;
    }
}
