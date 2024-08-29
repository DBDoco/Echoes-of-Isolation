using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pickup9mm : MonoBehaviour
{
    public float Distance = PlayerCasting.DistanceFromTarget;
    public GameObject FakeGun;
    public GameObject RealGun;

    public AudioSource gunPickupSound;

    public TextMeshProUGUI AmmoDisplay;
    public TextMeshProUGUI LoadedDisplay;
    public TextMeshProUGUI Divider;
    public TextMeshProUGUI AmmoLabel;

    public GameObject ObjectiveComplete;

    public TextMeshProUGUI interactionText;

    void Start() {
        gunPickupSound.playOnAwake = false;
    }

    void Update()
    {
        Distance = PlayerCasting.DistanceFromTarget;
    }

    void Take9mm()
    {
        gunPickupSound.Play();
        transform.position = new Vector3(0, -1000, 0);
        FakeGun.SetActive(false);
        RealGun.SetActive(true);

        AmmoLabel.text = "9mm";

        AmmoDisplay.gameObject.SetActive(true);
        LoadedDisplay.gameObject.SetActive(true);
        Divider.gameObject.SetActive(true);
    }

    void OnMouseOver()
    {
        if (Distance <= 2)
        {
            interactionText.text = "[E] Pick up pistol";
            interactionText.enabled = true;
        }

        if (Input.GetButtonDown("Action"))
        {

        if (Distance <= 2) {
            Take9mm();
            if (ObjectiveComplete != null)
                ObjectiveComplete.SetActive(true);
        }
        }
    }

    void OnMouseExit()
    {
        interactionText.enabled = false;
    }
}