using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool pauseGame;
    public GameObject pauseGameMenu;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
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

    public void Pause()
    {
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
        pauseGame = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
