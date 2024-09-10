using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("怪物基本數值")]
    public int health;
    public int damage;
    public float speed;
    
    public bool alreaydroppotion = false;
    [Header("動畫播放時間")]
    public float injuredtime;
    public float deathtime;
    [Header("隨機掉落")]
    public GameObject[] gos;
    private Animator anim;
    private BoxCollider2D coll;
    private PlayerHealth playerHealth;
    // Start is called before the first frame update
    public void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
        coll =  GetComponent<BoxCollider2D>();
        Potion();
    }

    // Update is called once per frame
    public void Update()
    {
        Death();
        Potion();
    }
    public void Death()
    {
        if(health <= 0)
        {
            speed = 0;
            
            Destroy(GetComponent<Rigidbody2D>());
            coll.enabled = false;
            anim.SetBool("Death", true);
            StartCoroutine(startDeathAnmation());
        }
    }
    IEnumerator startDeathAnmation()
    {
        yield return new WaitForSeconds(deathtime);
        Destroy(gameObject);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        
        GameController.CamShake.Shake();
        if(health > 0)
        {
            SoundManager.PlayMhitClip();
            anim.SetBool("Injured", true);
            StartCoroutine(startInjuredAnmation());
        }
        else
        {
            SoundManager.PlayMdeathClip();
        }
    }

    IEnumerator startInjuredAnmation()
    {
        yield return new WaitForSeconds(injuredtime);
        anim.SetBool("Injured", false);
    }

    void  OnTriggerEnter2D(Collider2D other) 
    {
       if(other.gameObject.CompareTag("Player")&& other.GetType().ToString() == "UnityEngine.PolygonCollider2D")
        {
            if(playerHealth != null)
            {
                playerHealth.DamagePlayer(damage);
            }
        }
    }
    public void Potion()
    {
        
        if(health <= 0 && alreaydroppotion == false)
        {
            alreaydroppotion = true;
            Vector3 pos = transform.position;
            Instantiate(gos[Random.Range(0,gos.Length)], pos, Quaternion.identity);
        }
    }
     
}
