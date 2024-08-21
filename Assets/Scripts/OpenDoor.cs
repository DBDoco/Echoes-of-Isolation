using System.Collections;
using UnityEngine;
using TMPro;  

public class OpenDoor : MonoBehaviour
{
    public GameObject Door;
    public float Distance;
    public TextMeshProUGUI interactionText;
    public GameObject ObjctiveComplete;

    void Update()
    {
        Distance = PlayerCasting.DistanceFromTarget;
    }

    private IEnumerator OpenTheDoor()
    {
        yield return new WaitForSeconds(4);
        Door.GetComponent<Animator>().enabled = false;
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
            if (Distance <= 2)
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
