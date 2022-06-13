using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;

public class LevelController : MonoBehaviour
{
    public Text textCircle;
    private static StringBuilder newTextCircle;
    public GameObject extensionPanel;
    public GameObject countdown;
    public bool isStartGame = false;
    public bool isEnd;

    public void Win()
    {
        isStartGame = false;
        Time.timeScale = 0;
        this.GetComponents<AudioSource>()[3].Play();
    }

    public void Lose()
    {
        isStartGame = false;
        this.GetComponents<AudioSource>()[2].Play();
    }

    void Start()
    {
        newTextCircle = new StringBuilder(textCircle.text, 3);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
            StartGame();
    }

    public void ChangeTextCircle(int countCircle)
    {
        newTextCircle[0] = Convert.ToString(countCircle).ToCharArray()[0];
        textCircle.text = newTextCircle.ToString();
    }

    private void StartGame()
    {
        extensionPanel.SetActive(false);
        countdown.SetActive(true);
        StartCoroutine(WaitAnimation());
        this.GetComponents<AudioSource>()[1].Play();
    }

    private IEnumerator WaitAnimation()
    {
        yield return new WaitForSeconds(3f);
        countdown.SetActive(false);
        isStartGame = true;
    }
}
