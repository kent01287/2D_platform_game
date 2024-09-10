using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBGM : MonoBehaviour
{
    public GameObject TitleBGM;
    GameObject BGM = null;
    bool Boss ;
    // Start is called before the first frame update
    void Start()
    {
        BGM = GameObject.FindGameObjectWithTag("BackGroundMusic");
        Boss = GameObject.FindGameObjectWithTag("Boss");
        if(BGM == null)
        {
            BGM = Instantiate(TitleBGM);
        }
        if(Boss)
        {
            Destroy(BGM);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
