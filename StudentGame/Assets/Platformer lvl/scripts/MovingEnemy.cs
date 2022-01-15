using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : Entity
{
    private float speed = 2.5f;
    private SpriteRenderer sprite;
    private Vector3 targetRight, targetLeft;
    private Vector3 nextTarget;


    private void Start()
    {
        targetRight = transform.GetChild(1).transform.position;
        targetLeft = transform.GetChild(2).transform.position;
        nextTarget = targetRight;
        sprite = GetComponentInChildren<SpriteRenderer>();
        lives = 1;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {

        transform.position = Vector3.MoveTowards(transform.position, nextTarget, speed * Time.deltaTime);
        if (transform.position.x == targetRight.x)
        {
            sprite.flipX = true;
            nextTarget = targetLeft;
        }
        else if (transform.position.x == targetLeft.x)
        {
            sprite.flipX = false;
            nextTarget = targetRight;
        }
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
