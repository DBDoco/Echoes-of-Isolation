using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutroController : MonoBehaviour
{
    public AudioSource outroAudio;
    private bool isSkipping = false;
    private float enterHoldTime = 2.0f;  
    private float enterHoldTimer = 0f;

    void Start()
    {
        StartCoroutine(PlayAudio());
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            enterHoldTimer += Time.deltaTime;
            if (enterHoldTimer >= enterHoldTime && !isSkipping)
            {
                isSkipping = true;
                StartCoroutine(SkipOutro());
            }
        }
        else
        {
            enterHoldTimer = 0f;
        }
    }

    private IEnumerator PlayAudio()
    {
        outroAudio.Play();
        yield return new WaitForSeconds(56f);
        if (!isSkipping)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private IEnumerator SkipOutro()
    {
        outroAudio.Stop();  
        SceneManager.LoadScene("MainMenu");
        yield return null;
    }
}
