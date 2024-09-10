using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioSource audioSrc;
    public static AudioClip attack;
    public static AudioClip Pick;
    public static AudioClip PlayerDeath;
    public static AudioClip PlayerHit;
    public static AudioClip Mhit;
    public static AudioClip Mdeath;
    public static AudioClip BossM;
    public GameObject TitleBGM;
    
    GameObject BGM = null;
    bool Boss ;
    // Start is called before the first frame update
    void Start()
    {
        
        audioSrc = GetComponent<AudioSource>();
        CheckBG();
        Pick = Resources.Load<AudioClip>("Pick");
        PlayerDeath = Resources.Load<AudioClip>("PlayerDeath");
        attack = Resources.Load<AudioClip>("attack");
        PlayerHit = Resources.Load<AudioClip>("PlayerHit");
        Mhit = Resources.Load<AudioClip>("Mhit");
        Mdeath = Resources.Load<AudioClip>("Mdeath");
        BossM = Resources.Load<AudioClip>("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlayattackClip()
    {
        audioSrc.PlayOneShot(attack);
    }
    public static void PlayPickClip()
    {
        audioSrc.PlayOneShot(Pick);
    }
    public static void PlayPlayerDeathClip()
    {
        audioSrc.PlayOneShot(PlayerDeath);
    }
    public static void PlayPlayerHitClip()
    {
        audioSrc.PlayOneShot(PlayerHit);
    }
    public static void PlayMhitClip()
    {
        audioSrc.PlayOneShot(Mhit);
    }
    public static void PlayMdeathClip()
    {
        audioSrc.PlayOneShot(Mdeath);
    }
    public static void PlayBossClip()
    {
        audioSrc.PlayOneShot(BossM);
    }
    public void CheckBG()
    {
        BGM = GameObject.FindGameObjectWithTag("BackGroundMusic");
        Boss = GameObject.FindGameObjectWithTag("Boss");
        if(BGM == null)
        {
            BGM = Instantiate(TitleBGM);
        }
        if(Boss)
        {
            Destroy(BGM);
        }
    } 
}
