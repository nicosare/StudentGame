using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotions : MonoBehaviour
{
    public int heal;


    private void Start()
    {
    }

    private void OnMouseDown()
    {
        GameObject.Find("Player").GetComponent<PlayerFight>().GetHeal(heal);
        Destroy(gameObject);
    }

    private void Update()
    {
        Fall();
    }

    private void Fall()
    {
        transform.Translate(0, -5f * Time.deltaTime, 0);
        if (transform.position.y < -6)
            Destroy(gameObject);
    }
}
