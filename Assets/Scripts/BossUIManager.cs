using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUIManager : MonoBehaviour
{
    public static BossUIManager instance; 

    public Text bossNameText; 
    public Image bossHealthBarFillImage;
    public GameObject bossUIContainer;

    private float maxHealth;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        HideBossUI();
    }

    public void SetBossName(string name)
    {
        if (bossNameText != null)
        {
            bossNameText.text = name;
            ShowBossUI();
        }
    }

    public void SetBossHealth(float health)
    {
        if (bossHealthBarFillImage != null)
        {
            maxHealth = health;
            bossHealthBarFillImage.fillAmount = maxHealth / maxHealth;
        }
    }

    public void UpdateBossHealth(float currentHealth)
    {
        if (bossHealthBarFillImage != null)
        {
            bossHealthBarFillImage.fillAmount = currentHealth / maxHealth;
        }
    }

    public void ShowBossUI()
    {
        if (bossUIContainer != null)
        {
            bossUIContainer.SetActive(true);
        }
    }


    public void HideBossUI()
    {
        if (bossUIContainer != null)
        {
            bossUIContainer.SetActive(false);
        }
    }

    public void ResetBossUI()
    {
        if (bossHealthBarFillImage != null)
        {
            bossHealthBarFillImage.fillAmount = 1f; 
        }

        if (bossNameText != null)
        {
            bossNameText.text = "";
        }

        HideBossUI();
    }
}
