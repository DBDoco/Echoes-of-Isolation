using UnityEngine;
using TMPro;

public class Pickup9mm : MonoBehaviour
{
    public float interactionDistance = 2f;
    public GameObject FakeGun;
    public GameObject RealGun;
    public AudioSource gunPickupSound;
    public TextMeshProUGUI AmmoDisplay;
    public TextMeshProUGUI LoadedDisplay;
    public TextMeshProUGUI Divider;
    public TextMeshProUGUI AmmoLabel;
    public GameObject ObjectiveComplete;
    public TextMeshProUGUI interactionText;

    private Transform playerTransform;

    void Start()
    {
        gunPickupSound.playOnAwake = false;
        playerTransform = Camera.main.transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance <= interactionDistance)
        {
            if (Input.GetButtonDown("Action"))
            {
                Take9mm();
            }
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
        if (ObjectiveComplete != null) ObjectiveComplete.SetActive(true);
    }

    void OnMouseOver()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        if (distance <= interactionDistance)
        {
            interactionText.text = "[E] Pick up pistol";
            interactionText.enabled = true;
        }
    }

    void OnMouseExit()
    {
        interactionText.enabled = false;
    }
}