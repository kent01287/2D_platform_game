using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    static public string PrevEntranceID = null;
    static public bool HaveShoes = false;
    static public bool HaveKey = false;
    static public int HP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().Health;
    static public int HPOrigin = HP;
}

