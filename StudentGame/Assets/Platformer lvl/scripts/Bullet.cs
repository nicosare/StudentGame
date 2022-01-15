using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Entity
{
    private float speed = 2.5f;
    private Vector3 dir;
    private float timeLife = 2f;
    public float TimeLife
    {
        set { timeLife = value; }
    }

    void Start()
    {
        dir = transform.right * -1;
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
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(timeLife);
        Die();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
            Die();
        } 
        else
        {
            Die();
        }
    }
}
