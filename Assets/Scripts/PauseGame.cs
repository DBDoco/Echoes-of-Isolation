using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject Crosshair;
    public List<AudioSource> activeAudioSources = new List<AudioSource>();
    private MonoBehaviour[] playerControllerScripts;
    private MonoBehaviour[] gunScripts;
    private FirstPersonLook firstPersonLookScript;
    private Zoom zoomScript;
    private bool isPaused = false;

    void Start()
    {
        GameObject playerControllerObject = GameObject.FindWithTag("Player");

        if (playerControllerObject != null)
        {
            playerControllerScripts = playerControllerObject.GetComponents<MonoBehaviour>();
        }

        TryFindGunScripts();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                Pause();
            }
        }
    }

    private void TryFindGunScripts()
    {
        GameObject gunObject = GameObject.FindWithTag("Gun");

        if (gunObject != null && gunObject.activeInHierarchy)
        {
            gunScripts = gunObject.GetComponents<MonoBehaviour>();
        }
        else
        {
            gunScripts = null;
        }
    }

    private void FindMainCameraScripts()
    {
        GameObject mainCameraObject = GameObject.FindWithTag("MainCamera");

        if (mainCameraObject != null)
        {
            // Get the specific scripts we want to disable/enable on the Main Camera
            firstPersonLookScript = mainCameraObject.GetComponent<FirstPersonLook>();
            zoomScript = mainCameraObject.GetComponent<Zoom>();
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);

        FindMainCameraScripts();
        TryFindGunScripts();

        PauseAllAudio();

        if (playerControllerScripts != null)
        {
            foreach (MonoBehaviour script in playerControllerScripts)
            {
                script.enabled = false;
            }
        }

        if (gunScripts != null)
        {
            foreach (MonoBehaviour script in gunScripts)
            {
                script.enabled = false;
            }
        }

        if (firstPersonLookScript != null)
        {
            firstPersonLookScript.enabled = false;
        }

        if (zoomScript != null)
        {
            zoomScript.enabled = false;
        }

        Time.timeScale = 0f;
        isPaused = true;
        Crosshair.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);

        if (playerControllerScripts != null)
        {
            foreach (MonoBehaviour script in playerControllerScripts)
            {
                script.enabled = true;
            }
        }

        if (gunScripts != null)
        {
            foreach (MonoBehaviour script in gunScripts)
            {
                script.enabled = true;
            }
        }

        if (firstPersonLookScript != null)
        {
            firstPersonLookScript.enabled = true;
        }

        if (zoomScript != null)
        {
            zoomScript.enabled = true;
        }

        Time.timeScale = 1f;
        isPaused = false;
        Crosshair.SetActive(true);
        UnpauseAllAudio();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void PauseAllAudio()
    {
        activeAudioSources.Clear();
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in allAudioSources)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
                activeAudioSources.Add(audioSource);
            }
        }
    }

    private void UnpauseAllAudio()
    {
        foreach (AudioSource audioSource in activeAudioSources)
        {
            audioSource.UnPause();
        }
        activeAudioSources.Clear();
    }

    public void Respawn()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
