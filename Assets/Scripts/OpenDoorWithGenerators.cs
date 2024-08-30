using System.Collections;
using UnityEngine;
using TMPro;

public class OpenDoorWithGenerators : MonoBehaviour
{
    public float interactionDistance = 2f;
    public TextMeshProUGUI interactionText;
    public AudioSource doorSound;
    public GeneratorActivation[] generators;
    private bool doorIsOpened = false;

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

        if (distanceToPlayer <= interactionDistance && !doorIsOpened)
        {
            if (AreAllGeneratorsEnabled())
            {
                interactionText.text = "[E] Open the door";
                interactionText.enabled = true;

                if (Input.GetButtonDown("Action"))
                {
                    StartCoroutine(OpenTheDoor());
                }
            }
            else
            {
                interactionText.text = "The door is locked. Enable all generators to open.";
                interactionText.enabled = true;
            }
        }
        else
        {
            interactionText.enabled = false;
        }
    }

    private bool AreAllGeneratorsEnabled()
    {
        foreach (GeneratorActivation generator in generators)
        {
            if (!generator.IsEnabled())
            {
                return false;
            }
        }
        return true;
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
