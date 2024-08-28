using System.Collections;
using UnityEngine;
using TMPro;

public class OpenDoorWithKeycards : MonoBehaviour
{
    public GameObject Door;
    public float Distance;
    public TextMeshProUGUI interactionText;
    public AudioSource doorSound;
    private bool doorIsOpening = false;

    private CardFragmentCollection cardCollection; 

    void Start()
    {
        doorSound.playOnAwake = false;
        cardCollection = GameObject.FindWithTag("Player").GetComponent<CardFragmentCollection>();
    }

    void Update()
    {
        Distance = PlayerCasting.DistanceFromTarget;
    }

    private IEnumerator OpenTheDoor()
    {
        doorIsOpening = true;
        doorSound.Play();
        yield return new WaitForSeconds(3);
        doorSound.Play();
        yield return new WaitForSeconds(2);
        Door.GetComponent<Animator>().enabled = false;
        doorIsOpening = false;
    }

    void OnMouseOver()
    {
        if (Distance <= 2)
        {
            if (cardCollection.HasAllCards())
            {
                interactionText.text = "[E] Open the door";
            }
            else
            {
                interactionText.text = "You need all three keycards to open this door";
            }
            interactionText.enabled = true;
        }

        if (Input.GetButtonDown("Action"))
        {
            if (Distance <= 2 && !doorIsOpening)
            {
                if (cardCollection.HasAllCards())
                {
                    Door.GetComponent<Animator>().enabled = true;
                    StartCoroutine(OpenTheDoor());
                }
                else
                {
                    Debug.Log("Door cannot be opened without all three keycards.");
                }
            }
        }
    }

    void OnMouseExit()
    {
        interactionText.enabled = false;
    }
}
