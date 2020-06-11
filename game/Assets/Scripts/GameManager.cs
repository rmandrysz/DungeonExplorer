using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float restartTime = 1f;
    private bool endGame = false;
    private static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void EndGame()
    {
        if (!endGame)
        {
            Invoke("Restart", restartTime);
        }
        endGame = true;
    }

    private void Restart()
    {
        endGame = false;
        GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>().LoadGame();
    }
}
