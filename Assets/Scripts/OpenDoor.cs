using System.Collections;
using UnityEngine;
using TMPro;

public class OpenDoor : MonoBehaviour
{
    public GameObject Door;
    public float Distance;
    public TextMeshProUGUI interactionText;
    public AudioSource doorSound;
    private bool doorIsOpening = false;

    public string requiredKeyColor; // The color of the key required to open this door
    private KeyCollection keyCollection; // Reference to the player's key collection

    void Start()
    {
        doorSound.playOnAwake = false;
        keyCollection = GameObject.FindWithTag("Player").GetComponent<KeyCollection>(); // Ensure the player has the KeyCollection script
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
            if (keyCollection.HasKey(requiredKeyColor))
            {
                interactionText.text = "[E] Open the door";
            }
            else
            {
                interactionText.text = "You need the " + requiredKeyColor + " key";
            }
            interactionText.enabled = true;
        }

        if (Input.GetButtonDown("Action"))
        {
            if (Distance <= 2 && !doorIsOpening)
            {
                if (keyCollection.HasKey(requiredKeyColor))
                {
                    Door.GetComponent<Animator>().enabled = true;
                    StartCoroutine(OpenTheDoor());
                }
                else
                {
                    Debug.Log("Door cannot be opened without the " + requiredKeyColor + " key.");
                }
            }
        }
    }

    void OnMouseExit()
    {
        interactionText.enabled = false;
    }
}
