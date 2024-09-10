using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float DoubleJumpSpeed;
    
    private Rigidbody2D myRigidbody;
    private Animator myAnim;
    private BoxCollider2D myFeet;
    private bool isGround;
    private bool canDoubleJump;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();//初始化
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Flip(); 
        Jump();
        CheckGrounded();
        //Attack();
        SwitchAnimation();
        
    }
     void CheckGrounded() //確認是否在地上
     {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
                   myFeet.IsTouchingLayers(LayerMask.GetMask("MovingPlatform")) ||
                   myFeet.IsTouchingLayers(LayerMask.GetMask("Spike"));
        //Debug.Log(isGround);
    }
     void Flip()//轉向
    {
        /*if(Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D))//右鍵和D不選轉
        {
            GetComponent<SpriteRenderer>().flipX=false;
            
        }
        if(Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A))//左鍵和A選轉
        {
            GetComponent<SpriteRenderer>().flipX=true;
        }*/
        bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;//若水平方向有值則設為True
        if(playerHasXAxisSpeed)
        {
            if(myRigidbody.velocity.x > 0.1f)//向右不須翻轉
            {
                transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }
            if(myRigidbody.velocity.x < -0.1f)//向左翻轉
            {
                transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            }
        }
    }
   
    void Run()
    {
        float moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVel; //抓取速度
        bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;//若水平方向有值則設為True
        myAnim.SetBool("Run", playerHasXAxisSpeed);//playerHasXAxisSpeed若True則Run為True，啟動動畫
    }
    
    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(isGround)//一段跳
            {
                myAnim.SetBool("Jump", true);
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                myRigidbody.velocity = Vector2.up * jumpVel;
                canDoubleJump = true;
            }
            else
            {
                if(canDoubleJump && GameData.HaveShoes)//二段跳 
                {
                    myAnim.SetBool("DoubleJump", true);
                    Vector2 DoubleJumpVel = new Vector2(0.0f, DoubleJumpSpeed);
                    myRigidbody.velocity = Vector2.up * DoubleJumpVel;
                    canDoubleJump = false;
                }
            }
        }
    }
    /*void Attack()
    {
        if(Input.GetButton("Attack"))
        {
            myAnim.SetTrigger("Attack");
        }
    }*/
    void SwitchAnimation()
    {
        myAnim.SetBool("Idle", false);
        if(myAnim.GetBool("Jump"))
        {
            if(myRigidbody.velocity.y < 0.0f)//啟動fall動畫
            {
                myAnim.SetBool("Jump", false);
                myAnim.SetBool("Fall", true);
            }
        }
        else if(isGround)//返回Idle動畫
        {
                myAnim.SetBool("Fall", false);
                myAnim.SetBool("Idle", true);
        }
    
        if(myAnim.GetBool("DoubleJump"))
        {
            if(myRigidbody.velocity.y < 0.0f)//啟動Doublefall動畫
            {
                myAnim.SetBool("DoubleJump", false);
                myAnim.SetBool("DoubleFall", true);
            }
        }
        else if(isGround)//返回Idle動畫
        {
                myAnim.SetBool("DoubleFall", false);
                myAnim.SetBool("Idle", true);
        }
    }
}
