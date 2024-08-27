using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Sway Settings")]
    public float swayAmount = 0.02f;  // Amount of sway
    public float maxSwayAmount = 0.06f;  // Maximum amount of sway
    public float smoothAmount = 6f;  // How smooth the sway will be

    [Header("Bobbing Settings")]
    public float bobbingAmount = 0.05f;  // Amount of bobbing up and down
    public float bobbingSpeed = 5f;  // Speed of bobbing

    [Header("References")]
    public Rigidbody playerRigidbody;  // Reference to the player's Rigidbody

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;

        // If Rigidbody isn't assigned, try to find it in the parent
        if (playerRigidbody == null)
        {
            playerRigidbody = GetComponentInParent<Rigidbody>();
        }

        if (playerRigidbody == null)
        {
            Debug.LogError("Rigidbody not found or assigned. Please ensure the script is on a gun object under a player object with a Rigidbody.");
        }
    }

    void Update()
    {
        // Sway based on mouse movement
        float movementX = -Input.GetAxis("Mouse X") * swayAmount;
        float movementY = -Input.GetAxis("Mouse Y") * swayAmount;

        // Clamp the sway amount to avoid excessive movement
        movementX = Mathf.Clamp(movementX, -maxSwayAmount, maxSwayAmount);
        movementY = Mathf.Clamp(movementY, -maxSwayAmount, maxSwayAmount);

        // Calculate the bobbing effect
        float waveslice = Mathf.Sin(Time.time * bobbingSpeed);
        float bobbingOffset = 0f;

        if (playerRigidbody != null && playerRigidbody.velocity.magnitude > 0.1f)
        {
            bobbingOffset = waveslice * bobbingAmount;
        }

        // Combine sway and bobbing
        Vector3 finalPosition = new Vector3(movementX, movementY + bobbingOffset, 0) + initialPosition;

        // Smoothly move towards the target position
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition, Time.deltaTime * smoothAmount);
    }
}
