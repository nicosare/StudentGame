using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;
    public EnemyFight enemy;
    public Animator animator;
    public int damage;
    public GameObject loseGameMenu;
    private Dictionary<string, int> absorption;
    private bool isDie;


    void Start()
    {
        isDie = false;
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

        absorption["Front"] = 0;
        absorption["Left"] = 0;
        absorption["Right"] = 0;
        if (!isDie)
        {
            if (Input.GetKey(KeyCode.W))
                FrontBlock();
            else if (Input.GetKey(KeyCode.A))
                LeftBlock();
            else if (Input.GetKey(KeyCode.D))
                RightBlock();
            if (Input.GetKeyDown(KeyCode.Space) && enemy.GetHealth() > 0)
            {
                animator.Play("Attack");

            }
        }
    }

    public void FrontBlock()
    {
        animator.Play("FrontBlock");
        absorption["Front"] = 100;
        absorption["Left"] = 0;
        absorption["Right"] = 0;
    }

    public void LeftBlock()
    {
        animator.Play("LeftBlock");
        absorption["Left"] = 100;
        absorption["Front"] = 0;
        absorption["Right"] = 0;
    }

    public void RightBlock()
    {
        animator.Play("RightBlock");
        absorption["Right"] = 100;
        absorption["Front"] = 0;
        absorption["Left"] = 0;
    }

    public void Attack()
    {
        if (enemy.isFreeForAttack == true)
            enemy.GetDamage(damage);
    }

    public void GetDamage(float damage, string attack)
    {
        if (enemy.attacks[attack])
        {
            damage = damage - (damage / 100) * absorption[attack];
        }

        if(damage > 0)
            this.GetComponent<SpriteRenderer>().color = new Color(200f, 0f, 0f);

        currentHealth -= (int)damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            isDie = true;
            animator.Play("Die");
        }
    }

    public void Die()
    {
        Time.timeScale = 0f;
        loseGameMenu.SetActive(true);
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}
