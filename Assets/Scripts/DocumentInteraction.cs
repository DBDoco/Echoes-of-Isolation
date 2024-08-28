using TMPro;
using UnityEngine;

public class DocumentInteraction : MonoBehaviour
{
    public float Distance = PlayerCasting.DistanceFromTarget; // Assuming PlayerCasting script provides the distance
    public GameObject documentUI; // The UI element that displays the document content
    public AudioSource paperPickupSound; // The sound to play when picking up the document

    public TextMeshProUGUI interactionText; // The TextMeshProUGUI for interaction text

    private bool isDocumentPickedUp = false;

    void Start()
    {
        paperPickupSound.playOnAwake = false; // Ensure the sound doesn't play on start
        documentUI.SetActive(false); // Ensure the document UI is hidden at start
    }

    void Update()
    {
        Distance = PlayerCasting.DistanceFromTarget; // Update distance from player to object
    }

    void PickupDocument()
    {
        paperPickupSound.Play(); // Play the paper pickup sound
        isDocumentPickedUp = true; // Mark the document as picked up
        documentUI.SetActive(true); // Show the document UI
        interactionText.enabled = false; // Hide the interaction text
    }

    void OnMouseOver()
    {
        if (!isDocumentPickedUp && Distance <= 2)
        {
            interactionText.text = "[E] Pick up document"; // Set the interaction text
            interactionText.enabled = true; // Show the interaction text

            if (Input.GetButtonDown("Action"))
            {
                if (Distance <= 2)
                {
                    PickupDocument(); // Call the function to pick up the document
                }
            }
        }
    }

    void OnMouseExit()
    {
        interactionText.enabled = false; // Hide the interaction text when not hovering
    }

    public void CloseDocument()
    {
        documentUI.SetActive(false); // Hide the document UI when closing
        isDocumentPickedUp = false; // Allow document interaction again if needed
    }
}