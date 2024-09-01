using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCollection : MonoBehaviour
{
    private HashSet<string> keysCollected = new HashSet<string>(); 

    public RawImage redKeyImage; 
    public RawImage blueKeyImage; 
    public RawImage greenKeyImage; 

    void Start()
    {
        redKeyImage.enabled = false;
        blueKeyImage.enabled = false;
        greenKeyImage.enabled = false;
    }

    public void CollectKey(string keyColor)
    {
        if (!keysCollected.Contains(keyColor))
        {
            keysCollected.Add(keyColor);
            UpdateKeyUI(keyColor);
        }
    }

    public bool HasKey(string keyColor)
    {
        return keysCollected.Contains(keyColor);
    }

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
