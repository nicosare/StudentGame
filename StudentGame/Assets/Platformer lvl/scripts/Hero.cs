using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Entity
{
    [SerializeField] private float speed = 3f; // скорость движения
    [SerializeField] private float jumpForce = 2f; // сила прыжка
    private bool isGrounded = false;
    private HealthBar healthBar;
    private int maxHealth = 100;
    private int currentHealth;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private GameObject groundChecker;
    public static Hero Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        groundChecker = GameObject.Find("Ground checker");
        healthBar = GameObject.Find("Health Bar").GetComponentInChildren<HealthBar>();
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal"))
            Run();
        if (isGrounded && Input.GetButtonDown("Jump"))
            Jump();
    }

    private void Run()
    {
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
        Collider2D[] collider = Physics2D.OverlapCircleAll(groundChecker.transform.position, 0.3f);
        isGrounded = collider.Length > 1;
    }

    public override void GetDamage()
    {
        currentHealth -= 20;
        healthBar.SetHealth(currentHealth);
        if (currentHealth < 1)
            Die();
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
        base.Die();
    }
}
