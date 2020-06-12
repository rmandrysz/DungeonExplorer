using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame() 
    {
        LevelLoader levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        levelLoader.LoadGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
