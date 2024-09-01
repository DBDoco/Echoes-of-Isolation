using TMPro;
using System.Collections;
using UnityEngine;

public class DocumentInteractionLevel003 : MonoBehaviour
{
    public float interactionDistance = 2f;
    public GameObject documentUI;
    public AudioSource paperPickupSound;
    public AudioSource playerVoice;
    public TextMeshProUGUI interactionText;
    public GameObject TheSubs;

    private bool isDocumentPickedUp = false;
    private bool hasPlayedVoice = false;
    private Transform playerTransform;

    void Start()
    {
        paperPickupSound.playOnAwake = false;
        playerVoice.playOnAwake = false;
        documentUI.SetActive(false);
        playerTransform = Camera.main.transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isDocumentPickedUp)
            {
                CloseDocument();
            }
            else if (distance <= interactionDistance)
            {
                PickupDocument();
            }
        }
    }

    void PickupDocument()
    {
        paperPickupSound.Play();
        isDocumentPickedUp = true;
        documentUI.SetActive(true);
        interactionText.enabled = false;
    }

    IEnumerator PlayVoiceWithDelay()
    {
        yield return new WaitForSeconds(2);
        playerVoice.Play();
        TheSubs.GetComponent<TextMeshProUGUI>().text = "Michael, what have they done to you?";
        yield return new WaitForSeconds(2);
        TheSubs.GetComponent<TextMeshProUGUI>().text = "";
    }

    void OnMouseOver()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        if (!isDocumentPickedUp && distance <= interactionDistance)
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
        if (!hasPlayedVoice)
        {
            StartCoroutine(PlayVoiceWithDelay());
            hasPlayedVoice = true;
        }
    }
}