using System.Collections;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{

    public Animator animator;
    public float transitionTime = 1f;
    public GameObject loadingScreen;
    public Slider slider;
    public void ReloadGame()
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
