using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    public GameObject loseGameMenu;
   
    public void Restart()
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            loseGameMenu.SetActive(false);
            Time.timeScale = 1f;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
