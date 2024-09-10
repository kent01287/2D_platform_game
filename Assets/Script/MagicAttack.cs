using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    [Header("飛行速度")]
    public float speed;
    [Header("傷害")]
    public int damage;
    public float destroyDistance;

    private Rigidbody2D rb2D;
    private Vector3 startPos;
    private PlayerHealth playerHealth;     
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = (-transform.right)* speed;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (transform.position - startPos).sqrMagnitude;
        if(distance > destroyDistance)
        {
            Destroy(gameObject);
        }
    }
    void  OnTriggerEnter2D(Collider2D other) 
    {
       if(other.gameObject.CompareTag("Player")&& other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if(playerHealth != null)
            {
                playerHealth.DamagePlayer(damage);
            }
        }
    }
}
