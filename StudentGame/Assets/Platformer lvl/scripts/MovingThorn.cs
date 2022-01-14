using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingThorn : Entity
{
    [SerializeField]private float speed = 2.5f;
    private Vector3 dir;
    private SpriteRenderer sprite;
    private bool isNearHero = false;

    public bool IsNearHero
    {
        set { isNearHero = value; }
    }


    private void Start()
    {
        dir = transform.up;
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (isNearHero)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipY = dir.y < 0.0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        dir *= -1f;
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
        }
    }
}
