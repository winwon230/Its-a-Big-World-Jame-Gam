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
            speed = 3f;
        }

        else if(enemyType == "SPIDER")
        {
            maxHealth = 20f;
            currentHealth = maxHealth;
            damage = 3f;
            speed = 4f;
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
    }

    private IEnumerator flashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColour;
    }
}
