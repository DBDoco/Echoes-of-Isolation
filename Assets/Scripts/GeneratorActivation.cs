using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GeneratorActivation : MonoBehaviour
{
    public float interactionDistance = 2f;
    public AudioSource activationSound;
    public AudioSource runningSoundLoop;
    public AudioSource Voice;
    public GameObject TheSubs;
    public TextMeshProUGUI interactionText;
    public Light generatorLight;
    public GameObject objectiveComplete;
    public static int totalGenerators = 4;

    private static int enabledGenerators = 0;
    private bool isEnabled = false;
    private Transform playerTransform;

    void Start()
    {
        if (enabledGenerators > 0)
        {
            enabledGenerators = 0;
        }

        GameObject player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;

        runningSoundLoop.loop = true;
        runningSoundLoop.playOnAwake = false;

        if (generatorLight != null)
        {
            generatorLight.enabled = false;
        }

        if (objectiveComplete != null)
        {
            objectiveComplete.SetActive(false);
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= interactionDistance && !isEnabled)
        {
            interactionText.text = "[E] Enable Generator";
            interactionText.enabled = true;

            if (Input.GetButtonDown("Action"))
            {
                EnableGenerator();
            }
        }
        else
        {
            interactionText.enabled = false;
        }
    }

    private void EnableGenerator()
    {
        if (!isEnabled)
        {
            isEnabled = true;
            activationSound.Play();
            runningSoundLoop.Play();

            if (generatorLight != null)
            {
                generatorLight.enabled = true;
            }

            enabledGenerators++;

            if (enabledGenerators == totalGenerators && objectiveComplete != null)
            {
                StartCoroutine(Sub());
                objectiveComplete.SetActive(true);
            }
        }
    }

    private IEnumerator Sub()
    {
        Voice.Play();
        TheSubs.GetComponent<Text>().text = "That should be all of the generators. I better keep going forward.";
        yield return new WaitForSeconds(5);
        TheSubs.GetComponent<Text>().text = "";
    }

    public bool IsEnabled()
    {
        return isEnabled;
    }
}
