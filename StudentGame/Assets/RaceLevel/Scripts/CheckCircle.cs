using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCircle : MonoBehaviour
{
    public GameObject player;
    public GameObject opponent;
    private int number;

    private void Start()
    {
        number = int.Parse(Convert.ToString(this.name[name.Length - 1]));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<Car2d>().SetCheck(number);
        }

        if (collision.gameObject.tag == "Opponent")
        {
            opponent.GetComponent<Car2d>().SetCheck(number);
        }
    }
}
