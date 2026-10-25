using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    public Sprite antSprite;
    public Sprite beetleSprite;
    public Sprite spiderSprite;
    public Material enemyFlashMaterial;

    private gameManager gameManager;

    private SpriteRenderer spriteRenderer;
    private Color originalColour;
    private Material enemyMaterial;
    private playerMovement playerScript;
    private Transform playerTransform;
    private Rigidbody2D rb;
    private Coroutine flashCoroutine;

    private Animator animator;

    public float maxHealth;
    public float currentHealth;
    private float previousHealth;
    public float damage;
    public float speed;
    public string enemyType;
    private float hitCooldown;
    private float fireRate = 50f;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindFirstObjectByType<gameManager>();
        rb = GetComponent<Rigidbody2D>();
        playerScript = FindFirstObjectByType<playerMovement>();
        playerTransform = playerScript.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColour = spriteRenderer.color;
        animator = GetComponent<Animator>();

        enemyMaterial = new Material(enemyFlashMaterial);
        spriteRenderer.material = enemyMaterial;
        enemyMaterial.SetFloat("_FlashAmount", 0f);

        if(enemyType == "ANT") // for animator, 0 = ant, 1 = beetle, 2 = spider
        {
            maxHealth = 5f;
            currentHealth = maxHealth;
            damage = 1f;
            speed = 3.5f;
            spriteRenderer.sprite = antSprite;
            animator.Play("AntWalk");
        }

        else if(enemyType == "BEETLE")
        {
            maxHealth = 12f;
            currentHealth = maxHealth;
            damage = 3f;
            speed = 2.5f;
            spriteRenderer.sprite = beetleSprite;
            animator.Play("BeetleWalk");
        }

        else if(enemyType == "SPIDER")
        {
            maxHealth = 100f;
            currentHealth = maxHealth;
            damage = 12f;
            speed = 1.3f;
            spriteRenderer.sprite = spiderSprite;
            animator.Play("SpiderWalk");
        }
        previousHealth = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.currentGameState == gameManager.gameState.Play)
        {
        weaponScript weaponScript = playerTransform.GetComponent<weaponScript>();
        hitCooldown -= Time.deltaTime;
        if(Vector2.Distance(playerTransform.position, transform.position) > 0.1f)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            rb.velocity = direction * speed;   
            transform.rotation = Quaternion.LookRotation(Vector3.forward, playerTransform.position - transform.position);
        }

        if(Vector2.Distance(playerTransform.position, transform.position) < 0.5f) //logs hits if it gets close enough to the player
        {
            Debug.Log("Hit");
            if(hitCooldown <= 0f)
            {
                weaponScript.currentHP -= damage;
                hitCooldown = 1f/fireRate;
            }
        }
        if(previousHealth > currentHealth)
        {
            if (flashCoroutine != null)
                StopCoroutine(flashCoroutine);

            if (enemyMaterial != null)
                flashCoroutine = StartCoroutine(flashRed());
            previousHealth = currentHealth;
        }

        if(currentHealth <= 0f)
        {
            if(enemyType == "ANT")
                {
                    weaponScript.XP += 5f;
                }
            else if(enemyType == "BEETLE")
                {
                    weaponScript.XP += 10f;
                }
            else if(enemyType == "SPIDER")
                {
                    weaponScript.XP += 50f;
                }
            Destroy(gameObject); //maybe even add a death animation later
        }
        }
    }

    private IEnumerator flashRed()
    {
        enemyMaterial.SetColor(
            "_FlashColor", Color.red);

        enemyMaterial.SetFloat(
            "_FlashAmount", 1f);

        yield return new WaitForSeconds(0.1f);

        if (enemyMaterial != null)
            enemyMaterial.SetFloat("_FlashAmount", 0f);

        flashCoroutine = null;
    }
}
