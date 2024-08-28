using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardFragmentCollection : MonoBehaviour
{
    private HashSet<string> cardsCollected = new HashSet<string>();

    public RawImage redCardImage;
    public RawImage blueCardImage;
    public RawImage yellowCardImage;
    public GameObject ObjctiveComplete; // Moved here

    void Start()
    {
        redCardImage.enabled = false;
        blueCardImage.enabled = false;
        yellowCardImage.enabled = false;

        // Ensure ObjctiveComplete is initially disabled
        if (ObjctiveComplete != null)
        {
            ObjctiveComplete.SetActive(false);
        }
    }

    public void CollectCardFragment(string cardColor)
    {
        if (!cardsCollected.Contains(cardColor))
        {
            cardsCollected.Add(cardColor);
            UpdateCardUI(cardColor);

            // Check if all cards are collected
            if (HasAllCards() && ObjctiveComplete != null)
            {
                ObjctiveComplete.SetActive(true); // Activate the objective complete object
            }
        }
    }

    public bool HasAllCards()
    {
        return cardsCollected.Contains("Red") && cardsCollected.Contains("Blue") && cardsCollected.Contains("Yellow");
    }

    private void UpdateCardUI(string cardColor)
    {
        switch (cardColor)
        {
            case "Red":
                redCardImage.enabled = true;
                break;
            case "Blue":
                blueCardImage.enabled = true;
                break;
            case "Yellow":
                yellowCardImage.enabled = true;
                break;
        }
    }
}
