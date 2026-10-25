using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public gameState currentGameState = gameState.Pause;
    public mobSpawner spawner;
    public GameObject menu;

    public enum gameState
    {
        Pause, Play, Lost
    }
    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
        currentGameState = gameState.Play;
        startGame(); // add shop as well and welcome text first
    }

    public void startGame()
    {
        currentGameState = gameState.Play;
        spawner.startLevel1();
    }

    public void quitGame()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void restartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        currentGameState = gameState.Play;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) //effective pause, need to use a menu tho
        {
            if(currentGameState == gameState.Play)
            {
                currentGameState = gameState.Pause;
                Time.timeScale = 0f;
                menu.SetActive(true);   
            }

            else if(currentGameState == gameState.Pause)
            {
                currentGameState = gameState.Play;
                Time.timeScale = 1f;
                menu.SetActive(false);
            }
        }
    }
}
