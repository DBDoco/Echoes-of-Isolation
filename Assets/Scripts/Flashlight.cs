using UnityEngine;

public class FlashlightToggle : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.F;  // The key to toggle the flashlight
    public AudioClip toggleSound;          // The sound to play when toggling
    public GameObject ObjctiveComplete;
    private Light flashlight;
    private AudioSource audioSource;


    void Start()
    {
        // Find the Light component attached to this GameObject
        flashlight = GetComponent<Light>();
        if (flashlight == null)
        {
            Debug.LogError("Light component not found. Attach this script to a GameObject with a Light component.");
        }

        // Find or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Check if the player pressed the toggle key
        if (Input.GetKeyDown(toggleKey))
        {
            if (flashlight != null)
            {
                flashlight.enabled = !flashlight.enabled;
                ObjctiveComplete.SetActive(true);

                // Play the toggle sound
                if (toggleSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(toggleSound);
                }
            }
        }
    }
}
