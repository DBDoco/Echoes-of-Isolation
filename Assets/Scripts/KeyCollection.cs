using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCollection : MonoBehaviour
{
    private HashSet<string> keysCollected = new HashSet<string>(); // Tracks collected keys

    public RawImage redKeyImage; // UI RawImage for the Red Key
    public RawImage blueKeyImage; // UI RawImage for the Blue Key
    public RawImage greenKeyImage; // UI RawImage for the Green Key

    void Start()
    {
        // Disable the key images by default
        redKeyImage.enabled = false;
        blueKeyImage.enabled = false;
        greenKeyImage.enabled = false;
    }

    // Call this method when the player collects a key
    public void CollectKey(string keyColor)
    {
        if (!keysCollected.Contains(keyColor))
        {
            keysCollected.Add(keyColor);
            UpdateKeyUI(keyColor);
        }
    }

    // Check if the player has a specific key
    public bool HasKey(string keyColor)
    {
        return keysCollected.Contains(keyColor);
    }

    // Update the UI with the collected keys
    private void UpdateKeyUI(string keyColor)
    {
        switch (keyColor)
        {
            case "Red":
                redKeyImage.enabled = true;
                break;
            case "Blue":
                blueKeyImage.enabled = true;
                break;
            case "Green":
                greenKeyImage.enabled = true;
                break;
        }
    }
}
