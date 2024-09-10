using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("玩家血量")]
    
    public  int Health;
    [Header("受傷CD")]
    public float HitBoxCdTime;
    [Header("死亡時間")]
    public float dietime;
    
    private Animator anim;
    private CapsuleCollider2D coll;
    private PolygonCollider2D polycoll;
    // Start is called before the first frame update
    void Start()
    {
        HealthBar.HealthMax = GameData.HPOrigin;
        HealthBar.HealthCurrent = GameData.HP;
        anim = GetComponent<Animator>();
        coll = GetComponent<CapsuleCollider2D>();
        polycoll = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
       HealthBar.HealthCurrent = GameData.HP;
       if(GameData.HP < 0)
        {
            GameData.HP = 0;
        }
        if(GameData.HP > Health)
        {
            GameData.HP = Health;
        }
    }
    public void DamagePlayer(int damage)//Player被攻擊
    {
        SoundManager.PlayPlayerHitClip();
        GameData.HP -= damage;
        if(GameData.HP < 0)
        {
            GameData.HP = 0;
        }
        if(GameData.HP > Health)
        {
            GameData.HP = Health;
        }
        HealthBar.HealthCurrent = GameData.HP;    
        if(GameData.HP <= 0)
        {
            anim.SetTrigger("Death");
            SoundManager.PlayPlayerDeathClip();
            Destroy(GetComponent<Rigidbody2D>());
            coll.enabled = false;
            Invoke("KillPlayer", dietime);//延遲Destroy
        }
        polycoll.enabled = false;
        Injured();
        StartCoroutine(ShowPlayerHitBox());
    } 
    IEnumerator ShowPlayerHitBox()
    {
        yield return new WaitForSeconds(HitBoxCdTime);
        polycoll.enabled = true;
    }
    void KillPlayer()//Destroy Player
    {
        Destroy(gameObject);
    }
    void Injured()//玩家受傷
    {
        anim.SetTrigger("Injured");
    }
}
