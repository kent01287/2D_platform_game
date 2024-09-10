using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoblin : Enemy
{
    
    [Header("怪物開始等待的時間")]
    public float starWaitTime; 
    [Header("怪物留在原地等待多久")]
    private float waitTime; //等待時間
    public enum Status{idle,walk,attack,RandomWalk};
    public Status status;
    private Animator anim;
    private float lastActTime; //最近一次指令時間
    public float actRestTime;
    private Transform myTransForm;
    public Transform playerTransfrom;
    private float distanceToPlayer;
    public float[] actionWeight={6000,3000};
    public enum Face {Right,Left};
    public Face face;
    private SpriteRenderer spr;
    void RandomAction()
    {
        //更新行動時間
        lastActTime = Time.time;
        //根據權重隨機
        float number = Random.Range(0, actionWeight[0] + actionWeight[1]);
        if (number <= actionWeight[0])
        {
            status=Status.idle;

        }
        else 
        {
            status=Status.RandomWalk;
            transform.localRotation = Quaternion.Euler(0, Random.Range(0,2) * 180, 0);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        RandomAction();
        myTransForm=this.transform;
        if(GameObject.Find("player")!=null)
        {
            playerTransfrom=GameObject.Find("player").transform;
        }
        anim=GetComponent<Animator>();
        spr=this.transform.GetComponent<SpriteRenderer>();
        if(playerTransfrom)
        {
            if(spr.flipX)
            {
                face=Face.Right;
            }
            else
            {
                face=Face.Left;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        switch(status)
        {
            case Status.idle:
                distanceToPlayer=Vector3.Distance(myTransForm.position,playerTransfrom.position);
                anim.SetBool("Idle",true);
                anim.SetBool("Walk",false);
                if(playerTransfrom)
                {
                    if(Mathf.Abs(distanceToPlayer)<5f) 
                    {
                        status=Status.walk;
                    }
                }
                if(Time.time-lastActTime>actRestTime)
                {
                    RandomAction();
                }
                break;
            case Status.RandomWalk:
                distanceToPlayer=Vector3.Distance(myTransForm.position,playerTransfrom.position);
            
                anim.SetBool("Walk",true);
                anim.SetBool("Idle",false);
                if(Time.time-lastActTime>actRestTime)
                {
                    RandomAction();
                }
                if(this.gameObject.transform.rotation.y==180)
                {
                    transform.Translate(Vector3.right * Time.deltaTime * speed);
                }
                else
                {
                    transform.Translate(Vector3.left * Time.deltaTime * speed);
                }
                if(playerTransfrom)
                {
                    if(Mathf.Abs(distanceToPlayer)<5f) 
                    {
                        status=Status.walk;
                    }
                }
                break;
            case Status.walk:
                anim.SetBool("Walk",true);
                anim.SetBool("Idle",false);
                anim.SetBool("Attack",false);
                if(myTransForm.position.x>=playerTransfrom.position.x)//玩家在左邊
                {
                   /* spr.flipX=false;*/
                    face=Face.Left;
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    /*spr.flipX=true;*/
                    face=Face.Right;
                     transform.localRotation = Quaternion.Euler(0,180, 0);
                }
                switch(face)
                {
                    case Face.Right:
                        myTransForm.position+=new Vector3(speed*Time.deltaTime,0,0);
                        
                        break;
                    case Face.Left:
                        myTransForm.position+=new Vector3(-speed*Time.deltaTime,0,0);
                        
                        break;
                }
                distanceToPlayer=Vector3.Distance(myTransForm.position,playerTransfrom.position);
                if(playerTransfrom)
                {
                    if(Mathf.Abs(distanceToPlayer)>=5f)
                    {
                        status=Status.idle;
                    }
                    if(Mathf.Abs(distanceToPlayer)<1.6f)
                    {
                        status=Status.attack;
                    }
                }
                break;
            case Status attack:
                distanceToPlayer=Vector3.Distance(myTransForm.position,playerTransfrom.position);
                anim.SetBool("Attack",true);
                if(Mathf.Abs(distanceToPlayer)>=1.6f)
                {
                        status=Status.walk;
                }
                break;
        }
    }
}
