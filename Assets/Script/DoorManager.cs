using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public List<ToNextLevel> DoorList;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        string eid = GameData.PrevEntranceID; 
        ToNextLevel door = GetDoorByID(eid);//用ID到對應的門
        if(door)//向門口要座標
        {
            Vector2 pos = door.GetFrontPosition(); 
            player.transform.position = pos;
        }
        
    }
    ToNextLevel GetDoorByID(string eid) 
    {
        foreach(ToNextLevel door in DoorList)
        {
            if(door.id == eid) 
            {
                return door;
                
            }
        }
        return null;
    }
}
