using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFight : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public PlayerFight player;
    public bool isBlock;
    public Animator animator;
    private int lvl = 1;
    private int damage = 5;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        StartCoroutine(Combinations());
    }


    void Update()
    {
        if (currentHealth <= 0 && lvl < 3)
            LvlUp();
        if (player.isAttack)
            Block();
    }

    //Action0
    private void FrontAttack()
    {
        if (!player.isFrontBlock)
        {
            player.GetDamage(damage);
        }
        Debug.Log("Front");
    }

    //Action1
    public void LeftAttack()
    {
        if (!player.isLeftBlock)
        {
            player.GetDamage(damage);
        }
        Debug.Log("Left");
    }

    //Action2
    private void RightAttack()
    {
        if (!player.isRightBlock)
        {
            player.GetDamage(damage);
        }
        Debug.Log("Right");
    }

    //Action3
    public void Wait()
    {
        Debug.Log("Wait");
    }

    private void Idle()
    {
        isBlock = false;
    }

    //Block
    public void Block()
    {
        if (Random.Range(0, 2) == 0)
        {
            GetDamage(player.damage);
        }
        else
        {
            animator.Play("Block");
            Debug.Log("Block");
        }
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        Debug.Log("Hit");
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
        while (player.GetHealth() > 0)
        {
            Action(Random.Range(0,4));
            yield return new WaitForSeconds(1);
        }
    }

    private void Action(int num)
    {
        animator.Play("Action" + num);
    }
}
