using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;
    public EnemyFight enemy;
    public bool isFrontBlock;
    public bool isRightBlock;
    public bool isLeftBlock;
    public bool isAttack;
    public int damage = 20;
    private bool isMove;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        isAttack = false;
        isFrontBlock = false;
        isLeftBlock = false;
        isRightBlock = false;
        this.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f);

        if (Input.GetKey(KeyCode.W))
            FrontBlock();
        if (Input.GetKey(KeyCode.A))
            LeftBlock();
        if (Input.GetKey(KeyCode.D))
            RightBlock();
        if (Input.GetKeyDown(KeyCode.Space) && enemy.GetHealth() > 0)
            Attack();
    }

    public void FrontBlock()
    {
        if (!(isLeftBlock || isRightBlock))
        {
            this.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f);
            isFrontBlock = true;
        }
    }

    public void LeftBlock()
    {
        if (!(isFrontBlock || isRightBlock))
        {
            this.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 255f);
            isLeftBlock = true;
        }
    }

    public void RightBlock()
    {
        if (!(isLeftBlock || isFrontBlock))
        {
            this.GetComponent<SpriteRenderer>().color = new Color(0f, 255f, 0f);
            isRightBlock = true;
        }
    }

    public void Attack()
    {
        if (!(isLeftBlock || isRightBlock || isFrontBlock || enemy.isBlock))
        {
            isAttack = true;
            this.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
        }
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}
