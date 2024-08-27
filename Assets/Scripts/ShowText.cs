using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class ShowText : MonoBehaviour
{
    public string displayMessage = "Hold [CTRL] to crouch";
    public Text uiText; 

    void Start()
    {
        if (uiText == null)
        {
            Debug.LogError("UI Text component not assigned. Please assign a UI Text component in the Inspector.");
        }
        else
        {
            uiText.text = "";
            uiText.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (uiText != null)
            {
                uiText.gameObject.SetActive(true);
                uiText.text = displayMessage;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (uiText != null)
            {
                uiText.text = "";
                uiText.gameObject.SetActive(false);
            }
        }
    }
}
