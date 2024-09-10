using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireBallGhost : Enemy
{
    [Header("鬼魂偵測玩家半徑")]
    public float radius;
    
    [Header("移動後停留時間")]
    public float startWaitTime;
    private float waitTime;//蝙蝠到達目標位置停留多久   
    [Header("施法時間")]
    public float AttackWaitTime;
    
    [Header("鬼魂移動範圍限制")]
    public Transform MovePos;
    public Transform LeftDownPos;
    public Transform RightUpPos;
    private float movespeed;
    private Transform PlayerTransform;
    private Animator anim;
    private Fireball endAttack;
    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        waitTime = startWaitTime;
        movespeed = speed;
        MovePos.position = GetRandomPos();//目標位置
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        endAttack = GetComponent<Fireball>();
        anim.SetBool("Injured", false);
    }

    // Update is called once per frame
    public void Update()
    {
        base.Update();//使用父類Update
        MovementsandFlip();
        
    }
    Vector2 GetRandomPos()
    {
        Vector2 rndPos = new Vector2(Random.Range(LeftDownPos.position.x, RightUpPos.position.x), Random.Range(LeftDownPos.position.y, RightUpPos.position.y));//生成隨機位置
        return rndPos;
    }
    void MovementsandFlip()
    {
        if(PlayerTransform != null)
        {
            float distance = (transform.position - PlayerTransform.position).sqrMagnitude;//偵測與玩家的距離

            if(distance < radius)//距離小於設定，啟動跟隨
            {
                float towardsactivate = transform.position.x - PlayerTransform.position.x;//偵測面對方向
                
                transform.position = Vector2.MoveTowards(transform.position, PlayerTransform.position, movespeed * Time.deltaTime);//朝玩家移動
                if(towardsactivate < 0.1f )//Flip
                {
                    transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                }
                else if(towardsactivate > 0.1f )//Flip
                {
                    transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                }
            }
            else if(distance > radius )//巡邏模式  && anim.GetBool("Attack") == false
            {
                float towardsnothing = transform.position.x - MovePos.position.x ;//偵測面對方向
                transform.position = Vector2.MoveTowards(transform.position, MovePos.position, movespeed * Time.deltaTime);//鬼魂移動方式
                if(towardsnothing < 0.1f && towardsnothing != 0)//Flip
                {
                    transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                    
                }
                else if(towardsnothing > 0.1f)//Flip
                {
                    transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                }
                if(Vector2.Distance(transform.position, MovePos.position) < 0.1f)//若當前位置極靠近目標位置，生成一新目標
                {
                    if(waitTime <= 0)//到達位置後停留 
                    {
                        MovePos.position = GetRandomPos();
                        waitTime = startWaitTime;
                    }
                    else
                    {
                        waitTime -= Time.deltaTime;
                    }
                }                      
            }
            
        }
    }
    public void StartAttack()
    {
        anim.SetTrigger("Attack");
        MovePos.position = transform.position;
        movespeed = 0;
        float towardsactivate = transform.position.x - PlayerTransform.position.x;//偵測面對方向
        if(towardsactivate < 0.1f )//Flip
        {
            transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else if(towardsactivate > 0.1f )//Flip
        {
            transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        StartCoroutine(AttackTime());
    }
    IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(AttackWaitTime);
        movespeed = speed;
        anim.SetBool("Attack", false);
        endAttack.enabled = true;
    }
    void UseFire()
    {
        GameObject.FindGameObjectWithTag("FireBall").GetComponent<FireBallHit>().Fire();
    }
}

