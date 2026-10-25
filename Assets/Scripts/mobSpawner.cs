using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class mobSpawner : MonoBehaviour
{
    public GameObject mob;
    public gameManager manager;

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
        enemyCount = 10f + (currentLevel - 1f)*5f; //10 at base and 5 per level inc

    }

    public void startNextlevel()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentEnemyCount = GameObject.FindGameObjectsWithTag("ENEMY").Length;
        if(currentEnemyCount < 1f && levelStarted == true)
        {
            levelStarted = false; // wait till another function calls level started tobe enabled
            levelComplete();
            Debug.Log("Congratulations! You finished level one!");
        }
    }
}
