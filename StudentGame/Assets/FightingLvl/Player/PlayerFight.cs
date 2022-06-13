using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{
    public GameObject DmgNumParent, DamageText;
    public GameObject HealNumParent, HealText;
    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;
    public EnemyFight enemy;
    public Animator animator;
    public float damage;
    public GameObject loseGameMenu;
    private Dictionary<string, float> absorption;
    private bool isDie;
    private bool isAttack;
    private bool isGoing;
    private DmgNumObj DmgNumsPool;
    private DmgNumObj HealNumsPool;
    private bool isFirstDmg;
    private bool isFirstHeal;
    public GameObject shieldLeft;
    public GameObject shieldFront;
    public GameObject shieldRight;

    void Start()
    {
        isFirstHeal = true;
        isFirstDmg = true;
        isAttack = false;
        isDie = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        DmgNumParent.SetActive(true);

        absorption = new Dictionary<string, float>
        {
            ["Left"] = 0,
            ["Front"] = 0,
            ["Right"] = 0,
            ["SuperFront"] = 0,
            ["SuperLeft"] = 0,
            ["SuperRight"] = 0
        };
    }

    void Update()
    {
        if (!isDie && !isGoing)
        {
            if (Input.GetKey(KeyCode.W))
            {
                FrontBlock();
            }
            else if (Input.GetKey(KeyCode.A))
            {
                LeftBlock();
            }
            else if (Input.GetKey(KeyCode.D))
            {
                RightBlock();
            }
            if (Input.GetKeyDown(KeyCode.Space) && !isAttack)
            {
                isAttack = true;
                RandomAttack();
            }

            if (Input.GetKeyUp(KeyCode.W))
                shieldFront.SetActive(false);
            if (Input.GetKeyUp(KeyCode.A))
                shieldLeft.SetActive(false);
            if (Input.GetKeyUp(KeyCode.D))
                shieldRight.SetActive(false);
        }
    }

    public void GoingToScene()
    {
        isGoing = true;
        animator.Play("GoingToScene");
        transform.Translate(Vector3.up * Time.deltaTime);
        if (transform.localPosition.y >= -2.6)
            animator.Play("idle");
    }

    private void RandomAttack()
    {
        var rnd = new System.Random();
        animator.Play("Attack" + rnd.Next(3));
    }

    public void FrontBlock()
    {
        shieldFront.SetActive(true);
        shieldRight.SetActive(false);
        shieldLeft.SetActive(false);
        animator.Play("FrontBlock");
        absorption["Front"] = 100f;
        absorption["SuperFront"] = 80f;
    }

    private void Idle()
    {
        absorption["Front"] = 0;
        absorption["Left"] = 0;
        absorption["Right"] = 0;
        absorption["SuperFront"] = 0;
        absorption["SuperLeft"] = 0;
        absorption["SuperRight"] = 0;
        isAttack = false;
        isGoing = false;

    }

    public void LeftBlock()
    {
        shieldLeft.SetActive(true);
        shieldFront.SetActive(false);
        shieldRight.SetActive(false);
        animator.Play("LeftBlock");
        absorption["Left"] = 100f;
        absorption["SuperLeft"] = 80f;
    }

    public void RightBlock()
    {
        shieldRight.SetActive(true);
        shieldFront.SetActive(false);
        shieldLeft.SetActive(false);
        animator.Play("RightBlock");
        absorption["Right"] = 100f;
        absorption["SuperRight"] = 80f;
    }

    public void Attack()
    {
        if (enemy.isFreeForAttack)
            enemy.GetDamage(damage);
    }

    public void GetDamage(float damage, string attack)
    {
        if (enemy.attacks[attack])
        {
            if (isFirstDmg)
            {
                DmgNumsPool = Instantiate(DamageText, DmgNumParent.transform).GetComponent<DmgNumObj>();
                isFirstDmg = false;
            }
            damage = damage - (damage / 100) * absorption[attack];

            currentHealth -= (int)damage;
            healthBar.SetHealth(currentHealth);

            if (currentHealth <= 0)
            {
                isDie = true;
                animator.Play("Die");
            }
        }
        if (absorption[attack] == 0)
        {
            if(!isDie)
                StartCoroutine(GetDamageAnim(attack));
            DmgNumsPool.StartMotion("-" + damage.ToString(),
                new Color(180f, 0f, 0f),
                new Vector2(Random.Range(-200, 200), Random.Range(0, 250)),
                new Vector2(0, -300));
        }
        else
        {
            if (damage > 0)
            {
                if(!isDie)
                    StartCoroutine(GetDamageAnim(attack));
                DmgNumsPool.StartMotion("-" + damage.ToString(),
                    new Color(235f, 220f, 0f),
                    new Vector2(Random.Range(-200, 200), Random.Range(0, 250)),
                    new Vector2(0, -300));
            }
            else
                DmgNumsPool.StartMotion(damage.ToString(),
                    new Color(235f, 220f, 0f),
                    new Vector2(Random.Range(-200, 200), Random.Range(0, 250)),
                    new Vector2(0, -300));
        }
    }

    private IEnumerator GetDamageAnim(string attack)
    {
        this.GetComponent<SpriteRenderer>().color = new Color32(240, 150, 150, 255);
        if (attack.Contains("Left"))
            transform.localPosition -= new Vector3(-.05f, .05f, 0);
        if (attack.Contains("Right"))
            transform.localPosition -= new Vector3(.05f, .05f, 0);
        if (attack.Contains("Front"))
            transform.localPosition -= new Vector3(0, .07f, 0);
        yield return new WaitForSeconds(.2f);
        transform.localPosition = new Vector3(0, -2.5f, -1f);
        GetComponent<SpriteRenderer>().color = new Color32(244, 242, 255, 255);
    }

    public void GetHeal(int heal)
    {
        if (isFirstHeal)
        {
            HealNumsPool = Instantiate(HealText, HealNumParent.transform).GetComponent<DmgNumObj>();
            isFirstHeal = false;
        }
        var diff = maxHealth - currentHealth;
        if (diff > heal)
            currentHealth += heal;
        else
        {
            heal = diff;
            currentHealth += (heal);
        }
        HealNumsPool.StartMotion("+" + heal.ToString(),
            new Color(0f, 170f, 0f),
            new Vector2(0, 250),
            new Vector2(200, -300));
        healthBar.SetHealth(currentHealth);
    }

    public void Die()
    {
        shieldFront.SetActive(false);
        shieldRight.SetActive(false);
        shieldLeft.SetActive(false);
        DmgNumParent.SetActive(false);
        Time.timeScale = 0f;
        loseGameMenu.SetActive(true);
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}
