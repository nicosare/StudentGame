using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Entity
{
    private float speed = 2.5f;
    private Vector3 dir;
    private SpriteRenderer sprite;
    private int timeLife = 3;

    void Start()
    {
        dir = transform.right * -1;
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(Death());
    }
    private void Update()
    {
        Move();
        transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(timeLife);
        Die();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
        }
        Die();
    }
}
