using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPlatForm : MonoBehaviour
{
    [Header("隱藏時間")]
    public float HideTime;
    private Animator anim;
    private BoxCollider2D bx2D;
    private SpriteRenderer spr;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        bx2D = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            anim.SetTrigger("Collapse");
        }    
    }
    void DisableBoxCollider()
    {
        bx2D.enabled = false;
    }
    void HideTrapPlatForm()
    {
        spr.enabled = false;
        StartCoroutine(ShowTrapPlatform());
    }
    IEnumerator ShowTrapPlatform()
    {
        yield return new WaitForSeconds(HideTime);
        spr.enabled = true;
        bx2D.enabled = true;
        anim.SetTrigger("Idle");
    }
}
