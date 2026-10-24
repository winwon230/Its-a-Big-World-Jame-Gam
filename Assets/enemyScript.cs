using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColour;

    public float maxHealth;
    public float currentHealth;
    private float previousHealth;
    public float damage;
    public float speed;
    public string enemyType;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColour = spriteRenderer.color;
        if(enemyType == "ANT")
        {
            maxHealth = 5f;
            currentHealth = maxHealth;
            damage = 1f;
            speed = 4.5f;
        }

        else if(enemyType == "BEETLE")
        {
            maxHealth = 12f;
            damage = 3f;
            speed = 3.5f;
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
