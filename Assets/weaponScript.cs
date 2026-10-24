using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Mathematics;
using UnityEngine;

public class weaponScript : MonoBehaviour
{
    public GameObject needle;

    public float detectionRange = 3f;
    public float fireRate = 100f;
    public LayerMask enemyLayer;

    private float cooldown = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, detectionRange, enemyLayer);
        
        Transform target = null;
        float closestDistance = Mathf.Infinity;

        foreach(Collider2D enemy in enemies)
        {
            float distance = Vector2.Distance(enemy.transform.position, transform.position);
            if(distance < closestDistance)
            {
                closestDistance = distance;
                target = enemy.transform;
            }
        }

        if(target != null && cooldown <= 0f)
        {
            Shoot(target);
            cooldown = 1f/fireRate;
        }


    }

    public void Shoot(Transform target)
    {
        GameObject projectile = Instantiate(needle, transform.position, quaternion.identity);
        projectileScript projscript = projectile.GetComponent<projectileScript>();
        projscript.shootTowards(target);
    }
}
