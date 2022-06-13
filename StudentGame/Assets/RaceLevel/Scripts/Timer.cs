using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float timeStart = 3;
    public Text textTimer;
    private bool timerRunning = true;
    public bool isStartGame;

    void Start()
    {
        textTimer.text = timeStart.ToString();
    }

    void Update()
    {
        if (textTimer.isActiveAndEnabled)
        {
            if (timerRunning == true && timeStart > 0.5)
            {
                textTimer.text = Mathf.Round(timeStart).ToString();
                timeStart -= Time.deltaTime;
            }

            if (timeStart < 0.5)
            {
                textTimer.text = "Старт!";
                timerRunning = false;
            }
        }
    }
}
