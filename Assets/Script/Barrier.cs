using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BossIsDie();
    }
    void BossIsDie()
    {
        bool bossgone = GameObject.FindGameObjectWithTag("Enemy");
        if(bossgone == false)
        {
            Destroy(gameObject);
        }
    }
}
