using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Entity
{
    private Transform shootPosition;
    [SerializeField] private GameObject bullet;
    private bool canShoot = true;
    private int timeReload = 2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
            GetDamage();
        }
    }

    private void Awake()
    {
        shootPosition = GameObject.Find("Shoot possition").transform;
        lives = 1;
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
