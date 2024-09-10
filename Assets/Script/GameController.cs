using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static CameraShake CamShake;
    void Start()
    {
        if(GameData.HaveShoes)
        {
            Destroy(GameObject.FindGameObjectWithTag("Shoes"));
        }
        if(GameData.HaveKey)
        {
            Destroy(GameObject.FindGameObjectWithTag("Key"));
        }
    }
}
