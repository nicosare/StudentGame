using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Entity
{
    static Vector3 lastCheckpointPosition = Vector3.zero;
    [SerializeField] private float speed = 3.2f; // скорость движения
    [SerializeField] private float jumpForce = 10.2f; // сила прыжка
    [SerializeField] private GameObject loseGameMenu;
    private bool isGrounded = false;
    private bool isDie = false;
    private bool isDamage = false;
    private bool isDialogue = false;
    private bool isHeal = false;
    private HealthBar healthBar;
    private int maxHealth = 100;
    private int currentHealth;
    private Vector3 startPosition;

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
    public bool IsDialogue
    {
        set { isDialogue = value; }
    }

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void Start()
    {
        if (lastCheckpointPosition != Vector3.zero)
        {
            transform.position = lastCheckpointPosition;
        }
    }

    private void Awake()
    {
        startPosition = transform.position;
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
        if (isGrounded && !isDie && !isDamage && !isAttacking && !isHeal) State = States.idle;
        if (!isGrounded && !isDie && !isDamage && !isAttacking && !isHeal) State = States.jump;

        if (Input.GetButton("Horizontal") && !isDie && !isAttacking && !isDialogue)
            Run();
        if (isGrounded && Input.GetButtonDown("Jump") && !isDie && !isAttacking && !isDialogue)
            Jump();
        if (Input.GetButtonDown("Fire1") && isGrounded && !isDie && !isDialogue)
            Attack();
    }

    private void Run()
    {
        if (isGrounded && !isDamage && !isHeal) State = States.run;

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(groundChecker.transform.position, 0.1f);
        isGrounded = collider.Length > 1;
    }

    private IEnumerator DamageAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        isDamage = false;
    }

    private IEnumerator HealAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        isHeal = false;
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
        isHeal = true;
        State = States.heal;
        StartCoroutine(HealAnimation());
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

    public void EndGame()
    {
        lastCheckpointPosition = startPosition;
        sprite.enabled = false;
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

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<Entity>().GetDamage();
        }
    }

    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(3f);
        isRecharged = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            lastCheckpointPosition = collision.transform.position;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Falling platform" && (Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.DownArrow)))
        {
            collision.transform.GetComponent<FallingPlatform>().OffCollider();
        }
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
    attack3,
    heal
}
