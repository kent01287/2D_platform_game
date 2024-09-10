using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVampire : Enemy
{
    // Start is called before the first frame update
    private GameObject playerUnit; //獲取玩家物件
    private Animator myAnim; //獲得自身動畫組件
    private Vector3 initialPostion; //初始化位置
    [Header("遊走半徑")]
    public float wanderRadius;
    [Header("警戒半徑")]
    public float alertRadius;
    [Header("自衛半徑")]
    public float defendRadius;
    [Header("追擊半徑")]
    public float chaseRadius;
    [Header("攻擊距離")]
    public float attackRange;

    private enum MonsterState
    {
       STAND,      //原地呼吸
        CHECK,       //原地觀察
        WALK,       //移動
        CHASE,      //追擊玩家
        RETURN      //超出追擊範圍後返回
    }
     private MonsterState currentState = MonsterState.STAND;//默認狀態爲原地呼吸
     public float[] actionWeight={3000,3000,4000};
     [Header("更換待機指令的間隔時間")]
     public float actRestTime;
     private float lastActTime; //最近一次指令時間
     private float distanceToPlayer;//怪物與玩家距離
     private float distanceToInitial;//怪物與初始目標距離
     private Quaternion targetRotation;         //怪物的目標朝向
     private Transform myTransForm;
    public Transform playerTransfrom;
    void Start()
    {
        base.Start();
        playerUnit = GameObject.FindGameObjectWithTag("Player");
        myAnim=GetComponent<Animator>();
        //保存初始位置信息
        initialPostion = gameObject.GetComponent<Transform>().position;
        //檢查並修正怪物設置
        //1. 自衛半徑不大於警戒半徑，否則就無法觸發警戒狀態，直接開始追擊了
        defendRadius= Mathf.Min(alertRadius, defendRadius);
        //2. 攻擊距離不大於自衛半徑，否則就無法觸發追擊狀態，直接開始戰鬥了
        attackRange = Mathf.Min(defendRadius, attackRange);
        //3. 遊走半徑不大於追擊半徑，否則怪物可能剛剛開始追擊就返回出生點
        wanderRadius = Mathf.Min(chaseRadius, wanderRadius);

        //隨機一個待機動作
        RandomAction();
        myTransForm=this.transform;
        if(GameObject.Find("player")!=null)
        {
            playerTransfrom=GameObject.Find("player").transform;
        }
    }
    void RandomAction()
    {
        //更新行動時間
        lastActTime=Time.time;
        //根據權重隨機
        float number= Random.Range(0,actionWeight[0] + actionWeight[1] + actionWeight[2]);
        if(number<=actionWeight[0])
        {
            currentState=MonsterState.STAND;
            myAnim.SetTrigger("Idle");
        }
        else if (actionWeight[0] < number && number <= actionWeight[0] + actionWeight[1])
        {
            currentState = MonsterState.CHECK;
            myAnim.SetTrigger("Check");
        }
        if (actionWeight[0] + actionWeight[1] < number && number <= actionWeight[0] + actionWeight[1] + actionWeight[2])
        {
            currentState = MonsterState.WALK;
            //隨機一個朝向
            transform.localRotation = Quaternion.Euler(0,Random.Range(0,2)*180,0);
            myAnim.SetTrigger("Idle");
        }
    }
    // Update is called once per frame
    void Update()
    {
        base.Update();
        switch(currentState)
        {
            case MonsterState.STAND:
                myAnim.SetTrigger("Idle");
                if(Time.time-lastActTime>actRestTime)
                {
                    RandomAction();
                }
                EnemyDistanceCheck();
                break;
            case MonsterState.CHECK:
                myAnim.SetTrigger("Check");
                if(Time.time-lastActTime>actRestTime)
                {
                    RandomAction();
                }
                EnemyDistanceCheck();
                break;
            case MonsterState.WALK:
                print("walk");
                myAnim.SetTrigger("Idle");
                if(this.gameObject.transform.rotation.y==-180)
                {
                    /*transform.Translate(Vector3.right * Time.deltaTime * speed);*/
                    myTransForm.position+=new Vector3(speed*Time.deltaTime,0,0);
                }
                else 
                {
                    /*transform.Translate(Vector3.left * Time.deltaTime * speed);*/
                    myTransForm.position+=new Vector3(-speed*Time.deltaTime,0,0);
                }
                if (Time.time - lastActTime > actRestTime)
                {
                    RandomAction();         //隨機切換指令
                }
                //該狀態下的檢測指令
                WanderRadiusCheck();
                break;
            //追擊狀態,朝著玩家跑去
            case MonsterState.CHASE:
                print("Chase");
                print(distanceToPlayer);
                myAnim.SetTrigger("Idle");
                //朝向玩家位置
               if(myTransForm.position.x>=playerTransfrom.position.x)//玩家在左邊
                {
                    transform.localRotation = Quaternion.Euler(0,0, 0);
                    myTransForm.position+=new Vector3(-speed*Time.deltaTime,0,0);
                }
                else
                {
                    transform.localRotation = Quaternion.Euler(0,180, 0);
                    myTransForm.position+=new Vector3(speed*Time.deltaTime,0,0);
                }
                //該狀態下的檢測指令
                ChaseRadiusCheck();
                break;
            //返回狀態,超出追擊範圍後返回出生位置
            /*case MonsterState.RETURN:
                //朝向初始位置移動
                print("return");
                if(myTransForm.position.x>=initialPostion.x)//起始點在左邊
                {
                    transform.localRotation = Quaternion.Euler(0,0, 0);
                    myTransForm.position=Vector3.MoveTowards(myTransForm.position,initialPostion,speed*Time.deltaTime);
                }
                else
                {
                    transform.localRotation = Quaternion.Euler(0,180, 0);
                    myTransForm.position=Vector3.MoveTowards(myTransForm.position,initialPostion,-speed*Time.deltaTime);
                }
                //該狀態下的檢測指令
                ReturnCheck();
                break;*/
        }
    }
     void EnemyDistanceCheck()
        {
            distanceToPlayer=Vector3.Distance(playerUnit.transform.position,transform.position);
            if(distanceToPlayer<attackRange)
            {
                myAnim.SetTrigger("Attack");
            }
            else if(distanceToPlayer<defendRadius)
            {
                currentState=MonsterState.CHASE;
            }
            
        }
        /// <summary>
        /// 遊走狀態檢測，檢測敵人距離及遊走是否越界
        /// </summary>
        void WanderRadiusCheck()
        {
            distanceToPlayer=Vector3.Distance(playerUnit.transform.position,transform.position);
            distanceToInitial=Vector3.Distance(transform.position,initialPostion);
            if(distanceToPlayer<attackRange)
            {
                myAnim.SetTrigger("Attack");
            }
            else if(distanceToPlayer<defendRadius)
            {
                currentState=MonsterState.CHASE;
            }
            if(distanceToInitial>wanderRadius)
            {
                //調整為初始方向
                targetRotation=Quaternion.LookRotation(initialPostion-transform.position,Vector3.up);
            }
        }
        /// <summary>
        // 追擊狀態檢測，檢測敵人是否進入攻擊範圍以及是否離開警戒範圍
        // </summary>
      void ChaseRadiusCheck()
      {
            distanceToPlayer=Vector3.Distance(playerUnit.transform.position,transform.position);
            distanceToInitial=Vector3.Distance(transform.position,initialPostion);
            if(distanceToPlayer<attackRange)
            {
                myAnim.SetTrigger("Attack");
            }
            //如果超出追擊範圍或者敵人的距離超出警戒距離就返回
            /*if (distanceToInitial > chaseRadius || distanceToPlayer > alertRadius)
            {
                currentState = MonsterState.RETURN;
            }*/
      }
      /// <summary>
    /// 超出追擊半徑，返回狀態的檢測，不再檢測敵人距離
    /// </summary>
        void ReturnCheck()
        {

            distanceToInitial = Vector3.Distance(transform.position,initialPostion);
            //如果已經接近初始位置，則隨機一個待機狀態
            if (distanceToInitial < 0.7f)
            {
                RandomAction();
            }
        }
}

