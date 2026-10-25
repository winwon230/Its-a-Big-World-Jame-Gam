using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class weaponScript : MonoBehaviour //includes shop stuff
{
    public GameObject needle;
    private gameManager gameManager;
    private playerMovement playerMovement;

    private mobSpawner mobSpawner;
    public GameObject shop;

    public TMP_Text XPtext;
    public float XP;

    public float currentHP;
    public float maxHP = 100f;
    public Slider hpBar;

    public float detectionRange = 5f;
    public float needleDamage = 2f;
    public float fireRate = 150f;
    public LayerMask enemyLayer;

    //below is the list of the different shop items
    public GameObject buySword;
    public GameObject buyReload;
    public GameObject buyMilk;
    public GameObject buyChocolate;
    public GameObject buyEnergy;
    public GameObject buySpeaker;

    //items used in the shop stuff
    public GameObject chocolateBar;
    public GameObject speaker;

    private float cooldown = 0f;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindFirstObjectByType<playerMovement>();
        shop.SetActive(false);
        gameManager = FindFirstObjectByType<gameManager>();
        currentHP = maxHP;
        hpBar.maxValue = maxHP;
        mobSpawner = FindFirstObjectByType<mobSpawner>();
    }

    public void upgradeHealth()
    {
        maxHP = maxHP * 1.2f;
        hpBar.maxValue = maxHP;
    }

    public void upgradeDamage()
    {
        needleDamage = needleDamage * 1.25f;
    }

    public void upgradeReload()
    {
        fireRate = fireRate * 1.2f;
    }

    public void resetHealth()
    {
        currentHP = maxHP;
    }

    public void openShop()
    {
        shop.SetActive(true);
        buySword.SetActive(false);
        buyReload.SetActive(false);
        buyMilk.SetActive(false);
        buyChocolate.SetActive(false);
        buyEnergy.SetActive(false);
        buySpeaker.SetActive(false);
        //0-75 gives one of the common ones, 75-95 gives epic, 95-100 gives legendary

        for(int i = 0; i<3; i++) //randomises shop selection
        {
            int rarityRoll = Random.Range(1, 101);
            GameObject chosenUpgrade = null;

            if(rarityRoll <= 75)
            {
                GameObject[] common = {buySword, buyReload, buyMilk, buyChocolate, buyEnergy};
                chosenUpgrade = common[Random.Range(0, common.Length)];
            }

            else
            {
                GameObject[] epic = {buySpeaker};
                chosenUpgrade = epic[Random.Range(0, epic.Length)];
            }


            chosenUpgrade.SetActive(true);
            RectTransform rect = chosenUpgrade.GetComponent<RectTransform>();
            if(i == 0)
            {
                rect.anchoredPosition = new Vector2(-400f, 0f);
            }
            else if(i == 1)
            {
                rect.anchoredPosition = new Vector2(0f, 0f);
            }
            else if(i == 2)
            {
                rect.anchoredPosition = new Vector2(400f, 0f);
            }
        }

    }

    public void buyUpgrade(string UpgradeType)
    {
        if(UpgradeType == "DAMAGE" && XP >= 30f)
        {
            XP -= 30f;
            upgradeDamage();
        }

        else if(UpgradeType == "HEALTH" && XP >= 30f)
        {
            XP -= 30f;
            upgradeHealth();
        }

        else if(UpgradeType == "RELOAD" && XP>= 30f)
        {
            XP -= 30f;
            upgradeReload(); //lowk no need for these js upgrade in the buy upgrade section
        }

        else if(UpgradeType == "ENERGYDRINK" && XP >= 30f)
        {
            XP -= 30f;
            playerMovement.moveSpeed = playerMovement.moveSpeed * 1.3f;
        }

        else if(UpgradeType == "CHOCOLATE" && XP >= 30f)
        {
            XP -= 30f;
            GameObject spawnedBar = Instantiate(chocolateBar, new Vector2(Random.Range(-20f, 20f), Random.Range(-15f, 15f)), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        }

        else if(UpgradeType == "SPEAKER" && XP >= 120f)
        {
            XP -= 120f;
            GameObject spawnedSpeaker = Instantiate(speaker, new Vector2(Random.Range(-20f, 20f), Random.Range(-15f, 15f)), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        }
        shop.SetActive(false);
        mobSpawner.startNextlevel();
    }

    // Update is called once per frame
    void Update()
    {
        XPtext.text = XP.ToString();
        hpBar.value = currentHP;
        if(gameManager.currentGameState == gameManager.gameState.Play)
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
            if(currentHP <= 0f)
            {
                gameManager.currentGameState = gameManager.gameState.Lost;
                Debug.Log("You lost");
            }
        }


    }

    public void Shoot(Transform target)
    {
        GameObject projectile = Instantiate(needle, transform.position, Quaternion.identity);
        projectileScript projscript = projectile.GetComponent<projectileScript>();
        projscript.shootTowards(target, needleDamage);
    }
}
