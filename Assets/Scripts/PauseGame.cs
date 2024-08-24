using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject Player;
    public GameObject PlayerCamera;
    private bool isPaused = false;

    void Start()
    {
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

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Player.SetActive(false);
        PlayerCamera.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        // Unlock and show the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Player.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;

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