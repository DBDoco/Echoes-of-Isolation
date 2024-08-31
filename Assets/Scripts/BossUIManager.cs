using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUIManager : MonoBehaviour
{
    public static BossUIManager instance; // Singleton instance

    public Text bossNameText; // UI Text component to display the boss's name
    public Image bossHealthBarFillImage;
    public GameObject bossUIContainer; // The container that holds the boss UI elements

    private float maxHealth;

    void Awake()
    {
        // Implement singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes if necessary
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        HideBossUI(); // Initially hide the boss UI until a boss is encountered
    }

    /// <summary>
    /// Sets the boss name in the UI.
    /// </summary>
    /// <param name="name">The name of the boss.</param>
    public void SetBossName(string name)
    {
        if (bossNameText != null)
        {
            bossNameText.text = name;
            ShowBossUI();
        }
    }

    /// <summary>
    /// Sets the boss's max health and initializes the health bar.
    /// </summary>
    /// <param name="health">The max health of the boss.</param>
    public void SetBossHealth(float health)
    {
        if (bossHealthBarFillImage != null)
        {
            maxHealth = health;
            bossHealthBarFillImage.fillAmount = maxHealth / maxHealth;
        }
    }

    /// <summary>
    /// Updates the boss's health bar.
    /// </summary>
    /// <param name="currentHealth">The current health of the boss.</param>
    public void UpdateBossHealth(float currentHealth)
    {
        if (bossHealthBarFillImage != null)
        {
            bossHealthBarFillImage.fillAmount = currentHealth / maxHealth;
        }
    }

    /// <summary>
    /// Shows the boss UI when a boss is encountered.
    /// </summary>
    public void ShowBossUI()
    {
        if (bossUIContainer != null)
        {
            bossUIContainer.SetActive(true);
        }
    }

    /// <summary>
    /// Hides the boss UI when the boss is defeated.
    /// </summary>
    public void HideBossUI()
    {
        if (bossUIContainer != null)
        {
            bossUIContainer.SetActive(false);
        }
    }
}
