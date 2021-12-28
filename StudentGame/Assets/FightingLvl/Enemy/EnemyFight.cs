using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFight : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public PlayerFight player;
    public Animator animator;
    private int lvl = 1;
    private float damage = 5;
    public bool isFreeForAttack;
    public Dictionary<string, bool> attacks;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        StartCoroutine(Combinations());
        attacks = new Dictionary<string, bool>
        {
            ["Left"] = false,
            ["Front"] = false,
            ["Right"] = false,
        };
    }

    void Update()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f);
        if (currentHealth <= 0 && lvl < 3)
            LvlUp();
    }

    private void Attack(string attack)
    {
        attacks[attack] = true;
        player.GetDamage(damage, attack);
    }

    private void EndAttack(string attack)
    {
        attacks[attack] = false;
    }

    private void FrontAttack()
    {
        animator.Play("Front");
    }

    public void LeftAttack()
    {
        animator.Play("Left");
    }

    private void RightAttack()
    {
        animator.Play("Right");
    }

    public void Wait()
    {
        isFreeForAttack = true;
    }

    private void Idle()
    {
        isFreeForAttack = false;
    }

    public void GetDamage(int damage)
    {
        this.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f);
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    public int GetHealth()
    {
        return currentHealth;
    }


    private void LvlUp()
    {
        lvl++;
        damage *= lvl;
        maxHealth *= lvl;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    IEnumerator Combinations()
    {
        yield return new WaitForSeconds(.1f);

        while (player.currentHealth >= 0)
        {
            LeftAttack();
            yield return new WaitForSeconds(1);
            FrontAttack();
            yield return new WaitForSeconds(1);
            RightAttack();
            yield return new WaitForSeconds(1);
            Wait();
            yield return new WaitForSeconds(1);
        }
    }
}
