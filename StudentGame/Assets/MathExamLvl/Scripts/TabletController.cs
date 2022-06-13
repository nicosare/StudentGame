using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class TabletController : MonoBehaviour
{
    public GameObject text;
    public GameObject selectGreenRed;
    public GameObject switch_;
    private int countSymbolInput = 0;
    private readonly string defaultText = "_______";
    private Text valueText;
    private StringBuilder stringBuilder;
    public bool isStartGame = false;


    private void Start()
    {
        valueText = text.GetComponent<Text>();
        stringBuilder = new StringBuilder(defaultText, 7);
        valueText.text = stringBuilder.ToString();
    }

    public void PrintZero()
    {
        isStartGame = true;

        if(countSymbolInput < 7)
        {
            countSymbolInput += 1;
            stringBuilder[countSymbolInput - 1] = '0';
            valueText.text = stringBuilder.ToString();

            if (countSymbolInput == 7)
                CheckAnswer();
        }
    }

    public void PrintOne()
    {
        isStartGame = true;

        if(countSymbolInput < 7)
        {
            countSymbolInput += 1;
            stringBuilder[countSymbolInput - 1] = '1';
            valueText.text = stringBuilder.ToString();

            if (countSymbolInput == 7)
                CheckAnswer();
        }
    }

    private void CheckAnswer()
    {
        var firstAnswer = "0111100";
        var secondAnswer = "1000011";

        if (stringBuilder.ToString() == firstAnswer)
            selectGreenRed.SetActive(true);
        else if (stringBuilder.ToString() == secondAnswer)
            switch_.SetActive(true);
        else
        {
            countSymbolInput = 0;
            stringBuilder = new StringBuilder(defaultText, 7);
            valueText.text = stringBuilder.ToString();
            this.GetComponent<AudioSource>().Play();
        }

    }
}
