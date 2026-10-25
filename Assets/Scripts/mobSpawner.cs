using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class mobSpawner : MonoBehaviour
{
    public GameObject mob;
    public gameManager manager;

    private weaponScript weaponScript;// this is for the shop functions

    public TMP_Text waveText;

    public bool levelStarted = false;

    public float t1Count; //ants
    public float t2Count; //beetles
    public float t3Count; //spiders
    public float spawnDelay = 0.3f;


    public float currentLevel;
    public float enemyCount; // This is for the max no enemies
    public float currentEnemyCount; // this is for current no. enemies
    public float generalMobCount;
    
    // Start is called before the first frame update
    void Start()
    {
        weaponScript = FindFirstObjectByType<weaponScript>();
        currentLevel = 1f;
    }

    public void startLevel1()
    {
        enemyCount = 10f;
        t1Count = 9f;
        t2Count = 1f;
        t3Count = 0f;
        levelStarted = true;
        StartCoroutine(spawnWithDelay()); // shoudl spawn all mobs
    }

    private IEnumerator spawnWithDelay()
    {
            for(int i = 0; i<t1Count; i++)
            {
                Vector2 position = new Vector2(Random.Range(-20f, 20f), Random.Range(-15f, 15f));
                GameObject spawnedAnt = Instantiate(mob, position, Quaternion.identity);
                enemyScript antScript = spawnedAnt.GetComponent<enemyScript>();
                antScript.enemyType = "ANT";
                yield return new WaitForSeconds(spawnDelay);
            }

            for(int i = 0; i<t2Count; i++)
            {
                Vector2 position = new Vector2(Random.Range(-20f, 20f), Random.Range(-15f, 15f));
                GameObject spawnedBeetle = Instantiate(mob, position, Quaternion.identity);
                enemyScript beetleScript = spawnedBeetle.GetComponent<enemyScript>();
                beetleScript.enemyType = "BEETLE";
                yield return new WaitForSeconds(spawnDelay);
            }

            for(int i = 0; i<t3Count; i++)
            {
                Vector2 position = new Vector2(Random.Range(-20f, 20f), Random.Range(-15f, 15f));
                GameObject spawnedSpider = Instantiate(mob, position, Quaternion.identity);
                enemyScript spiderScript = spawnedSpider.GetComponent<enemyScript>();
                spiderScript.enemyType = "SPIDER";
                yield return new WaitForSeconds(spawnDelay);                
            }
    }

    public void levelComplete()
    {
        currentLevel += 1f;
        enemyCount = 10f + (currentLevel - 1f)*5f; //10 at base and 10 per level inc
        t1Count = 0.9f * enemyCount;
        t2Count = 0.1f * enemyCount;
        t3Count = Mathf.FloorToInt(0.02f * enemyCount);
        weaponScript.openShop();

    }

    public void startNextlevel()
    {
        weaponScript.shop.SetActive(false);
        weaponScript.resetHealth();
        levelStarted = true;
        StartCoroutine(spawnWithDelay());
    }

    // Update is called once per frame
    void Update()
    {
        waveText.text = "Current Level: " + currentLevel.ToString();
        currentEnemyCount = GameObject.FindGameObjectsWithTag("ENEMY").Length;
        if(currentEnemyCount < 1f && levelStarted == true)
        {
            levelStarted = false; // wait till another function calls level started tobe enabled
            levelComplete();
            Debug.Log("Congratulations! You finished level one!");
        }
    }
}
