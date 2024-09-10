using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseMenu : MonoBehaviour
{
    public GameObject LoseMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            LoseMenuUI.SetActive(true);
        }
    }
    public void Restart()
    {
        LoseMenuUI.SetActive(false);
        GameData.PrevEntranceID = null;
        GameData.HaveShoes = false;
        GameData.HaveKey = false;
        GameData.HP = GameData.HPOrigin;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        GameData.PrevEntranceID = null;
        GameData.HaveShoes = false;
        GameData.HaveKey = false;
        GameData.HP = GameData.HPOrigin;
    }
}
