using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScreenChange : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject img1;
    public GameObject img2;
    public GameObject img3;
    public GameObject img4;
    public GameObject img5;
    public float time;
    private int count = 0;
    private Animator anim; 
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            anim.SetBool("ChangeToWhite", true);
            Invoke("ChangeImage", time);
            count++;
        }
    }
    void ChangeImage()
    {
        if(count == 0)
        {
            img1.SetActive(false);
            img2.SetActive(true);
            //count++;
            anim.SetBool("ChangeToWhite", false);
        }
        if(count == 1)
        {
            img2.SetActive(false);
            img3.SetActive(true);
            //count++;
            anim.SetBool("ChangeToWhite", false);
        }
        if(count == 2)
        {
            img3.SetActive(false);
            img4.SetActive(true);
            //count++;
            anim.SetBool("ChangeToWhite", false);
        }
        if(count == 3)
        {
            img4.SetActive(false);
            img5.SetActive(true);
            //count++;
            anim.SetBool("ChangeToWhite", false);
        }
        if(count == 4)
        {
            
        }
        if(count == 5)
        {
            GameData.PrevEntranceID = null;
            GameData.HaveShoes = false;
            GameData.HaveKey = false;
            GameData.HP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().Health;;//GameData.HPOrigin;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        }
    }
}
