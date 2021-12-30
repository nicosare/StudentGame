
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Entity
{
    [SerializeField] private float speed = 3f; // скорость движения
    [SerializeField] private float jumpForce = 2f; // сила прыжка
    [SerializeField] private GameObject loseGameMenu;
    private bool isGrounded = false;
    private bool isDie = false;
    private bool isDamage = false;
    private HealthBar healthBar;
    private int maxHealth = 100;
    private int currentHealth;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private GameObject groundChecker;

    public bool isAttacking = false;
    public bool isRecharged = true;

    public Transform attackPos;
    public float attackRange;
    public LayerMask enemy;
    public static Hero Instance { get; set; }

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value);  }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        groundChecker = GameObject.Find("Ground checker");
        healthBar = GameObject.Find("Main Canvas").GetComponentInChildren<HealthBar>();
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
        isRecharged = true;
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (isGrounded && !isDie && !isDamage && !isAttacking) State = States.idle;

        if (Input.GetButton("Horizontal") && !isDie && !isAttacking)
            Run();
        if (isGrounded && Input.GetButtonDown("Jump") && !isDie && !isAttacking)
            Jump();
        if (Input.GetButtonDown("Fire1") && isGrounded && !isDie)
            Attack();
    }

    private void Run()
    {
        if (isGrounded & !isDamage) State = States.run;

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        if (!isGrounded & !isDamage) State = States.jump;
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(groundChecker.transform.position, 0.3f);
        isGrounded = collider.Length > 1;
    }

    private IEnumerator DamageAnimation()
    {
        yield return new WaitForSeconds(0.21f);
        isDamage = false;
    }
    public override void GetDamage()
    {
        isDamage = true;
        State = States.hurt;
        StartCoroutine(DamageAnimation());
        currentHealth -= 20;
        healthBar.SetHealth(currentHealth);
        if (currentHealth < 1)
        {
            isDie = true;
            State = States.death;
        }
    }

    public override void GetHeal()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += 20;
            healthBar.SetHealth(currentHealth);
        }
    }

    public override void Die()
    {
        healthBar.SetHealth(0);
        Time.timeScale = 0f;
        loseGameMenu.SetActive(true);
    }

    private void Attack()
    {
        State = States.attack3;
        isAttacking = true;
        isRecharged = false;

        StartCoroutine(AttackAnimation());
        StartCoroutine(AttackCoolDown());
    }

    private void OnAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);

        for (int i = 0; i <colliders.Length; i++)
        {
            colliders[i].GetComponent<Entity>().GetDamage();
        }
    }

    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.6f);
        isAttacking = false;
    }

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        isRecharged = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}

public enum States
{
    idle,
    run,
    jump,
    hurt,
    death,
    attack1,
    attack2,
    attack3
}
