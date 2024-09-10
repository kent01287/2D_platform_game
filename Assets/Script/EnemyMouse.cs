using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMouse : Enemy
{
    // Start is called before the first frame update
    public float[] actionWeight={3000,3000};
    private float lastActTime; //最近一次指令時間
    private Animator myAnim;
    private enum MonsterState
    {
       STAND,      //原地呼吸
        WALK,       //移動
    }
    private MonsterState currentState = MonsterState.STAND;
    public float actRestTime;
    private Transform myTransForm;
    void Start()
    {
        base.Start();
        
        myTransForm=this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        switch(currentState)
        {
            case MonsterState.STAND:
            if(Time.time-lastActTime>actRestTime)
                {
                    RandomAction();
                }
                break;
            case MonsterState.WALK:
                 if(myTransForm.rotation.y==0)
                {
                    /*transform.Translate(Vector3.right * Time.deltaTime * speed);*/
                    myTransForm.position+=new Vector3(-speed*Time.deltaTime,0,0);
                }
                else
                {
                    myTransForm.position+=new Vector3(speed*Time.deltaTime,0,0);
                }
                
                if (Time.time - lastActTime > actRestTime)
                {
                    RandomAction();         //隨機切換指令
                }
                break;
        }
    }
    void RandomAction()
    {
        //更新行動時間
        lastActTime=Time.time;
        //根據權重隨機
        float number= Random.Range(0,actionWeight[0] + actionWeight[1]);
        if(number<=actionWeight[0])
        {
            currentState=MonsterState.STAND;
        }
        else 
        {
            //隨機一個朝向
            transform.localRotation = Quaternion.Euler(0,Random.Range(0,2)*180,0);
            currentState = MonsterState.WALK;
        }
        
    }
}
