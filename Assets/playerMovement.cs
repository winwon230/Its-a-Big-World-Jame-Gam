using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class playerMovement : MonoBehaviour 
{
    private float moveSpeed = 5f;
    private float damage = 1f;
    private Vector2 move;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        move = move.normalized * moveSpeed;
    }

    void FixedUpdate()
    {
        rb.velocity = move;
    }
}
