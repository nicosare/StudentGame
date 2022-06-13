using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private Cutscene cutscene;
    public bool pauseGame;
    public GameObject pauseGameMenu;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "FightingLvl")
            cutscene = GameObject.Find("Canvas").GetComponent<Cutscene>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Platformer lvl")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pauseGame)
                    Resume();
                else
                    Pause();
            }
        }
        else if (!cutscene.IsEnabled() && Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseGame)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
        pauseGame = false;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
        pauseGame = false;
    }

    public void Pause()
    {
        if (Time.timeScale > 0)
        {
            pauseGameMenu.SetActive(true);
            Time.timeScale = 0f;
            pauseGame = true;
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
