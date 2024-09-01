using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentInteractionLevel003 : MonoBehaviour
{
    public float Distance = PlayerCasting.DistanceFromTarget;
    public GameObject documentUI;
    public AudioSource paperPickupSound;
    public AudioSource playerVoice;
    public TextMeshProUGUI interactionText;
    private bool isDocumentPickedUp = false;
    private bool hasPlayedVoice = false;
    public GameObject TheSubs;

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
        isDocumentPickedUp = true;
        documentUI.SetActive(true);
        interactionText.enabled = false;
    }

    IEnumerator PlayVoiceWithDelay()
    {
        yield return new WaitForSeconds(2);
        playerVoice.Play();
        TheSubs.GetComponent<Text>().text = "Is this the Michael I'm looking for?";
        yield return new WaitForSeconds(2);
        TheSubs.GetComponent<Text>().text = "";
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
        if (!hasPlayedVoice)
        {
            StartCoroutine(PlayVoiceWithDelay());
            hasPlayedVoice = true;
        }
    }
}