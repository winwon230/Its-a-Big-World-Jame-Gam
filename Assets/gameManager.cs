using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public gameState currentGameState = gameState.Pause;
    public mobSpawner spawner;

    public enum gameState
    {
        Pause, Play
    }
    // Start is called before the first frame update
    void Start()
    {
        startGame(); // add tutorial later if i want to
    }

    public void startGame()
    {
        currentGameState = gameState.Play;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
