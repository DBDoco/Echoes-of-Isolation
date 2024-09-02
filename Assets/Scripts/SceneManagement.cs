using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void PlayGame()
    {
        string lastLevel = PlayerPrefs.GetString("LastLevel", "Level001");

        StartCoroutine(ReloadScene(lastLevel));
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator ReloadScene(string sceneName)
    {
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);

        while (!unloadOperation.isDone)
        {
            yield return null;
        }

        System.GC.Collect();
        Resources.UnloadUnusedAssets();

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!loadOperation.isDone)
        {
            yield return null;
        }
    }
}
