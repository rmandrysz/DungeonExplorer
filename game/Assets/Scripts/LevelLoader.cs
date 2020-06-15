using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{

    public Animator animator;
    public float transitionTime = 1f;
    public GameObject loadingScreen;
    private GameManager gameManager;
    public Slider slider;
    private int mainMenuIndex = 0;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
    public void RestartGame()
    {
 
        StartCoroutine(ReloadScene());
    }

    public void LoadMenu()
    {
        StartCoroutine(LoadScene(mainMenuIndex));
        gameManager.ResetPoints();
    }

    public void NextFloor()
    {
        StartCoroutine(ReloadScene());
    }
    IEnumerator LoadSceneAsync(int sceneIndex)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            yield return null;
        }

    }

    IEnumerator LoadScene(int sceneIndex)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator ReloadScene()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);


        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            Debug.Log("Scene loading progress: "+ progress);

            yield return null;
        }
    }

    #region Main Menu

    public GameObject mainMenu;

    public void LoadGame()
    {
        mainMenu.SetActive(false);

        StartCoroutine(LoadSceneAsync("Dungeon"));
    }

    #endregion
}
