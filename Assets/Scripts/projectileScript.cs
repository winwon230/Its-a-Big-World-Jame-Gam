using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class projectileScript : MonoBehaviour
{
    private float speed = 3f;
    private Rigidbody2D rb;
    private Vector2 direction;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ENEMY"))
        {
            enemyScript enemyScript = other.GetComponent<enemyScript>();
            if(enemyScript != null)
            {
                enemyScript.currentHealth -= 3f;
            }

            Destroy(gameObject);
            return;
        }
    }

    public void shootTowards(Transform target)
    {
        direction = (target.position - transform.position).normalized;
        transform.up = direction;
        rb.velocity = direction * speed;
    }
}
