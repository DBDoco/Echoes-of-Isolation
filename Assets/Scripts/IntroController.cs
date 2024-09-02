using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public AudioSource introAudio;
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
                StartCoroutine(SkipIntro());
            }
        }
        else
        {
            enterHoldTimer = 0f;
        }
    }

    private IEnumerator PlayAudio()
    {
        introAudio.Play();
        yield return new WaitForSeconds(64f);
        if (!isSkipping)
        {
            SceneManager.LoadScene("Level001");
        }
    }

    private IEnumerator SkipIntro()
    {
        introAudio.Stop();  
        SceneManager.LoadScene("Level001");
        yield return null;
    }
}
