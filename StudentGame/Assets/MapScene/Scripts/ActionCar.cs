using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCar : MonoBehaviour
{
    private GameObject player;
    private bool go;
    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void GetInCar()
    {
        player.SetActive(false);
        go = true;
    }
    private void Update()
    {
        if (go)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 1.3f * transform.position.y, transform.position.z), Time.deltaTime);
        }
    }
}
