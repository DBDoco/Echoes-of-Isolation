using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject Crosshair;
    private MonoBehaviour[] playerControllerScripts;
    private MonoBehaviour[] gunScripts;
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

        // Try to find and store gun scripts if the gun is active in the scene
        TryFindGunScripts();

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

    private void TryFindGunScripts()
    {
        // Find the gun object in the scene
        GameObject gunObject = GameObject.FindWithTag("Gun");

        if (gunObject != null && gunObject.activeInHierarchy)
        {
            // Get all MonoBehaviour scripts attached to the gun object if it's active
            gunScripts = gunObject.GetComponents<MonoBehaviour>();
        }
        else
        {
            Debug.Log("Gun object not found or is inactive. Skipping gun script management.");
            gunScripts = null; // Clear any previous references to avoid errors
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
        TryFindGunScripts();

        // Disable all scripts attached to the player controller object
        if (playerControllerScripts != null)
        {
            foreach (MonoBehaviour script in playerControllerScripts)
            {
                script.enabled = false;
            }
        }

        // Disable all scripts attached to the gun object, if available
        if (gunScripts != null)
        {
            foreach (MonoBehaviour script in gunScripts)
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

        // Re-enable all scripts attached to the gun object, if available
        if (gunScripts != null)
        {
            foreach (MonoBehaviour script in gunScripts)
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
