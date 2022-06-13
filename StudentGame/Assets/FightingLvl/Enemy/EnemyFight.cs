using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void Delegate();
public class EnemyFight : MonoBehaviour
{
    public Cutscene cutscene;
    public GameObject winGameMenu;
    public GameObject pauseMenu;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public PlayerFight player;
    public Animator animator;
    public GameObject splashScreen;
    public GameObject bottomScreen;
    public GameObject topScreen;
    private int lvl = 1;
    private float damage = 5f;
    private float dmg;
    public bool isFreeForAttack;
    public Dictionary<string, bool> attacks;
    private bool isDie;
    private bool isLvlUp;
    
    void Start()
    {
        cutscene.On();
        transform.position = new Vector3(-11, 1, 0);
        isDie = false;
        isLvlUp = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        attacks = new Dictionary<string, bool>
        {
            ["Left"] = false,
            ["Front"] = false,
            ["Right"] = false,
            ["SuperFront"] = false,
            ["SuperRight"] = false,
            ["SuperLeft"] = false
        };
    }

    private void GoingToScene()
    {
        animator.Play("GoingToScene");
        transform.Translate(Vector3.right * Time.deltaTime);
        if (transform.localPosition.x >= 0)
        {
            cutscene.Off();
            RemoveSplashScreen();
            animator.Play("idle");
            StartCoroutine(Combinations());
        }
    }
    public void RemoveSplashScreen()
    {
        splashScreen.GetComponent<Animator>().Play("RemoveSplash");
        bottomScreen.GetComponent<Animator>().Play("RemoveBottom");
        topScreen.GetComponent<Animator>().Play("RemoveTop");
    }

    void Update()
    {
        if (cutscene.IsEnabled())
            GoingToScene();
        if (player.transform.localPosition.y < -2.6 && !cutscene.IsEnabled())
        {
            player.GoingToScene();
        }
    }

    private void Attack(string attack)
    {
        attacks[attack] = true;
        player.GetDamage(dmg, attack);
    }

    private void EndAttack(string attack)
    {
        if (attack.Contains("Super"))
        {
            transform.localScale -= new Vector3(3f, 3f, 0);
        }

        attacks[attack] = false;
    }

    private void SuperFrontAttack()
    {

        transform.localScale += new Vector3(3f, 3f, 0);
        dmg = damage * 1.2f;
        isFreeForAttack = false;
        animator.Play("SuperFront");
    }

    private void SuperLeftAttack()
    {

        transform.localScale += new Vector3(3f, 3f, 0);
        dmg = damage * 1.2f;
        isFreeForAttack = false;
        animator.Play("SuperLeft");
    }

    private void SuperRightAttack()
    {

        transform.localScale += new Vector3(3f, 3f, 0);
        dmg = damage * 1.2f;
        isFreeForAttack = false;
        animator.Play("SuperRight");
    }

    private void FrontAttack()
    {
        dmg = damage;
        isFreeForAttack = false;
        animator.Play("Front");
    }

    public void LeftAttack()
    {
        dmg = damage;
        isFreeForAttack = false;
        animator.Play("Left");
    }

    private void RightAttack()
    {
        dmg = damage;
        isFreeForAttack = false;
        animator.Play("Right");
    }

    public void Wait()
    {
        animator.Play("Wait");
    }

    private void Idle()
    {
        isFreeForAttack = true;
        isLvlUp = false;
    }

    public void GetDamage(float damage)
    {
        currentHealth -= (int)damage;
        healthBar.SetHealth(currentHealth);
        StartCoroutine(GetDamageAnim());
        if (currentHealth <= 0)
        {
            if (lvl < 3)
            {
                isLvlUp = true;
                LvlUp();
            }
            else
            {
                isDie = true;
                animator.Play("Die");
            }
        }
    }
    public int GetHealth()
    {
        return currentHealth;
    }

    public int GetLvl()
    {
        return lvl;
    }


    private void LvlUp()
    {
        isFreeForAttack = false;
        animator.Play("LvlUp");
        lvl++;
        damage *= lvl;
        maxHealth *= lvl;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        transform.localPosition += new Vector3(0f, .7f, 0f);
    }

    private IEnumerator GetDamageAnim()
    {
        GetComponent<SpriteRenderer>().color = new Color32(240, 150, 150, 255);
        transform.localPosition += new Vector3(0, .1f, 0);
        yield return new WaitForSeconds(.2f);
        transform.localPosition -= new Vector3(0, .1f, 0);
        GetComponent<SpriteRenderer>().color = new Color(255f, 244f, 244f);
    }

    private void GetBigger()
    {
        transform.localScale += new Vector3(1f, 1f, 0f);
    }

    private void Die()
    {
        Time.timeScale = 0f;
        winGameMenu.SetActive(true);
    }

    IEnumerator Combinations()
    {
        yield return new WaitForSeconds(5f);

        while (player.currentHealth > 0)
        {
            RandomMove();
            yield return new WaitForSeconds(3f - lvl * 0.5f);
        }
    }

    private void RandomMove()
    {
        var rnd = new System.Random();
        Delegate[] moves = new Delegate[]
        {
            LeftAttack, FrontAttack, RightAttack, Wait, SuperFrontAttack, SuperLeftAttack, SuperRightAttack
        };

        int a = rnd.Next(moves.Length);
        if (!isDie && !isLvlUp)
            moves[a]();
    }
}
