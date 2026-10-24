using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void startGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
