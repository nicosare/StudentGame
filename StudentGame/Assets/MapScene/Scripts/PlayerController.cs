using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private Animator anim;
    private Rigidbody2D rb;
    public Vector2 direction;
    private bool playerMoving;
    private Vector2 lastMove;
    public bool isDialogue;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        direction.y = Input.GetAxisRaw("Vertical");
        direction.x = Input.GetAxisRaw("Horizontal");

        playerMoving = false;
        if (!isDialogue)
        {
            if (direction.x > 0.5f || direction.x < -0.5f || direction.y > 0.5f || direction.y < -0.5f)
            {
                rb.MovePosition(rb.position + direction.normalized * moveSpeed * Time.deltaTime);
                playerMoving = true;
                lastMove = direction;
            }

            anim.SetBool("PlayerMoving", playerMoving);
            anim.SetFloat("MoveX", direction.x); ;
            anim.SetFloat("MoveY", direction.y);
            anim.SetFloat("LastMoveX", lastMove.x);
            anim.SetFloat("LastMoveY", lastMove.y);
        }
    }
}
