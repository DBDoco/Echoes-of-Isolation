using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject Crosshair;
    private MonoBehaviour[] playerControllerScripts;
    private FirstPersonLook firstPersonLookScript;
    private Zoom zoomScript;
    private bool isPaused = false;

    void Start()
    {
        // Find the player controller object in the scene
        GameObject playerControllerObject = GameObject.FindWithTag("Player");

        if (playerControllerObject != null)
        {
            // Get all MonoBehaviour scripts attached to the player controller object
            playerControllerScripts = playerControllerObject.GetComponents<MonoBehaviour>();
        }
        else
        {
            Debug.LogError("Player Controller not found! Make sure the Player object has the 'Player' tag assigned.");
        }

        // Ensure the cursor is locked and invisible when the game starts
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

    private void FindMainCameraScripts()
    {
        GameObject mainCameraObject = GameObject.FindWithTag("MainCamera");

        if (mainCameraObject != null)
        {
            // Get the specific scripts we want to disable/enable on the Main Camera
            firstPersonLookScript = mainCameraObject.GetComponent<FirstPersonLook>();
            zoomScript = mainCameraObject.GetComponent<Zoom>();
        }
        else
        {
            Debug.LogError("Main Camera not found! Make sure the camera object has the 'MainCamera' tag assigned.");
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);

        // Find the MainCamera scripts each time Pause is triggered
        FindMainCameraScripts();

        // Disable all scripts attached to the player controller object
        if (playerControllerScripts != null)
        {
            foreach (MonoBehaviour script in playerControllerScripts)
            {
                script.enabled = false;
            }
        }

        // Disable the FirstPersonLook and Zoom scripts on the Main Camera
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

        // Unlock and show the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);

        // Re-enable all scripts attached to the player controller object
        if (playerControllerScripts != null)
        {
            foreach (MonoBehaviour script in playerControllerScripts)
            {
                script.enabled = true;
            }
        }

        // Re-enable the FirstPersonLook and Zoom scripts on the Main Camera
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

        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
