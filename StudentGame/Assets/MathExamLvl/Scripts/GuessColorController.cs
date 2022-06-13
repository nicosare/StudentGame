using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuessColorController : MonoBehaviour
{
    private bool[] isActiveButtons = new bool[4];

    void Start()
    {
        SetDefaultValue();
    }

    public void SetActiveButton(int index)
    {
        isActiveButtons[index] = true;

        if (CheckcCountTrue())
            CheckAnswer();
    }

    private void End()
    {
        SceneController.AddEnding(4);
    }

    private bool CheckcCountTrue()
    {
        var countTrue = 0;
        for (var i = 0; i < isActiveButtons.Length; i++)
            if (isActiveButtons[i])
                countTrue++;
        return countTrue == 2;
    }

    private void SetDefaultValue()
    {
        for (var i = 0; i < isActiveButtons.Length; i++)
            isActiveButtons[i] = false;
    }

    private void CheckAnswer()
    {
        if (isActiveButtons[0] && isActiveButtons[2])
        {
            this.GetComponent<AudioSource>().Play();
            End();
        }
        else
            SetDefaultValue();
    }
}
