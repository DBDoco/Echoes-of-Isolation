using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeneratorActivation : MonoBehaviour
{
    public float interactionDistance = 2f;
    public AudioSource activationSound;
    public AudioSource runningSoundLoop;
    public TextMeshProUGUI interactionText;
    public Light generatorLight; 
    public GameObject objectiveComplete;  
    public static int totalGenerators = 4; 

    private static int enabledGenerators = 0;  
    private bool isEnabled = false;
    private Transform playerTransform;

    void Start()
    {
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
                objectiveComplete.SetActive(true);  
            }
        }
    }

    public bool IsEnabled()
    {
        return isEnabled;
    }
}
