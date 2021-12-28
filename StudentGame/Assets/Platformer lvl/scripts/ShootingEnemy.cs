using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Entity
{
    private Transform shootPosition;
    [SerializeField] private GameObject bullet;
    [SerializeField] private int lives = 3;
    private bool canShoot = true;
    private int timeReload = 2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
            lives--;
            Debug.Log("У полицейского" + lives);
        }

        if (lives < 1)
            Die();
    }

    private void Awake()
    {
        shootPosition = GameObject.Find("Shoot possition").transform;
    }
    
    private IEnumerator Shoot()
    {
        canShoot = false;
        Instantiate(bullet, shootPosition.position, transform.rotation);
        yield return new WaitForSeconds(timeReload);
        canShoot = true;
    }

    private void Update()
    {
        if (canShoot)
        {
            StartCoroutine(Shoot());
        }
    }
}
