using System.Collections;
using UnityEngine;
using TMPro;

public class OpenDoorBoss : MonoBehaviour
{
    public float interactionDistance = 2f;
    public TextMeshProUGUI interactionText;
    public AudioSource doorSound;
    private bool doorIsOpened = false;

    private Transform playerTransform;
    private Animator doorAnimator;

    public Boss boss;

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

        if (boss == null)
        {
            Debug.LogError("Boss reference not set on the door.");
        }
    }

    void Update()
    {
        if (playerTransform == null || boss == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= interactionDistance)
        {
            if (!doorIsOpened)
            {
                if (boss.health <= 0.1f)
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
                    interactionText.text = "Defeat the boss to open the door";
                    interactionText.enabled = true;
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
