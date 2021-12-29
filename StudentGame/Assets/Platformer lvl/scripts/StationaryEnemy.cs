using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryEnemy : Entity
{

    private void Start()
    {
        lives = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
            GetDamage();
        }
    }
}
