using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Add this if you're using TextMeshPro

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
    public Image staminaBarFillImage;  // Change this to the fill image of your stamina bar
    public float Stamina, MaxStamina = 100f;
    public float staminaDrainRate = 10f;
    public float staminaRegenRate = 5f;

    private Rigidbody rb;
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Stamina = MaxStamina;
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
        }
        else
        {
            Stamina += staminaRegenRate * Time.deltaTime;
        }

        Stamina = Mathf.Clamp(Stamina, 0, MaxStamina);

        if (Stamina <= 0)
        {
            canRun = false;
        }
        else if (Stamina >= 30)
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
}