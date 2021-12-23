using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;
    public EnemyFight enemy;
    public int damage;
    private Dictionary<string, int> absorption;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        absorption = new Dictionary<string, int>
        {
            ["Left"] = 0,
            ["Front"] = 0,
            ["Right"] = 0
        };
    }

    void Update()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f);

        if (Input.GetKey(KeyCode.W))
            FrontBlock();
        else absorption["Front"] = 0;

        if (Input.GetKey(KeyCode.A))
            LeftBlock();
        else absorption["Left"] = 0;

        if (Input.GetKey(KeyCode.D))
            RightBlock();
        else absorption["Right"] = 0;

        if (Input.GetKeyDown(KeyCode.Space) && enemy.GetHealth() > 0)
            Attack();
    }

    public void FrontBlock()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f);
        absorption["Front"] = 100;
        absorption["Left"] = 0;
        absorption["Right"] = 0;
    }

    public void LeftBlock()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 255f);
        absorption["Left"] = 100;
        absorption["Front"] = 0;
        absorption["Right"] = 0;
    }

    public void RightBlock()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(0f, 255f, 0f);
        absorption["Right"] = 100;
        absorption["Front"] = 0;
        absorption["Left"] = 0;
    }

    public void Attack()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
        if (enemy.isFreeForAttack == true)
            enemy.GetDamage(damage);
    }

    public void GetDamage(float damage, string attack)
    {
        if (enemy.attacks[attack])
        {
            damage = damage - (damage / 100) * absorption[attack];
        }

        currentHealth -= (int)damage;
        healthBar.SetHealth(currentHealth);
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}
