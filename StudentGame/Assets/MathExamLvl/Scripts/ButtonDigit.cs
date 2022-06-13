using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDigit : MonoBehaviour
{
    private Text digit;
    public GameObject bomb;

    private void Start()
    {
        digit = this.GetComponentInChildren<Text>();
    }

    public void GetDigit()
    {
        bomb.GetComponent<BombController>().PrintDigit(digit.text[0]);
    }
}
