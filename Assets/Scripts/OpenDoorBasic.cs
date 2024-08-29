using System.Collections;
using UnityEngine;
using TMPro;

public class OpenDoorBasic : MonoBehaviour
{
    public float interactionDistance = 3f;
    public TextMeshProUGUI interactionText;
    public AudioSource doorSound;
    private bool doorIsOpened = false;
    private Transform playerTransform;
    private Animator doorAnimator;

    void Start()
    {
        doorSound.playOnAwake = false;
        GameObject player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;
        doorAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerTransform == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= interactionDistance && !doorIsOpened)
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
            interactionText.enabled = false;
        }
    }

    private IEnumerator OpenTheDoor()
    {
        doorIsOpened = true;
        interactionText.enabled = false;
        doorSound.Play();
        doorAnimator.enabled = true;
        yield return new WaitForSeconds(1.2f);
        doorAnimator.enabled = false;
    }
}
