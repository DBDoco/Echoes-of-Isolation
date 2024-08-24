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

    void Awake()
    {
        // Store instance for static access
        instance = this;
    }

    void Start()
    {
        // Initialize hurt sounds array
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
            Die();
        }
    }

    public static void Die()
    {
        // Handle player death (reload the scene, show game over screen, etc.)
        SceneManager.LoadScene("GameOver");
        // Unlock and show the cursor
        PlayerHealth = 5;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
