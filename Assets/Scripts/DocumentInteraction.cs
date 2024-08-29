using TMPro;
using UnityEngine;
using System.Collections;

public class DocumentInteraction : MonoBehaviour
{
    public float Distance = PlayerCasting.DistanceFromTarget;
    public GameObject documentUI;
    public AudioSource paperPickupSound;
    public AudioSource playerVoice;
    public TextMeshProUGUI interactionText;
    public GameObject objectiveComplete;
    private bool isDocumentPickedUp = false;
    private bool hasPlayedVoice = false;

    void Start()
    {
        paperPickupSound.playOnAwake = false;
        playerVoice.playOnAwake = false;
        documentUI.SetActive(false);
    }

    void Update()
    {
        Distance = PlayerCasting.DistanceFromTarget;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isDocumentPickedUp)
            {
                CloseDocument();
            }
            else if (Distance <= 2)
            {
                PickupDocument();
            }
        }
    }

    void PickupDocument()
    {
        paperPickupSound.Play();
        objectiveComplete.SetActive(true);
        isDocumentPickedUp = true;
        documentUI.SetActive(true);
        interactionText.enabled = false;

        if (!hasPlayedVoice)
        {
            StartCoroutine(PlayVoiceWithDelay());
            hasPlayedVoice = true;
        }
    }

    IEnumerator PlayVoiceWithDelay()
    {
        yield return new WaitForSeconds(3f);
        playerVoice.Play();
    }

    void OnMouseOver()
    {
        if (!isDocumentPickedUp && Distance <= 2)
        {
            interactionText.text = "[E] Pick up document";
            interactionText.enabled = true;
        }
    }

    void OnMouseExit()
    {
        interactionText.enabled = false;
    }

    public void CloseDocument()
    {
        documentUI.SetActive(false);
        isDocumentPickedUp = false;
    }
}