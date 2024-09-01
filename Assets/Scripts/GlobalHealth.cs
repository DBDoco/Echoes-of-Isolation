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

    public Camera fpsCamera; 
    public float fallDuration = 2.0f; 
    public string gameOverScene = "GameOver"; 
    public float sceneSwitchDelay = 3.0f; 
    public float fallDistance = 1.5f; 

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        PlayerHealth = 5;
        hurtSounds = new AudioSource[] { Hurt001, Hurt002, Hurt003 };
        ScreenFlash.SetActive(false); 
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
        string currentSceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("LastLevel", currentSceneName);

        StartCoroutine(HandleDeath());
    }

    private IEnumerator HandleDeath()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(FallCamera());

        yield return new WaitForSeconds(sceneSwitchDelay);

        SceneManager.LoadScene(gameOverScene);
    }

    private IEnumerator FallCamera()
    {
        Vector3 startRotation = fpsCamera.transform.rotation.eulerAngles;
        Vector3 endRotation = startRotation + new Vector3(60, 0, 0);

        Vector3 startPosition = fpsCamera.transform.position;
        Vector3 endPosition = startPosition + new Vector3(0, -fallDistance, 0);

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

        fpsCamera.transform.rotation = Quaternion.Euler(endRotation);
        fpsCamera.transform.position = endPosition;
    }

    private void TriggerScreenFlash()
    {
        ScreenFlash.SetActive(true);
        StartCoroutine(DisableFlashAfterDelay(0.1f)); 
    }

    private IEnumerator DisableFlashAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ScreenFlash.SetActive(false); 
    }

    private void PlayRandomHurtSound()
    {
        int randomIndex = Random.Range(0, hurtSounds.Length);
        hurtSounds[randomIndex].Play();
    }
}
