using UnityEngine;

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
