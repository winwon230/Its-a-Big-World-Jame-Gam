using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class mobSpawner : MonoBehaviour
{
    public GameObject mob;
    public gameManager manager;

    public bool levelStarted = false;

    public float currentLevel;
    public float enemyCount; // this is for all enemies
    public float generalMobCount;
    
    // Start is called before the first frame update
    void Start()
    {
        currentLevel = 1f;
    }

    public void startLevel1()
    {
        generalMobCount = 10f;
        levelStarted = true;
    }

    public void levelComplete()
    {
        currentLevel += 1f;
        generalMobCount = 10f + (currentLevel - 1f)*5f; //10 at base and 5 per level inc

    }

    public void startNextlevel()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("ENEMY").Length;
        if(enemyCount < 1f && levelStarted == true)
        {
            levelStarted = false; // wait till another function calls level started tobe enabled
            levelComplete();
        }
    }
}
