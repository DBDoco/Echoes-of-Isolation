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

    void Start() {
        gunPickupSound.playOnAwake = false;
    }

    void Update()
    {
        Distance = PlayerCasting.DistanceFromTarget;

        if (Input.GetButtonDown("Action") && Distance <= 2) {
            Take9mm();
        }
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
}