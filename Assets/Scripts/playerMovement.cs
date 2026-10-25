using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour //includes hp mechanics
{
    private Animator animator;
    private gameManager gameManager;
    public float moveSpeed = 5f;
    private Vector2 move;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindFirstObjectByType<gameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.currentGameState == gameManager.gameState.Play)
        {
            move.x = Input.GetAxisRaw("Horizontal");
            move.y = Input.GetAxisRaw("Vertical");
            animator.SetFloat("InputX", move.x);
            animator.SetFloat("InputY", move.y);
            move = move.normalized * moveSpeed;
            if(move.magnitude > 0f)
            {
                animator.SetBool("isWalking", true);
            }

            else
            {
                animator.SetBool("isWalking", false);
            }   
        }
    }

    void FixedUpdate()
    {
        rb.velocity = move;
    }
}
