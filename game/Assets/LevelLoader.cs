using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator animator;
    public float transitionTime = 1f;
    public void LoadGame()
    {
        StartCoroutine(ReloadScene());
    }

    IEnumerator ReloadScene()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
