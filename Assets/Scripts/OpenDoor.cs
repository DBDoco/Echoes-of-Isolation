using System.Collections;
using UnityEngine;
using TMPro;

public class OpenDoor : MonoBehaviour
{
    public GameObject Door;
    public float Distance;
    public TextMeshProUGUI interactionText;
    public GameObject ObjctiveComplete;
    public AudioSource doorSound;
    private bool doorIsOpening = false;

    void Start()
    {
        doorSound.playOnAwake = false;
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
            interactionText.text = "[E] Open the door";
            interactionText.enabled = true;
        }

        if (Input.GetButtonDown("Action"))
        {
            if (Distance <= 2 && !doorIsOpening)
            {
                Door.GetComponent<Animator>().enabled = true;
                StartCoroutine(OpenTheDoor());
                ObjctiveComplete.SetActive(true);
            }
        }
    }

    void OnMouseExit()
    {
        interactionText.enabled = false;
    }
}
