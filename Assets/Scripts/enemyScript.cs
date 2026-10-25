using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColour;
    private playerMovement playerScript;
    private Transform playerTransform;
    private Rigidbody2D rb;

    public float maxHealth;
    public float currentHealth;
    private float previousHealth;
    public float damage;
    public float speed;
    public string enemyType;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerScript = FindFirstObjectByType<playerMovement>();
        playerTransform = playerScript.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColour = spriteRenderer.color;
        if(enemyType == "ANT")
        {
            maxHealth = 5f;
            currentHealth = maxHealth;
            damage = 1f;
            speed = 1f;
        }

        else if(enemyType == "BEETLE")
        {
            maxHealth = 12f;
            currentHealth = maxHealth;
            damage = 3f;
            speed = 1f;
        }

        else if(enemyType == "SPIDER")
        {
            maxHealth = 100f;
            currentHealth = maxHealth;
            damage = 12f;
            speed = 2f;
        }
        previousHealth = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(playerTransform.position, transform.position) > 0.1f)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            rb.velocity = direction * speed;   
        }
        if(previousHealth != currentHealth)
        {
            StartCoroutine(flashRed());
            previousHealth = currentHealth;
        }

        if(currentHealth <= 0f)
        {
            Destroy(gameObject); //maybe even add a death animation later
        }
    }

    private IEnumerator flashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColour;
    }
}
