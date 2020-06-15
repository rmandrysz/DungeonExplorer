using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float restartTime = 1f;
    private bool endGame = false;
    private static GameManager instance;
    private int floorsPassed = 0;
    private int roomsCleared = 0;

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
            ResetPoints();
            Invoke("Restart", restartTime);
        }
        endGame = true;
    }

    public void Restart()
    {
        endGame = false;
        GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>().RestartGame();
        ResetPoints();
        
    }

    public void NextFloor()
    {
        floorsPassed += 1;
        GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>().NextFloor();
    }

    #region Score
    public int GetFloorsPassed()
    {
        return floorsPassed;
    }

    public void ClearRoom()
    {
        roomsCleared += 1;
    }

    public int GetRoomsCleared()
    {
        return roomsCleared;
    }

    public void ResetPoints()
    {
        floorsPassed = 0;
        roomsCleared = 0;
    }
    #endregion
}
