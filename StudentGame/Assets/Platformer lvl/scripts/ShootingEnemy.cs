using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Entity
{
    private Transform shootPosition;
    [SerializeField] private GameObject bullet;
    private bool canShoot = true;
    private float timeReload = 3.5f;
    [SerializeField] private float timeLifeBullet = 2f;

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
        shootPosition = this.transform.GetChild(1).transform;
        lives = 1;
    }
    
    private IEnumerator Shoot()
    {
        canShoot = false;
        Instantiate(bullet, shootPosition.position, transform.rotation);
        bullet.GetComponent<Bullet>().TimeLife = timeLifeBullet;
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
