using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet1Hit : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet1Prefab;
    public GameObject bullet2Prefab;
    public Transform myTransForm;
    void Start()
    {
        myTransForm=this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void shoot()
    {
        myTransForm.position=new Vector3(myTransForm.position.x,Random.Range(-2,1f),myTransForm.position.z);
        Instantiate(bullet1Prefab,myTransForm.position,transform.rotation);
    }
    public void shoot2()
    {
        
        Instantiate(bullet2Prefab,myTransForm.position,transform.rotation);
    }
}
