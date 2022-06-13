using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class TwoDigitController : MonoBehaviour
{
    public GameObject score;
    private int countInput = 0;
    private readonly string defaultText = "00";
    private Text valueScore;
    private StringBuilder stringBuilder;

    private void Start()
    {
        valueScore = score.GetComponent<Text>();
        stringBuilder = new StringBuilder(defaultText, 2);
        valueScore.text = stringBuilder.ToString();
    }

    public void AddPoint()
    {
        if (countInput < 99)
        {
            countInput += 1;

            if (countInput % 10 != 0)
                stringBuilder[1] = (char)(countInput % 10 + '0');
            else
            {
                stringBuilder[1] = '0';
                stringBuilder[0] = (char)(countInput / 10 + '0');
            }
            valueScore.text = stringBuilder.ToString();
        }
    }

    public void Input()
    {
        var firstAnswer = "09";
        var secondAnswer = "53";

        if (stringBuilder.ToString() == firstAnswer)
        {
            countInput = 100;
            SceneController.AddEnding(2);
            this.GetComponents<AudioSource>()[1].Play();
        }
        else if (stringBuilder.ToString() == secondAnswer)
        {
            countInput = 100;
            SceneController.AddEnding(3);
            this.GetComponents<AudioSource>()[2].Play();
        }
        else
        {
            countInput = 0;
            stringBuilder = new StringBuilder(defaultText, 2);
            valueScore.text = stringBuilder.ToString();
            this.GetComponent<AudioSource>().Play();
        }
    }
}
