using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovingThorn : MonoBehaviour
{
    private GameObject[] movingThotns;
    private void Start()
    {
        movingThotns = GameObject.FindGameObjectsWithTag("Moving thorn");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Hero")
        {
            for(int i = 0; i < movingThotns.Length; i++)
            {
                movingThotns[i].GetComponent<MovingThorn>().IsNearHero = true;
            }
        }
    }
}
