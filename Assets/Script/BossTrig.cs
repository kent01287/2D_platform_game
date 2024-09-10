using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrig : MonoBehaviour
{
    private bool Boss;
    // Start is called before the first frame update
    void Start()
    {
        Boss = GameObject.FindGameObjectWithTag("Boss");
        if(Boss)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
