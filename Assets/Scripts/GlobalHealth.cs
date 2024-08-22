using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GlobalHealth : MonoBehaviour
{
    public static int PlayerHealth = 10;
    public int InternalHealth;
    public GameObject HealthDisplay;

    // Update is called once per frame
    void Update()
    {
        InternalHealth = PlayerHealth;

        HealthDisplay.GetComponent<TextMeshProUGUI>().text = "Health: " + PlayerHealth;

        if (PlayerHealth == 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}
