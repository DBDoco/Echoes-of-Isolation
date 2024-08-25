using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnLevel : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene(0);
    }

}
