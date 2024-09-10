using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Continue();
            }
            else
            {
                Paused();
            }
        }
    }
    public void Continue()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }
    public void Paused()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
    }
    public void MainMenu()
    {
        GameData.PrevEntranceID = null;
        GameData.HaveShoes = false;
        GameData.HaveKey = false;
        GameData.HP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().Health;;//GameData.HPOrigin;
        GameIsPaused = false;
        Time.timeScale = 1.0f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
