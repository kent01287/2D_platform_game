using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    
    // Start is called before the first frame update
    private Animator anim;
    private Transform PlayerTransform;
    private EnemyFireBallGhost start;
    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        start = GetComponent<EnemyFireBallGhost>();
    }

    // Update is called once per frame
    void Update()
    {
         AttackTrig();
    }
    void AttackTrig()
    {
        if((transform.position.y - PlayerTransform.position.y) < 0.3f && (transform.position.y - PlayerTransform.position.y) > -0.3f)
        {
            start.StartAttack();
            this.enabled = false;
        }
    }
    
}
