using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public static bool isComplete = false;
    private Color[,] panel = new Color[3, 3];
    public GameObject guessColor;

    private void Start()
    {
        InitializationPanel();
    }

    private void InitializationPanel()
    {
        for (var i = 0; i < 3; i++)
            for (var j = 0; j < 3; j++)
                panel[i, j] = Color.white;
    }

    public void SetColor(Color color, (int, int) indexes)
    {
        panel[indexes.Item1, indexes.Item2] = color;
        CheckAnswer();
    }

    private void CheckAnswer()
    {
        var answer = new Color[3, 3]
        {
            {Color.green, Color.white, Color.green},
            {Color.green, Color.green, Color.green},
            {Color.green, Color.white, Color.green},
        };

        if(CheckArrayColor(panel,answer))
        {
            guessColor.SetActive(true);
            isComplete = true;
        }
    }

    private bool CheckArrayColor(Color[,] first, Color[,] second)
    {
        var isEqual = true;

        for (var i = 0; i < 3; i++)
            for (var j = 0; j < 3; j++)
                if (first[i, j] != second[i, j])
                    isEqual = false;

        return isEqual;
    }
}
