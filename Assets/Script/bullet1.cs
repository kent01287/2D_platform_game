using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet1 : MonoBehaviour
{
    private Animator anim;
    private Transform PlayerTransform;
    private EnemyBoss start;
    private float distanceToPlayer;
    private Transform myTransform;
    void Start()
    {
        if(GameObject.Find("player")!=null)
        {
            PlayerTransform=GameObject.Find("player").transform;
        }
        anim = GetComponent<Animator>();
        myTransform=this.transform;
        start = GetComponent<EnemyBoss>();
    }

    // Update is called once per frame
    void Update()
    {
         AttackTrig();
    }
    void AttackTrig()
    {
        print(distanceToPlayer);
        distanceToPlayer=Vector3.Distance(myTransform.position,PlayerTransform.position);
        if(distanceToPlayer<=10)
        {
            print("開始進攻");
            start.StartAttack();
            this.enabled = false;
        }
    }
}
