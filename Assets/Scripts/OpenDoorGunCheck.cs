using System.Collections;
using UnityEngine;
using TMPro;

public class OpenDoorGunCheck : MonoBehaviour
{
    public float interactionDistance = 2f;
    public TextMeshProUGUI interactionText;
    public AudioSource doorSound;
    public string requiredKeyColor;
    public GameObject Pistol;

    private bool doorIsOpened = false;
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

        if (Pistol == null)
        {
            Debug.LogError("Pistol GameObject reference is missing. Please assign it in the inspector.");
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= interactionDistance)
        {
            if (!doorIsOpened)
            {
                if (keyCollection.HasKey(requiredKeyColor) && Pistol.activeInHierarchy)
                {
                    interactionText.text = "[E] Open the door";
                }
                else if (!keyCollection.HasKey(requiredKeyColor))
                {
                    interactionText.text = "You need the " + requiredKeyColor + " key";
                }
                else if (!Pistol.activeInHierarchy)
                {
                    interactionText.text = "You need to pick up the pistol";
                }

                interactionText.enabled = true;

                if (Input.GetButtonDown("Action"))
                {
                    if (keyCollection.HasKey(requiredKeyColor) && Pistol.activeInHierarchy)
                    {
                        StartCoroutine(OpenTheDoor());
                    }
                }
            }
            else
            {
                interactionText.text = "";
                interactionText.enabled = false;
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
