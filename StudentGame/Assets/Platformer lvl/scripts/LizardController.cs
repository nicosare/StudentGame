using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardController : MonoBehaviour
{
    private bool isNextLevel;
    private float speed = 10f;
    private GameObject target;
    private SpriteRenderer sprite;
    private Vector3 fixedPositionsY;
    void Start()
    {
        target = GameObject.Find("Portal");
        sprite = GetComponentInChildren<SpriteRenderer>();
        fixedPositionsY = new Vector3(0, target.transform.position.y - transform.position.y, 0);
        sprite.enabled = false;
    }


    void Update()
    {
        isNextLevel = target.GetComponent<PortalController>().IsNextLevel;
        if (isNextLevel)
        {
            sprite.enabled = true;
            transform.position = Vector3.MoveTowards(transform.position, 
                target.transform.position - fixedPositionsY, speed * Time.deltaTime);
        }
    }
}
