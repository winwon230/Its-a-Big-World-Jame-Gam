using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speakerScript : MonoBehaviour
{
    private Animator animat;
public float detectionRange = 8f;
    public float fireRate = 2f;
    public float damage = 10f;
    public float aoeRadius = 4f;
    public LayerMask enemyLayer;

    private float cooldown = 0f;
    private gameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<gameManager>();
        animat = GetComponent<Animator>();
    }

    void Update()
    {
        animat.Play("Speakering");
        if (gameManager.currentGameState != gameManager.gameState.Play)
            return;

        cooldown -= Time.deltaTime;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            transform.position, detectionRange, enemyLayer
        );

        Transform target = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D enemy in enemies)
        {
            float distance = Vector2.Distance(
                transform.position, enemy.transform.position
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = enemy.transform;
            }
        }

        if (target != null)
        {
            Vector2 direction = target.position - transform.position;
            transform.right = direction.normalized;

            if (cooldown <= 0f)
            {
                ShootAOE();
                cooldown = fireRate;
            }
        }
    }

    void ShootAOE()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            transform.position, aoeRadius, enemyLayer
        );

        foreach (Collider2D enemy in enemies)
        {
            enemyScript enemyHealth = enemy.GetComponent<enemyScript>();

            if (enemyHealth != null)
            {
                enemyHealth.currentHealth -= damage;
            }
        }
    }
}
