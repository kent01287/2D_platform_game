using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    public float time;
    public float starttime;
    
    private Animator anim;
    private PolygonCollider2D coll2D;

    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        coll2D = GetComponent<PolygonCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }
    void Attack()
    {
        //if(Input.GetButton("Attack"))
        //{
          //  anim.SetTrigger("Attack");
          //  StartCoroutine(startAttack());
        //}
        if(Input.GetMouseButtonDown(0))//Input.GetKeyDown(KeyCode.J))
        {
            anim.SetTrigger("Attack");
            SoundManager.PlayattackClip();
            StartCoroutine(startAttack());
        }
    }
    
    IEnumerator startAttack()//開始攻擊
    {
        yield return new WaitForSeconds(starttime);
        coll2D.enabled = true;//啟動hitbox
        StartCoroutine(disableHitBox());
    }
    IEnumerator disableHitBox()//結束攻擊
    {
        yield return new WaitForSeconds(time);
        coll2D.enabled = false;//關閉hitbox
    }
    // 使用 StartCoroutine 之前我們要做個事前準備 IEnumerator 介面

    // IEnumerator 宣告名稱()
    // {
    // 程式碼
    // yield return 0;
    // }

    // 程式碼可以像範例那樣寫迴圈計時，當然不想這樣寫也可以
    // 當程式碼碰到 StartCoroutine 會去執行 IEnumerator 介面內的程式
    // 程式執行到 yield return 0; 時會先跳離介面同步執行 StartCoroutine 之後的程式
    // 然後再回去繼續執行 IEnumerator 介面
    // 直到迴圈結束沒遇到 yield return 0; 結束 StartCoroutine

    // 當然 IEnumerator 介面執行時也可以再塞 StartCoroutine 來執行其他 IEnumerator 介面

    void  OnTriggerEnter2D(Collider2D other)//偵測攻擊敵人
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
        
    }
}
