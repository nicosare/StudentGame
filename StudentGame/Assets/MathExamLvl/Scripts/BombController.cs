using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class BombController : MonoBehaviour
{
    public GameObject code;
    private int countSymbolInput = 0;
    private readonly string defaultText = "____";
    private Text valueCode;
    private StringBuilder stringBuilder;
    public Animator fireAnimator;

    private void Start()
    {
        valueCode = code.GetComponent<Text>();
        stringBuilder = new StringBuilder(defaultText, 4);
        valueCode.text = stringBuilder.ToString();
    }

    public void PrintDigit(char digit)
    {
        if (countSymbolInput < 4)
        {
            countSymbolInput += 1;
            stringBuilder[countSymbolInput - 1] = digit;
            valueCode.text = stringBuilder.ToString();

            if (countSymbolInput == 4)
                CheckAnswer();

        }
    }

    private void CheckAnswer()
    {
        var firstAnswer = "2011";
        var secondAnswer = "1993";

        if (stringBuilder.ToString() == firstAnswer)
        {
            SceneController.AddEnding(5);
            this.GetComponent<Animator>().SetBool("isStartAnimation", true);
            this.GetComponents<AudioSource>()[2].Play();
        }
        else if (stringBuilder.ToString() == secondAnswer)
        {
            SceneController.AddEnding(6);
            fireAnimator.SetBool("isStartAnimation", true);
            this.GetComponents<AudioSource>()[1].Play();
        }
        else
        {
            countSymbolInput = 0;
            stringBuilder = new StringBuilder(defaultText, 4);
            valueCode.text = stringBuilder.ToString();
            this.GetComponent<AudioSource>().Play();
        }

    }
}
