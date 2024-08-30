using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GlobalHealth : MonoBehaviour
{
    public static int PlayerHealth = 5;
    public int InternalHealth;
    public GameObject HealthDisplay;

    public GameObject ScreenFlash;
    public AudioSource Hurt001;
    public AudioSource Hurt002;
    public AudioSource Hurt003;
    private static AudioSource[] hurtSounds;
    private static GlobalHealth instance;

    public Camera fpsCamera; // Reference to the FPS camera
    public float fallDuration = 2.0f; // Duration of the camera falling animation
    public string gameOverScene = "GameOver"; // The scene to load on death
    public float sceneSwitchDelay = 3.0f; // Delay before switching the scene
    public float fallDistance = 1.5f; // Distance the camera falls

    void Awake()
    {
        // Store instance for static access
        instance = this;
    }

    void Start()
    {
        // Initialize hurt sounds array
        PlayerHealth = 5;
        hurtSounds = new AudioSource[] { Hurt001, Hurt002, Hurt003 };
        ScreenFlash.SetActive(false); // Ensure flash is initially disabled
    }

    void Update()
    {
        InternalHealth = PlayerHealth;

        HealthDisplay.GetComponent<TextMeshProUGUI>().text = "Health: " + PlayerHealth;

        if (PlayerHealth <= 0)
        {
            Die();
        }
    }

    public static void ApplyDamage(int damageAmount)
    {
        PlayerHealth -= damageAmount;

        // Trigger screen flash and hurt sound
        instance.TriggerScreenFlash();
        instance.PlayRandomHurtSound();

        if (PlayerHealth <= 0)
        {
            PlayerHealth = 0;
            instance.Die();
        }
    }

    public void Die()
    {
        // Store the current scene name before dying
        string currentSceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("LastLevel", currentSceneName);

        // Handle player death (camera fall and then load game over scene)
        StartCoroutine(HandleDeath());
    }

    private IEnumerator HandleDeath()
    {
        // Unlock and show the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Start the camera fall
        StartCoroutine(FallCamera());

        // Wait for the specified delay before switching the scene
        yield return new WaitForSeconds(sceneSwitchDelay);

        // Load the game over scene
        SceneManager.LoadScene(gameOverScene);
    }

    private IEnumerator FallCamera()
    {
        Vector3 startRotation = fpsCamera.transform.rotation.eulerAngles;
        Vector3 endRotation = startRotation + new Vector3(60, 0, 0); // Rotate the camera downwards

        Vector3 startPosition = fpsCamera.transform.position;
        Vector3 endPosition = startPosition + new Vector3(0, -fallDistance, 0); // Move the camera downwards

        float elapsedTime = 0f;
        while (elapsedTime < fallDuration)
        {
            float t = elapsedTime / fallDuration;
            Vector3 currentRotation = Vector3.Lerp(startRotation, endRotation, t);
            Vector3 currentPosition = Vector3.Lerp(startPosition, endPosition, t);
            fpsCamera.transform.rotation = Quaternion.Euler(currentRotation);
            fpsCamera.transform.position = currentPosition;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position and rotation are exactly the end position and rotation
        fpsCamera.transform.rotation = Quaternion.Euler(endRotation);
        fpsCamera.transform.position = endPosition;
    }

    private void TriggerScreenFlash()
    {
        ScreenFlash.SetActive(true);
        StartCoroutine(DisableFlashAfterDelay(0.1f)); // Disable the flash after a short delay
    }

    private IEnumerator DisableFlashAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ScreenFlash.SetActive(false); // Disable the screen flash
    }

    private void PlayRandomHurtSound()
    {
        int randomIndex = Random.Range(0, hurtSounds.Length);
        hurtSounds[randomIndex].Play();
    }
}
