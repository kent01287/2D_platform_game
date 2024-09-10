using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    // Start is called before the first frame update
    private Transform myTransForm;
    public Transform playerTransfrom;
    private SpriteRenderer spr;
    private Animator anim;
    public enum Status{idle,attack};
    public Status status;
    private float distanceToPlayer;
    private bullet1 endAttack;
    [Header("施法時間")]
    public float AttackWaitTime;
    public float AttackWaitTime2;
    void Start()
    {
        base.Start();
        if(GameObject.Find("player")!=null)
        {
            playerTransfrom=GameObject.Find("player").transform;
        }
        anim=GetComponent<Animator>();
        spr=this.transform.GetComponent<SpriteRenderer>();
        myTransForm=this.transform;
        endAttack = GetComponent<bullet1>();
        /*status=Status.idle;*/
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        switch(status)
        {
            case Status.idle:
            print("Idle");
                distanceToPlayer=Vector3.Distance(myTransForm.position,playerTransfrom.position);
                if(playerTransfrom)
                {
                    if(Mathf.Abs(distanceToPlayer)<10f) 
                    {
                        status=Status.attack;
                    }
                }
                break;
            case Status.attack:
            if(myTransForm.position.x>=playerTransfrom.position.x)//玩家在左邊
                {
                    transform.localRotation = Quaternion.Euler(0,0, 0);
                }
                else
                {
                    transform.localRotation = Quaternion.Euler(0,180, 0);
                }
            if(playerTransfrom)
                {
                    if(Mathf.Abs(distanceToPlayer)>=10f) 
                    {
                        status=Status.idle;
                    }
                }
                break;
        }
    }
    public void StartAttack()
    {
        float towardsactivate = myTransForm.position.x - playerTransfrom.position.x;//偵測面對方向
        if(towardsactivate < 0.1f )//Flip
        {
            transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else if(towardsactivate > 0.1f )//Flip
        {
            transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        StartCoroutine(AttackTime());
        if(health<=40)
        {
            print("血量<=25");
            spr.color=Color.red;
            StartCoroutine(AttackTime2());
        }
    }
    IEnumerator AttackTime()
    {
        Usebullet1();
        yield return new WaitForSeconds(AttackWaitTime);
        endAttack.enabled = true;
    }
    IEnumerator AttackTime2()
    {
        Usebullet2();
        yield return new WaitForSeconds(AttackWaitTime2);
        print("發射bullet2");
        endAttack.enabled = true;
    }
    void Usebullet1()
    {
        GameObject.FindGameObjectWithTag("bullet1").GetComponent<bullet1Hit>().shoot();
    }
    void Usebullet2()
    {
        GameObject.FindGameObjectWithTag("bullet1").GetComponent<bullet1Hit>().shoot2();
    }
}
