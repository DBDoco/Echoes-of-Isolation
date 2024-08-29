using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    [Header("Stamina")]
    public GameObject staminaBarObject;
    public Image staminaBarFillImage;
    public float Stamina, MaxStamina = 100f;
    public float staminaDrainRate = 10f;
    public float staminaRegenRate = 5f;

    [Header("Audio")]
    public AudioSource audioSource;  // Reference to the AudioSource component
    public AudioClip heavyBreathingClip;  // The heavy breathing sound clip

    private Rigidbody rb;
    private bool isBreathingSoundPlaying = false;

    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Stamina = MaxStamina;

        // Ensure the AudioSource is properly configured
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        UpdateStamina();
        UpdateStaminaBar();
    }

    void FixedUpdate()
    {
        UpdateMovement();
    }

    void UpdateStamina()
    {
        IsRunning = canRun && Input.GetKey(runningKey) && Stamina > 0;

        if (IsRunning)
        {
            Stamina -= staminaDrainRate * Time.deltaTime;
            staminaBarObject.SetActive(true);
            StopBreathingSound();
        }
        else
        {
            Stamina += staminaRegenRate * Time.deltaTime;

            // Play heavy breathing sound if stamina is low and not running
            if (Stamina < MaxStamina && !IsRunning && !isBreathingSoundPlaying)
            {
                PlayBreathingSound();
            }
        }

        Stamina = Mathf.Clamp(Stamina, 0, MaxStamina);

        if (Stamina <= 0)
        {
            canRun = false;
        }
        else if (Stamina >= MaxStamina)
        {
            canRun = true;
            if (!IsRunning)
            {
                StartCoroutine(HideStaminaBar());
            }
        }
    }

    void UpdateStaminaBar()
    {
        float fillAmount = Stamina / MaxStamina;
        staminaBarFillImage.fillAmount = fillAmount;
    }

    void UpdateMovement()
    {
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);
        rb.velocity = transform.rotation * new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.y);
    }

    private IEnumerator HideStaminaBar()
    {
        yield return new WaitForSeconds(2.5f);
        if (!IsRunning && Stamina >= MaxStamina)
        {
            staminaBarObject.SetActive(false);
        }
    }

    private void PlayBreathingSound()
    {
        if (audioSource != null && heavyBreathingClip != null)
        {
            audioSource.clip = heavyBreathingClip;
            audioSource.Play();
            isBreathingSoundPlaying = true;
        }
    }

    private void StopBreathingSound()
    {
        if (audioSource != null && isBreathingSoundPlaying)
        {
            audioSource.Stop();
            isBreathingSoundPlaying = false;
        }
    }
}
