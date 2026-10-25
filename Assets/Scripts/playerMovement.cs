using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class playerMovement : MonoBehaviour 
{
    private Animator animator;
    private float moveSpeed = 5f;
    private float damage = 1f;
    private Vector2 move;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
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

    void FixedUpdate()
    {
        rb.velocity = move;
    }
}
