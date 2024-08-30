using System.Collections;
using UnityEngine;
using TMPro;

public class OpenDoor : MonoBehaviour
{
    public float interactionDistance = 2f;
    public TextMeshProUGUI interactionText;
    public AudioSource doorSound;
    private bool doorIsOpened = false;

    public string requiredKeyColor;
    private KeyCollection keyCollection;
    private Transform playerTransform;

    private Animator doorAnimator;

    void Start()
    {
        doorSound.playOnAwake = false;
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found. Make sure the player has the 'Player' tag.");
            return;
        }
        keyCollection = player.GetComponent<KeyCollection>();
        if (keyCollection == null)
        {
            Debug.LogError("KeyCollection script not found on the player.");
            return;
        }
        playerTransform = player.transform;
        doorAnimator = GetComponent<Animator>();
        if (doorAnimator == null)
        {
            Debug.LogError("Animator component not found on the door.");
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= interactionDistance)
        {
            if (keyCollection.HasKey(requiredKeyColor) && !doorIsOpened)
            {
                interactionText.text = "[E] Open the door";
            }
            else
            {
                interactionText.text = "You need the " + requiredKeyColor + " key";
            }
            interactionText.enabled = true;

            if (Input.GetButtonDown("Action"))
            {
                if (!doorIsOpened)
                {
                    if (keyCollection.HasKey(requiredKeyColor))
                    {
                        StartCoroutine(OpenTheDoor());
                    }
                }
            }
        }
        else
        {
            interactionText.enabled = false;
        }
    }

    private IEnumerator OpenTheDoor()
    {
        doorIsOpened = true;
        doorSound.Play();
        doorAnimator.enabled = true;
        yield return new WaitForSeconds(1.2f);
        doorAnimator.enabled = false;
    }
}