using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Sway Settings")]
    public float swayAmount = 0.02f;  
    public float maxSwayAmount = 0.06f; 
    public float smoothAmount = 6f; 

    [Header("Bobbing Settings")]
    public float bobbingAmount = 0.05f;  
    public float bobbingSpeed = 5f;  

    [Header("References")]
    public Rigidbody playerRigidbody;  

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;

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
        float movementX = -Input.GetAxis("Mouse X") * swayAmount;
        float movementY = -Input.GetAxis("Mouse Y") * swayAmount;

        movementX = Mathf.Clamp(movementX, -maxSwayAmount, maxSwayAmount);
        movementY = Mathf.Clamp(movementY, -maxSwayAmount, maxSwayAmount);

        float waveslice = Mathf.Sin(Time.time * bobbingSpeed);
        float bobbingOffset = 0f;

        if (playerRigidbody != null && playerRigidbody.velocity.magnitude > 0.1f)
        {
            bobbingOffset = waveslice * bobbingAmount;
        }

        Vector3 finalPosition = new Vector3(movementX, movementY + bobbingOffset, 0) + initialPosition;

        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition, Time.deltaTime * smoothAmount);
    }
}
